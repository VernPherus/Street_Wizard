using UnityEngine;
using WeaponsScripts.ImpactEffects;
using WeaponsScripts.Modifiers;

namespace WeaponsScripts
{
    public class WeaponModifierApplier : MonoBehaviour
    {

        [SerializeField]
        private ImpactType ImpactTypeOverride;

        [SerializeField]
        private WeaponManager weaponManager;

        private void Start()
        {
            new ImpactTypeModifier()
            {
                Amount = ImpactTypeOverride
            }.Apply(weaponManager.ActiveWeapon);

            weaponManager.ActiveWeapon.BulletImpactEffects = new ICollisionHandler[]
            {
                new Explode(
                    1.5f,
                    new AnimationCurve(new Keyframe[] {new Keyframe(0,1), new Keyframe(1, 0.25f)}),
                    10,
                    10
                )
            };
        }
    }
}