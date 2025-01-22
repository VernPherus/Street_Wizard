using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum PlayerStates
{
    walking, 
    sprinting,
    dashing,
    jumping, 
    crouching
    
}

public class Player_StateManager : MonoBehaviour
{
    [Header("Player State")]
    [SerializeField] private PlayerStates playerState;

    
}
