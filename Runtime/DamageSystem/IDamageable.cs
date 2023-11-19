namespace Eu4ng.Framework.Game.DamageSystem
{
	public class DamageType
	{

	}

	public interface IDamageable
	{
		void ApplyDamage(float damage, DamageType damageType);
	}
}
