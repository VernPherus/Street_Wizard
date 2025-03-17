using UnityEngine;
using Managers;

enum KeyType
{
    RedKey,
    GreenKey
}

[RequireComponent(typeof(Collider))]
public class KeyPickup : MonoBehaviour
{
    [SerializeField] UnlockPickupData unlockPickupData;
    [SerializeField] private KeyType keyType;
    public Vector3 SpinDirection = Vector3.up;


    private void Update()
    {
        transform.Rotate(SpinDirection);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerStats playerStats))
        {
            if (keyType == KeyType.RedKey)
            {
                playerStats.UnlockRedKey();
            }
            if (keyType == KeyType.GreenKey)
            {
                playerStats.UnlockGreendKey();
            }

            if (unlockPickupData.IncrementPickupCounter)
            {
                playerStats.IncrementPickupCounter();
            }

            Destroy(gameObject);
        }
    }

}