using System.Collections;
using UnityEngine;

[RequireComponent(typeof(IDamageable))]
public class SpawnParticlesOnDeath : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem DeathSystem;
    public IDamageable Damageable;

    private ParticleSystem spawnedParticle;

    private void Awake()
    {
        Damageable = GetComponent<IDamageable>();
    }

    private void OnEnable()
    {
        Damageable.OnDeath += Damageable_OnDeath;
    }

    private void Damageable_OnDeath(Vector3 Position)
    {
        spawnedParticle = Instantiate(DeathSystem, Position, Quaternion.identity);
        // StartCoroutine(DestroyAfterSeconds());
    }

    // private IEnumerator DestroyAfterSeconds()   
    // {
    //     yield return new WaitForSeconds(5);
    //     Destroy(spawnedParticle);
    // }
}