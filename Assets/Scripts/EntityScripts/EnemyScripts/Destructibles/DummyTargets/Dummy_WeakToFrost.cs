using UnityEngine;
using WeaponsScripts.Damage;

public class Dummy_WeakToFrost : MonoBehaviour
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