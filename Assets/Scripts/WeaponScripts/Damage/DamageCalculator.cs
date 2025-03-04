using UnityEngine;

namespace WeaponsScripts.Damage
{
    public class DamageCalculator : MonoBehaviour
    {

        [Header("Damage Multiplier Params")]
        [SerializeField] private float FireMultiplier = 1.5f;
        [SerializeField] private float FrostMultiplier = 1.2f;
        [SerializeField] private float CrystalMultiplier = 1.2f;
        [SerializeField] private float PoisonMultiplier = 1.4f;
        [SerializeField] private float MagicMultiplier = 1.4f;
        [SerializeField] private float BallisticMultiplier = 1f;
        [SerializeField] private float ExplosiveMultiplier = 1.5f;

        public float BasicDamage(float weaponDamage, float damageMultiplier, float enemyResistance, float weaknessBonus, float enemyDefense)
        {
            float res;

            res = (weaponDamage * damageMultiplier) * (1 - enemyResistance) * (1 + weaknessBonus) - enemyDefense;

            return res;
        }

        public float DamageOverTime() { return 1f; }


        public float GetMultiplier(DamageType damageType)
        {
            return damageType switch
            {
                DamageType.fireDamage => FireMultiplier,
                DamageType.frostDamage => FrostMultiplier,
                DamageType.crystalDamage => CrystalMultiplier,
                DamageType.poisonDamage => PoisonMultiplier,
                DamageType.magicDamage => MagicMultiplier,
                DamageType.ballisticDamage => BallisticMultiplier,
                DamageType.explosiveDamage => ExplosiveMultiplier,
                _ => 1f,
            };
        }
    }
}

