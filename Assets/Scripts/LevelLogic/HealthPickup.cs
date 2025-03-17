using UnityEngine;

[RequireComponent(typeof(Collider))]
public class HealthPickup : MonoBehaviour
{

    [SerializeField] PickupData pickupData;
    public Vector3 spinDirection = Vector3.up;

    private void Update()
    {
        transform.Rotate(spinDirection);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerStats playerStats))
        {
            playerStats.health.GainHealth(pickupData.Amount);
        }
        
        Destroy(gameObject);
    }

}