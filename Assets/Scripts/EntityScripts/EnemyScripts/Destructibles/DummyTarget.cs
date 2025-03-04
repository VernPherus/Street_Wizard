
using UnityEngine;

public class DummyTarget : MonoBehaviour
{
    public EnemyHealth health;

    private void Start()
    {
        health.OnDeath += SelfDestruct;
    }

    private void SelfDestruct(Vector3 Position)
    {
        Destroy(gameObject);
    }
}
