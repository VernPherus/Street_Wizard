using UnityEngine;

public class ExplosiveBarrel : MonoBehaviour
{
    public EnemyHealth health;

    private void Start()
    {
        health.OnDeath += SelfDestruct;
    }

    private void SelfDestruct(Vector3 position)
    {
        Destroy(gameObject);
    }
}