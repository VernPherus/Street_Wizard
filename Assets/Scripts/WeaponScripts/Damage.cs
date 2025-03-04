using UnityEngine;

public enum DamageType
{
    fireDamage,
    frostDamage,
    rotDamage,
    poisonDamage,
    magicDamage,
    ballisticDamage,
    explosiveDamage
}

public class Damage : MonoBehaviour
{

    [Header("Damage Multiplier Params")]
    [SerializeField] private float FireMultiplier;
    [SerializeField] private float FrostMultiplier;
    [SerializeField] private float RotMultiplier;
    [SerializeField] private float PoisonMultiplier;
    [SerializeField] private float MagicMultiplier;
    [SerializeField] private float BallisticMultiplier;
    [SerializeField] private float ExplosiveMultiplier;


    // this function does all damage calculations
    // public float DamageCalculator(float weaponDamage, float damageMultiplier, float enemyResistance, float weaknessBonus, float enemyDefense)
    // {
    //     float res;

    //     res = (weaponDamage * damageMultiplier) * (1 - enemyResistance) * (1 + weaknessBonus) - enemyDefense;

    //     return res;
    // }

    public float GetMultiplier(DamageType damageType)
    {
        switch (damageType)
        {
            case DamageType.fireDamage:
                return FireMultiplier;
            case DamageType.frostDamage:
                return FrostMultiplier;
            case DamageType.rotDamage:
                return RotMultiplier;
            case DamageType.poisonDamage:
                return PoisonMultiplier;
            case DamageType.magicDamage:
                return MagicMultiplier;
            case DamageType.ballisticDamage:
                return BallisticMultiplier;
            case DamageType.explosiveDamage:
                return ExplosiveMultiplier;
            default:
                return 1f;
        }

    }
}
