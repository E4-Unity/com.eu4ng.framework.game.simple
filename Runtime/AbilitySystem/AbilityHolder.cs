using System;
using System.Collections;
using UnityEngine;

namespace Eu4ng.Framework.Game.AbilitySystem
{
	public enum AbilityState
	{
		Ready,
		Active,
		Cooldown
	}

	[Serializable]
    public class AbilityHolder
    {
	    [SerializeReference] Ability _ability;
	    AbilityComponent _owner;
	    AbilityState _state = AbilityState.Ready;
	    WaitForSeconds _activeTime;
	    WaitForSeconds _cooldownTime;

	    public Ability CurrentAbility => _ability;

	    public void Init(AbilityComponent owner)
	    {
		    _owner = owner;

		    if (!_ability) return;
		    _activeTime = new WaitForSeconds(_ability.ActiveTime);
		    _cooldownTime = new WaitForSeconds(_ability.CooldownTime);
	    }

	    public void Activate(GameObject target = null)
	    {
		    if (!_ability || !CanActivate) return;

		    if(!_ability.TryActivate(_owner.gameObject, target)) return;
		    _state = AbilityState.Active;
		    _owner.StartCoroutine(ActiveTimer(target));
	    }

	    IEnumerator ActiveTimer(GameObject target)
	    {
		    yield return _activeTime;
		    _ability.Deactivate(_owner.gameObject, target);
		    _state = AbilityState.Cooldown;
		    _owner.StartCoroutine(CooldownTimer());
	    }

	    IEnumerator CooldownTimer()
	    {
		    yield return _cooldownTime;
		    _state = AbilityState.Ready;
	    }

	    public bool CanActivate => _state == AbilityState.Ready;
    }
}
