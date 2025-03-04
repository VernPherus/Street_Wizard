
// TODO: Add speed reduction when crouched

using UnityEngine;

[RequireComponent(typeof(PlayerFPSController))]
public class PlayerCrouch : MonoBehaviour
{
    [Header("Crouch Params")]
    [SerializeField] float crouchHeight = 1f;
    [SerializeField] float crouchTransitionSpeed = 10f;
    //[SerializeField] float crouchSpeedMultiplier = .5f;

    private PlayerFPSController controller;
    private PlayerInputHandler playerInput;

    private Vector3 initialCameraPosition;
    float currentHeight;
    float standingHeight;

    bool isCrouching => standingHeight - currentHeight > .1f;

    private void Awake()
    {
        controller = PlayerFPSController.Instance;
        playerInput = PlayerInputHandler.Instance;
    }

    void OnEnable() => controller.OnBeforeMove += OnBeforeMove;
    void OnDisable() => controller.OnBeforeMove -= OnBeforeMove;

    private void Start()
    {
        standingHeight = currentHeight = controller.Player.height;
        initialCameraPosition = controller.PlayerCamera.transform.localPosition;
    }

    void OnBeforeMove()
    {
        bool isTryingCrouch = playerInput.CrouchValue > 0;

        float heightTarget = isTryingCrouch ? crouchHeight : standingHeight;

        if (isCrouching && !isTryingCrouch)
        {
            var castOrigin = transform.position + new Vector3(0, currentHeight / 2, 0);
            if (Physics.Raycast(castOrigin, Vector3.up, out RaycastHit hit, 0.2f))
            {
                var distanceToCeiling = hit.point.y - castOrigin.y;
                heightTarget = Mathf.Max
                (
                    currentHeight + distanceToCeiling - 0.1f,
                    crouchHeight
                );
            }
        }

        if (!Mathf.Approximately(heightTarget, currentHeight))
        {
            var crouchDelta = Time.deltaTime * crouchTransitionSpeed;
            currentHeight = Mathf.Lerp(currentHeight, heightTarget, crouchDelta);

            Vector3 halfHeightDifference = new Vector3(0, (standingHeight - heightTarget) / 2, 0);
            var newCameraPosition = initialCameraPosition - halfHeightDifference;

            controller.PlayerCamera.transform.localPosition = newCameraPosition;
            controller.Player.height = currentHeight;
        }

    }

}
