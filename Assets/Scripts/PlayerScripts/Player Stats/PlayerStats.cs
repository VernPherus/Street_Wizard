using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public PlayerHealth health;
    public PlayerMana mana;

    public bool hasRedKey = false;
    public bool hasGreenKey = false;

    public bool hasUnlockedLimitless = false;
    public bool hasUnlockedBanisher = false;
    public bool hasUnlockedGatlingWand = false;
    public bool hasUnlockedBigBore = false;

    public int PickupCounter = 0;
    public int KillCounter = 0;
    public int SecretCounter = 0;

    private void Awake()
    {
    }

    public void UnlockRedKey() { hasRedKey = true; }
    public void UnlockGreendKey() { hasGreenKey = true; }

    public void IncrementKillCounter() { KillCounter++; }
    public void IncrementSecretCounter() { SecretCounter++; }
    public void IncrementPickupCounter() { PickupCounter++; }

}