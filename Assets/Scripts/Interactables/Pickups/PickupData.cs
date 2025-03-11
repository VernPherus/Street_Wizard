using UnityEngine;

[CreateAssetMenu(fileName = "PickupData", menuName = "Interactables/Pickup")]
public class PickupData : ScriptableObject
{
    [Header("Pickup Config")]
    public PickupType pickupType;
    public int Amount;
    public bool IncrementPickupCounter;
}