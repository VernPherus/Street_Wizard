using UnityEngine;
using Managers;

[RequireComponent(typeof(Collider))]
public class AmmoPickup : MonoBehaviour
{
    [SerializeField] AmmoPickupData AmmoPickupData;
    public Vector3 SpinDirection = Vector3.up;

    private void Update()
    {
        transform.Rotate(SpinDirection);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out WeaponManager WeaponSelector)
            && WeaponSelector.ActiveWeapon.weaponType == AmmoPickupData.AmmoType)
        {
            WeaponSelector.ActiveWeapon.AmmoConfig.AddAmmo(AmmoPickupData.AmmoAmount);
            Destroy(gameObject);
        }
    }
}