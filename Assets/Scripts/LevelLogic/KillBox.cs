using UnityEngine;

public class KillBox : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        // Destroy the object that enters the collider
        Destroy(other.gameObject);
    }
}