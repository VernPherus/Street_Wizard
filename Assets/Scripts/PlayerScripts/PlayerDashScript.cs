// TODO: Buff dash speed when not moving

using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PlayerFPSController))]
public class PlayerDashScript : MonoBehaviour
{
    [Header("Dash Params")]
    [SerializeField] private float dashSpeed = 8;
    [SerializeField] private float dashDuration = 0.25f;
    [SerializeField] private float dashCooldown = 10f;
    [SerializeField] private int maxDashes = 3;

    [SerializeField] private int currentDashes;
    private bool isDashing = false;
    private bool isRegenerating = false;

    private PlayerFPSController controller;
    private PlayerInputHandler inputHandler;

    private void Start()
    {
        controller = PlayerFPSController.Instance;
        inputHandler = PlayerInputHandler.Instance;
        currentDashes = maxDashes;

    }

    public void HandleDash(Vector3 direction)
    {
        if (currentDashes > 0)
        {
            StartCoroutine(Dash(direction));
        }
    }

    IEnumerator Dash(Vector3 direction)
    {
        if (isDashing || currentDashes <= 0) yield break;

        isDashing = true;
        currentDashes--;

        Debug.Log("Player Dashing");

        if (!isRegenerating)
        {
            StartCoroutine(dashRegenerate());
        }

        float startTime = Time.time;

        Vector3 dashVelocity = direction.normalized * dashSpeed;

        Debug.Log($"{dashVelocity}");

        while (Time.time < startTime + dashDuration)
        {
            controller.Player.Move(dashVelocity * Time.deltaTime);
            yield return null;
        }

        isDashing = false;
    }

    IEnumerator dashRegenerate()
    {
        if (isRegenerating) yield break;

        isRegenerating = true;

        while (currentDashes < maxDashes)
        {
            yield return new WaitForSeconds(dashCooldown);
            currentDashes++;
            Debug.Log($"Dash regenerated. Current Dashes: {currentDashes}");
        }

        isRegenerating = false;
    }

    public int GetDashNumber(){
        return currentDashes;
    }
}
