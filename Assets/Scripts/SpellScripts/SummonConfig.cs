using UnityEngine;

[CreateAssetMenu(fileName = "Summon Configuration", menuName = "Runes/Summon Configuration")]
public class SummonConfig : ScriptableObject
{
    [SerializeField] public int ManaConsumption;
    [SerializeField] public int SummonCooldown;
    [SerializeField] public int ImbueDuration;
}