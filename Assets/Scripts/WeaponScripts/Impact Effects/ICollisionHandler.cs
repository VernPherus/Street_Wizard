using UnityEngine;

namespace WeaponsScripts.ImpactEffects
{
    public interface ICollisionHandler
    {
        void HandleImpact(
            Collider ImpactedObject,
            Vector3 HitPosition,
            Vector3 HitNormal,
            WeaponScriptableObject Weapon
        );
    }
}