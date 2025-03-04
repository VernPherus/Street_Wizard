using System.Reflection;
using UnityEngine;

namespace WeaponsScripts.Modifiers
{
    public class Vector3Modifer : AbstractValueModifier<Vector3>
    {
        public override void Apply(WeaponScriptableObject weapon)
        {
            try
            {
                Vector3 value = GetAttribute<Vector3>(
                    weapon,
                    out object targetObject,
                    out FieldInfo field
                );
                value = new(value.x * Amount.x, value.y * Amount.x, value.z * Amount.z);
                field.SetValue(targetObject, value);
            }
            catch (InvalidPathSpecifiedException) { }
        }
    }
}