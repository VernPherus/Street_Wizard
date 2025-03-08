using System.Collections;
using UnityEngine;

public class PlayerMana
{
    public float MaxMana;
    public float CurrentMana;
    public float RegenAmount;

    private bool IsRegenerating = false;


    private void Start()
    {
        CurrentMana = MaxMana;
    }

    public void ConsumeMana(float Amount)
    {
        float manaReduction = Mathf.Clamp(Amount, 0, CurrentMana);

        CurrentMana -= manaReduction;
    }

    public float ReturnMana()
    {
        return CurrentMana;
    }

    public IEnumerator RegenerateMana()
    {
        if (IsRegenerating) yield break;

        IsRegenerating = true;

        while (CurrentMana < MaxMana)
        {
            CurrentMana += RegenAmount;

        }

        IsRegenerating = false;
    }
}