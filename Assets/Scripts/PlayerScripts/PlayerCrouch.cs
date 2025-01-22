using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouch : MonoBehaviour
{
    [Header("Crouch Params")]
    [SerializeField] float crouchHeight = 1f;
    float standingHeight;

    
    private PlayerFPSController controller;
    private PlayerInputHandler playerInput;
    private CharacterController player;
    
    private void Awake() {
        controller = GetComponent<PlayerFPSController>();
        playerInput = PlayerInputHandler.Instance;
    }

    private void Start() {
        standingHeight = controller.Player.height;
    }

    


}
