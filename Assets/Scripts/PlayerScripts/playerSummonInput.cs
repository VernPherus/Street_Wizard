using System.Collections.Generic;
using UnityEngine;
using Managers;

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
    [SerializeField] HUDController hUDController;

    private List<string> Sequence = new List<string>();
    private int maxSequenceLength = 4;

    private List<string> directionChar = new() { "U", "D", "L", "R" };

    private void Awake()
    {
        summonManager = GetComponent<SummonManager>();
        hUDController = GameObject.Find("PlayerHUD").GetComponent<HUDController>();
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

    private string ConvertInputToID(List<string> sequence)
    {
        return string.Join("", sequence);
    }

    public void SummonSpell(string id)
    {
        summonManager.SpawnFromSequence(id);
    }

    public void DespawnRune()
    {
        summonManager.DespawnRune();
    }

}