using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    [SerializeField]
    private float _MaxHealth = 100f;
    [SerializeField]
    private float _Health;


    public float CurrentHealth { get => _Health; private set => _Health = value; }

    public float MaxHealth { get => _MaxHealth; private set => _MaxHealth = value; }

    public event IDamageable.TakeDamageEvent OnTakeDamage;
    public event IDamageable.DeathEvent OnDeath;

    private void OnEnable()
    {
        CurrentHealth = MaxHealth;
    }

    public void TakeDamage(float Damage)
    {
        float damageTaken = Mathf.Clamp(Damage, 0, CurrentHealth);

        CurrentHealth -= damageTaken;

        if (damageTaken != 0)
        {
            OnTakeDamage?.Invoke(damageTaken);
        }

        if (CurrentHealth == 0 && damageTaken != 0)
        {
            OnDeath?.Invoke(transform.position);
        }
    }
}