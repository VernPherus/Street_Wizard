using System.Collections.Generic;
using UnityEngine;
using WeaponsScripts.Modifiers;

[CreateAssetMenu(fileName = "Summon Configuration", menuName = "Runes/Summon Configuration")]
public class SummonConfig : ScriptableObject
{

    [SerializeField] public int ManaConsumption;
    [SerializeField] public int SummonCooldown;
    [SerializeField] public int ImbueDuration;

    [Header("Weapon Overrides")]
    [SerializeField] public float DamageMultiplier;
    [SerializeField] public ImpactType ImpactTypeOverride;

    public List<IModifier> modifiers = new();

    public IModifier[] ReturnModifiers()
    {
        List<IModifier> modifiersToApply = new();

        DamageModifier damageModifier = new()
        {
            Amount = DamageMultiplier,
            AttributeName = "DamageConfig/DamageCurve"
        };

        modifiersToApply.Add(damageModifier);
        modifiers.Add(damageModifier);

        return modifiersToApply.ToArray();
    }

}