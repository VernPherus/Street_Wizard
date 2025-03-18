using UnityEngine;

public class TeleporterScript : MonoBehaviour
{
    [SerializeField] Transform Destination;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(Destination.position, 0.4f);
        var direction = Destination.TransformDirection(Vector3.forward);
        Gizmos.DrawRay(Destination.position, direction);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && other.TryGetComponent<PlayerFPSController>(out var playerFPSController))
        {
            playerFPSController.Teleport(Destination.position, Destination.rotation);
            Debug.Log("Player entered");
        }
    }
}