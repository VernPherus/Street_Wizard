using UnityEngine;
using static UnityEngine.ParticleSystem;


namespace WeaponsScripts
{
    [CreateAssetMenu(fileName = "DamageConfiguration", menuName = "Weapons/DamageConfiguration")]
    public class DamageConfig : ScriptableObject, System.ICloneable
    {
        public MinMaxCurve DamageCurve;

        private void Reset()
        {
            DamageCurve.mode = ParticleSystemCurveMode.Curve;
        }

        public int GetDamage(float Distance = 0)
        {
            return Mathf.CeilToInt(DamageCurve.Evaluate(Distance, Random.value));
        }

        public object Clone()
        {
            DamageConfig config = CreateInstance<DamageConfig>();

            config.DamageCurve = DamageCurve;
            return config;
        }

    }
}

