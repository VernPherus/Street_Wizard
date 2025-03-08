using UnityEngine;
using WeaponsScripts.Damage;

public class Dummy_WeakToFire : MonoBehaviour
{
    public EnemyHealth health;
    public DamageType weakness;

    private void Start()
    {
        health.OnDeath += SelfDestruct;
    }
    private void SelfDestruct(Vector3 Position)
    {
        Destroy(gameObject);
    }

}