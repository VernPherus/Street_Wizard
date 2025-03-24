using UnityEngine;
using WeaponsScripts;
using Managers;

[RequireComponent(typeof(Collider))]
public class GunPickup : MonoBehaviour
{
    public WeaponScriptableObject Weapon;
    public Vector3 SpinDirection = Vector3.up;

    [SerializeField]
    private UnlockPickupData unlockPickupData;

    [SerializeField]
    private PlayerStats playerStats;

    private void Update()
    {
        transform.Rotate(SpinDirection);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out WeaponManager WeaponManager))
        {
            playerStats.AddWeapon(Weapon.weaponType);
            WeaponManager.PickupWeapon(Weapon.weaponType);

            Destroy(gameObject);
        }
    }
}
