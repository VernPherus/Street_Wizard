using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    private new Rigidbody rigidbody;
    [field: SerializeField]
    public Vector3 SpawnLocation
    {
        get;
        private set;
    }
    [SerializeField] 
    private float DelayedDisableTime = 2f;

    public delegate void CollisionEvent(Projectile projectile, Collision collision);
    public event CollisionEvent OnCollision;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    public void Spawn(Vector3 SpawnForce)
    {
        SpawnLocation = transform.position;
        transform.forward = SpawnForce.normalized;
        rigidbody.AddForce(SpawnForce);
        StartCoroutine(DelayedDisable(DelayedDisableTime));
    }

    private IEnumerator DelayedDisable(float time)
    {
        yield return new WaitForSeconds(time);
        OnCollisionEnter(null);
    }

    private void OnCollisionEnter(Collision collision)
    {
        OnCollision?.Invoke(this, collision);
    }

    private void OnDisable()
    {
        StopAllCoroutines();
        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;
        OnCollision = null;
    }

}