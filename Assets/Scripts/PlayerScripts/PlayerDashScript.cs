using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashScript : MonoBehaviour
{
    [Header("Dash Params")]
    [SerializeField] private float dashSpeed = 10;
    [SerializeField] private float dashDuration = 0.25f;
    [SerializeField] private float dashCooldown = 10.0f;
    [SerializeField] private int dashThreshold = 3;

    private PlayerFPSController controller;

    private void Start()
    {
        controller = GetComponent<PlayerFPSController>();
    }

    public void PlayerDash()
    {
        StartCoroutine(Dash());
    }

    IEnumerator Dash()
    {
        float startTime = Time.time;
        Debug.Log("Player Dash");

        while (Time.time < startTime + dashDuration)
        {
            controller.Player.Move(dashSpeed * Time.deltaTime * controller.CurrentMovement);

            yield return null;
        }
    }
}
