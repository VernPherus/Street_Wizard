using UnityEngine;

namespace WeaponsScripts.Modifiers
{
    public interface IModifier
    {
        void Apply(WeaponScriptableObject weapon);
    }
}
