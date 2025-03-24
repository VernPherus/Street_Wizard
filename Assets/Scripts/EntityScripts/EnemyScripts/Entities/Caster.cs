using UnityEngine;

public class Caster : MonoBehaviour
{
    public EnemyHealth health;

    private void Start()
    {
        health.OnDeath += Die;
    }

    private void Die(Vector3 position)
    {
        Destroy(gameObject);
    }
}