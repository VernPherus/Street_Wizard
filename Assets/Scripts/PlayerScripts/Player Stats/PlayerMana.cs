using System.Collections;
using UnityEngine;

public class PlayerMana : MonoBehaviour
{
    public float MaxMana = 200f;
    public float CurrentMana = 100f;
    public float RegenAmount;

    private bool IsRegenerating = false;


    private void Start()
    {
        //CurrentMana = MaxMana;
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