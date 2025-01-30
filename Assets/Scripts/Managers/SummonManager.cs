// INFO:
/**
* This script handles:
* Summon Mechanics
* Weapon summoning
*/
using System.Collections.Generic;
using UnityEngine;


public class SummonManager : MonoBehaviour
{

    private List<string> sequence;

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


    void HandleSummonInput()
    {

    }

    void InputBuffer()
    {

    }

    void SummonWeapon()
    {

    }
}
