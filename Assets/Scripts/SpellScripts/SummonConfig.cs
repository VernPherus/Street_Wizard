using System.Collections.Generic;
using UnityEngine;
using WeaponsScripts.Damage;
using WeaponsScripts.Modifiers;

[CreateAssetMenu(fileName = "Summon Configuration", menuName = "Runes/Summon Configuration")]
public class SummonConfig : ScriptableObject
{

    [SerializeField] public int ManaConsumption = 20;
    [SerializeField] public int SummonCooldown = 30;
    [SerializeField] public int ImbueDuration = 30;

    [Header("Weapon Overrides")]
    [SerializeField] public float DamageOverride;
    [SerializeField] public ImpactType ImpactTypeOverride;
    [SerializeField] public DamageType DamageTypeOverride;

    public List<IModifier> modifiers = new();

    public IModifier[] ReturnModifiers()
    {
        List<IModifier> modifiersToApply = new();

        // DamageModifier damageModifier = new()
        // {
        //     Amount = DamageOverride,
        //     AttributeName = "DamageConfig/DamageCurve"
        // };

        DamageTypeModifier damageTypeModifier = new()
        {
            Amount = DamageTypeOverride,
            AttributeName = "DamageConfig/DamageType"
        };

        ImpactTypeModifier impactTypeModifier = new()
        { 
            Amount = ImpactTypeOverride,
        };

        // modifiersToApply.Add(damageModifier);
        modifiersToApply.Add(damageTypeModifier);

        // modifiers.Add(damageModifier);
        modifiers.Add(damageTypeModifier);

        return modifiersToApply.ToArray();
    }

}