using UnityEngine;

namespace WeaponsScripts.Damage
{
    
    public class DamageCalculator : MonoBehaviour
    {

        [Header("Damage Multiplier Params")]
        [SerializeField] private float EmberMultiplier = 1.5f;
        [SerializeField] private float FrostMultiplier = 1.2f;
        [SerializeField] private float CrystalMultiplier = 1.2f;
        [SerializeField] private float PoisonMultiplier = 1.4f;
        [SerializeField] private float MagicMultiplier = 1.4f;
        [SerializeField] private float BallisticMultiplier = 1f;
        [SerializeField] private float ExplosiveMultiplier = 1.5f;

        private float damageMultiplier = 0f;

        public float BasicDamage(float weaponDamage, DamageType damageType, DamageType weakness, DamageType immunity)
        {
            if (damageType == weakness)
            {
                damageMultiplier = GetMultiplier(weakness);
            }
            else
            {
                damageMultiplier = 1f;
            }

            if (damageType == immunity)
            {
                return 0f;
            }

            return weaponDamage * damageMultiplier;
        }

        public float DamageOverTime() { return 1f; }

        public float GetMultiplier(DamageType damageType)
        {
            return damageType switch
            {
                DamageType.emberDamage => EmberMultiplier,
                DamageType.frostDamage => FrostMultiplier,
                DamageType.crystalDamage => CrystalMultiplier,
                DamageType.poisonDamage => PoisonMultiplier,
                DamageType.magicDamage => MagicMultiplier,
                DamageType.ballisticDamage => BallisticMultiplier,
                DamageType.explosiveDamage => ExplosiveMultiplier,
                DamageType.defaultDamage => 1f,
                _ => 1f,
            };
        }
    }
}

