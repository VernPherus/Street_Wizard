using UnityEngine;
using WeaponsScripts.Damage;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    [SerializeField]
    private float _MaxHealth = 100f;
    [SerializeField]
    private float _Health;

    [Header("Damage params")]
    [SerializeField]
    private DamageType weakness;
    [SerializeField]
    private DamageType immunity;
    [SerializeField]
    private DamageCalculator damageCalculator;

    public float CurrentHealth { get => _Health; private set => _Health = value; }

    public float MaxHealth { get => _MaxHealth; private set => _MaxHealth = value; }

    public event IDamageable.TakeDamageEvent OnTakeDamage;
    public event IDamageable.DeathEvent OnDeath;

    private void OnEnable()
    {
        CurrentHealth = MaxHealth;
    }

    // Applies damage to the enemy
    public void TakeDamage(float Damage, DamageType damageType = DamageType.defaultDamage)
    {
        float damageTaken = Mathf.Clamp(Damage, 0, CurrentHealth);

        CurrentHealth -= damageCalculator.BasicDamage(damageTaken, damageType, weakness, immunity);

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