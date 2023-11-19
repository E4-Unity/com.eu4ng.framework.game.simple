using UnityEngine;

namespace Eu4ng.Framework.Game.AbilitySystem
{
    public abstract class Ability : ScriptableObject
    {
	    [SerializeField] string _name;
	    [SerializeField] float _cooldownTime;
	    [SerializeField] float _activeTime;

	    public string Name => _name;
	    public float CooldownTime => _cooldownTime;
	    public float ActiveTime => _activeTime;

	    public bool TryActivate(GameObject owner, GameObject target)
	    {
		    if (!CanActivate) return false;

		    Activate(owner, target);

		    return true;
	    }
	    protected abstract void Activate(GameObject owner, GameObject target);
	    public virtual void Deactivate(GameObject owner, GameObject target){}
	    public virtual bool CanActivate => true;
    }
}
