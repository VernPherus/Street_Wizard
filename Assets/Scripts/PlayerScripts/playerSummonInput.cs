using System;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    none,
    up,
    down,
    left,
    right
}

[DisallowMultipleComponent]
public class playerSummonInput : MonoBehaviour
{

    [SerializeField] SummonManager summonManager;

    private List<String> Sequence = new List<string>();
    private int maxSequenceLength = 4;

    private List<String> directionChar = new() { "U", "D", "L", "R" };

    private void Awake()
    {
        summonManager = GetComponent<SummonManager>();
    }

    public void TranslateInput(Direction inputDirection)
    {
        switch (inputDirection)
        {
            case Direction.up:
                Sequence.Add(directionChar[0]);
                break;
            case Direction.down:
                Sequence.Add(directionChar[1]);
                break;
            case Direction.left:
                Sequence.Add(directionChar[2]);
                break;
            case Direction.right:
                Sequence.Add(directionChar[3]);
                break;
            default:
                break;
        }

        if (Sequence.Count == maxSequenceLength)
        {
            SummonSpell(ConvertInputToID(Sequence));
            Sequence.Clear();
        }

    }

    private String ConvertInputToID(List<String> sequence)
    {
        return string.Join("", sequence);
    }

    public void SummonSpell(String id)
    {
        summonManager.SpawnFromSequence(id);
    }

    public void DespawnRune()
    {
        summonManager.DespawnRune();
    }

}