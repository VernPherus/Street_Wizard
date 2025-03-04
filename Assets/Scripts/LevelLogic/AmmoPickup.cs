using UnityEngine;
using WeaponsScripts;

[RequireComponent(typeof(Collider))]
public class AmmoPickup : MonoBehaviour
{
    public WeaponType Type;
    public int AmmoAmount = 30;
    public Vector3 SpinDirection = Vector3.up;

    private void Update()
    {
        transform.Rotate(SpinDirection);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out WeaponManager WeaponSelector)
            && WeaponSelector.ActiveWeapon.weaponType == Type)
        {
            WeaponSelector.ActiveWeapon.AmmoConfig.AddAmmo(AmmoAmount);
            Destroy(gameObject);
        }
    }
}