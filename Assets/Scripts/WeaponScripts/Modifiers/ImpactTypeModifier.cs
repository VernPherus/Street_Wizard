using UnityEngine;

namespace WeaponsScripts.Modifiers
{
    public class ImpactTypeModifier : AbstractValueModifier<ImpactType>
    {
        public override void Apply(WeaponScriptableObject Weapon)
        {
            Weapon.ImpactType = Amount;
        }
    }
}