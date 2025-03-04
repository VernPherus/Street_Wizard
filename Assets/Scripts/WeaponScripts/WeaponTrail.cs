using UnityEngine;

namespace WeaponsScripts
{
    [CreateAssetMenu(fileName = "WeaponTrail", menuName = "Weapons/WeaponTrail")]
    public class WeaponTrail : ScriptableObject, System.ICloneable
    {
        public Material material;
        public AnimationCurve widthCurve;

        public float Duration = 0.5f;
        public float MinVertexDistance = 0.1f;
        public Gradient Color;

        public float MissDistance = 100f;
        public float SimulationSpeed = 100f;

        public object Clone()
        {
            WeaponTrail config = CreateInstance<WeaponTrail>();

            Utilities.CopyValues(this, config);

            return config;
        }
    }
}

