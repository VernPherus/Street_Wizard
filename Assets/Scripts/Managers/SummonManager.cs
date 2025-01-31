// INFO:
/**
* This script handles:
* Summon Mechanics
* Weapon summoning
*/
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

public class SummonManager : MonoBehaviour
{

    private List<string> sequence = new List<string>();
    private int maxSequenceLength = 6;

    private PlayerInputHandler playerInput;

    public static SummonManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("Summon Manager instance assigned");
        }
        else
        {
            Destroy(gameObject);
            Debug.Log("Summong Manager instance dublicate found, destroying...");
        }

        playerInput = PlayerInputHandler.Instance;
    }

    private void Update()
    {
        HandleSummonInput();
    }

    Direction GetArrowInputDirection()
    {
        // Vector2 arrowDirection = new Vector2(playerInput.RuneInput.x, playerInput.RuneInput.y);

        // if (arrowDirection == Vector2.up) return Direction.up;
        // if (arrowDirection == Vector2.down) return Direction.down;
        // if (arrowDirection == Vector2.left) return Direction.left;
        // if (arrowDirection == Vector2.right) return Direction.right;

        if (Input.GetKeyDown(KeyCode.UpArrow)) return Direction.up;
        if (Input.GetKeyDown(KeyCode.DownArrow)) return Direction.down;
        if (Input.GetKeyDown(KeyCode.LeftArrow)) return Direction.left;
        if (Input.GetKeyDown(KeyCode.RightArrow)) return Direction.right;

        return Direction.none;
    }

    void HandleSummonInput()
    {
        // Take input
        Direction inputDirection = GetArrowInputDirection();

        if (sequence.Count >= maxSequenceLength)
        {
            Debug.Log("Current Sequence: " + string.Join(", ", sequence));
            SummonWeapon(sequence);
            sequence.Clear();
        }

        // Convert input into something they understand
        switch (inputDirection)
        {
            case Direction.up:
                sequence.Add("up");
                break;
            case Direction.down:
                sequence.Add("down");
                break;
            case Direction.left:
                sequence.Add("left");
                break;
            case Direction.right:
                sequence.Add("right");
                break;
            default:
                break;
        }

        // Debug.Log("Current Sequence: " + string.Join(", ", sequence));
    }

    void InputBuffer()
    {
        // Provide way for clean input from player
    }

    void SummonWeapon(List<string> arrowInput)
    {
        Debug.Log("Full sequence, deleting...");
        return;
        // take converted input and spawn weapon
    }
    
}
