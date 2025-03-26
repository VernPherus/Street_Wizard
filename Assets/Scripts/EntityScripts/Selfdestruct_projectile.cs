using UnityEngine;

public class SelfDestructOnCollision : MonoBehaviour
{
    public LayerMask destructibleLayers; // Set this in the Inspector to specify which layers trigger destruction

    private void OnCollisionEnter(Collision collision)
    {
        if (((1 << collision.gameObject.layer) & destructibleLayers) != 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & destructibleLayers) != 0)
        {
            Destroy(gameObject);
        }
        if (other.TryGetComponent(out IDamageable damageable))
        {
            damageable.TakeDamage(30);
        }
    }
}
