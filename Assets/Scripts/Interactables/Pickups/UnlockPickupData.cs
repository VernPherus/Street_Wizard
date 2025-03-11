using UnityEngine;

[CreateAssetMenu(fileName = "UnlockPickupData", menuName = "Interactables/UnlockPickup")]
public class UnlockPickupData : ScriptableObject
{
    [Header("Unlock config")]
    public PickupType pickupType;
    public bool isUnlocked = true;
    public bool IncrementPickupCounter;
}