using Managers;
using UnityEngine;
using WeaponsScripts;

[CreateAssetMenu(fileName = "AmmoPickupData", menuName = "Interactables/AmmoPickup")]
public class AmmoPickupData : ScriptableObject
{
    [Header("Ammo Config")]
    public WeaponType AmmoType;
    public int AmmoAmount;
}