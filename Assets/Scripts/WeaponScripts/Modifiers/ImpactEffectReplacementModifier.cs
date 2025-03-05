using WeaponsScripts.ImpactEffects;

namespace WeaponsScripts.Modifiers
{
    public class ImpactEffectReplacementModifier : AbstractValueModifier<ICollisionHandler[]>
    {
        public ImpactEffectReplacementModifier() : base() { }
        public ImpactEffectReplacementModifier(ICollisionHandler[] Values) : base(Values, "ShootConfig/BulletImpactEffects") { }

        public override void Apply(WeaponScriptableObject Weapon)
        {
            Weapon.BulletImpactEffects = Amount;
        }


    }
}