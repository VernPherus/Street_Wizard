using UnityEngine;
using WeaponsScripts.Damage;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [SerializeField]
    private float _CurrentHealth = 100f;
    [SerializeField]
    private float _MaxHealth = 200f;

    public float CurrentHealth { get => _CurrentHealth; private set => _CurrentHealth = value; }

    public float MaxHealth { get => _MaxHealth; private set => _MaxHealth = value; }

    public event IDamageable.TakeDamageEvent OnTakeDamage;
    public event IDamageable.DeathEvent OnDeath;

    public void TakeDamage(float Damage, DamageType damageType = DamageType.defaultDamage)
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

    public void GainHealth(float Amount)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth + Amount, 0, MaxHealth);
    }
}