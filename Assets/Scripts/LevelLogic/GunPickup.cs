using UnityEngine;
using WeaponsScripts;
using Managers;

[RequireComponent(typeof(Collider))]
public class GunPickup : MonoBehaviour
{
    public WeaponScriptableObject Weapon;
    public Vector3 SpinDirection = Vector3.up;

    private void Update()
    {
        transform.Rotate(SpinDirection);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out WeaponManager WeaponManager))
        {
            WeaponManager.PickupGun(Weapon);
            Destroy(gameObject);
        }
    }
}
