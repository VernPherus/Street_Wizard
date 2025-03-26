using System.Collections.Generic;
using UnityEngine;
using WeaponsScripts;

public class PlayerStats : MonoBehaviour
{
    public PlayerHealth health;
    public PlayerMana mana;

    [SerializeField]
    private DeathScreen deathScreen;

    public bool hasRedKey = false;
    public bool hasGreenKey = false;

    public List<WeaponType> unlockedWeapons = new();

    public int PickupCounter = 0;
    public int KillCounter = 0;
    public int SecretCounter = 0;

    private void Awake()
    {
        unlockedWeapons.Add(WeaponType.RatFood);
    }

    private void Start()
    {
        health.OnDeath += HandleDeath;
    }

    public void UnlockRedKey() { hasRedKey = true; }
    public void UnlockGreendKey() { hasGreenKey = true; }

    public void IncrementKillCounter() { KillCounter++; }
    public void IncrementSecretCounter() { SecretCounter++; }
    public void IncrementPickupCounter() { PickupCounter++; }

    private void HandleDeath(Vector3 position)
    {
        deathScreen.ActivateDeathScreen();
    }

    public void AddWeapon(WeaponType weapon)
    {
        unlockedWeapons.Add(weapon);
    }

    public bool CheckForWeaponUnlock(WeaponType weaponType)
    {
        return unlockedWeapons.Contains(weaponType);
    }

}