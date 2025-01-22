using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashScript : MonoBehaviour
{
    [Header("Dash Params")]
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashDuration;
    [SerializeField] private float dashCooldown;
    [SerializeField] private int dashThreshold;

    private PlayerFPSController controller;

    private void Start()
    {
        controller = GetComponent<PlayerFPSController>();
    }

    public void PlayerDash()
    {

    }

    IEnumerator Dash()
    {
        float startTime = Time.time;
        Debug.Log("Player Dash");

        while (Time.time < startTime + dashDuration)
        {

            yield return null;
        }
    }
}
