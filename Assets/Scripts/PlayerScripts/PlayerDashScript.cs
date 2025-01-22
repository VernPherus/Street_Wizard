// TODO: Add dash thresholding to limit player dashes
/** Since dashing pushes the player to the current direction it's facing, 
* funny things happen such as dashing while jumping results in a super jump.
* Dunno if we should keep it or nah, but it could be a fun mechanic.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerFPSController))]
public class PlayerDashScript : MonoBehaviour
{
    [Header("Dash Params")]
    [SerializeField] private float dashSpeed = 5;
    [SerializeField] private float dashDuration = 0.25f;
    [SerializeField] private float dashCooldown = 10.0f;
    [SerializeField] private int dashThreshold = 3;

    private PlayerFPSController controller;
    private PlayerInputHandler inputHandler;

    private void Start()
    {
        controller = PlayerFPSController.Instance;
        inputHandler = PlayerInputHandler.Instance;
    }

    public void HandleDash(Vector3 direction)
    {
        if (inputHandler.DashTriggered)
        {
            StartCoroutine(Dash(direction));
        }
    }

    IEnumerator Dash(Vector3 direction)
    {
        float startTime = Time.time;
        Debug.Log("Player Dashing");

        while (Time.time < startTime + dashDuration)
        {
            controller.Player.Move(dashSpeed * Time.deltaTime * direction);
            yield return null;
        }
    }
}
