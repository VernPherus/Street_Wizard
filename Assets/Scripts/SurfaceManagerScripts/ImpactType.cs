using System;
using UnityEngine;
using WeaponsScripts.Modifiers;

[CreateAssetMenu(menuName = "Impact System/Impact Type", fileName = "ImpactType")]
public class ImpactType : ScriptableObject
{
    public static implicit operator ImpactType(ImpactTypeModifier v)
    {
        throw new NotImplementedException();
    }
}