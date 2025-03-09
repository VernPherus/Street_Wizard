using UnityEngine;

public class Dummy_WeakToCrystal : MonoBehaviour
{
    public EnemyHealth health;
    public Vector3 SpinDirection = Vector3.left;      

    private void Start()
    {
        health.OnDeath += SelfDestruct;
    }

    private void SelfDestruct(Vector3 Position)
    {
        Destroy(gameObject);
    }

}