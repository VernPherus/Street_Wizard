using System.Reflection;

namespace WeaponsScripts.Modifiers
{
    public class FloatModifier : AbstractValueModifier<float>
    {

        public FloatModifier() : base() { }
        public FloatModifier(float Amount, string AttributeName) : base(Amount, AttributeName) { }

        public override void Apply(WeaponScriptableObject Weapon)
        {
            float value = GetAttribute<float>(Weapon, out object targetObject, out FieldInfo field);
            value *= Amount;
            field.SetValue(targetObject, value);
        }
    }
}