
using System.Reflection;
using WeaponsScripts.Damage;

namespace WeaponsScripts.Modifiers
{
    public class DamageTypeModifier : AbstractValueModifier<DamageType>
    {
        public override void Apply(WeaponScriptableObject Weapon)
        {
            try
            {
                DamageType damageType = GetAttribute<DamageType>(
                    Weapon,
                    out object targetObject,
                    out FieldInfo field
                );

                damageType = Amount;
                field.SetValue(targetObject, damageType);
            }
            catch (InvalidPathSpecifiedException) { }

        }
    }
}
