using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WeaponsScripts
{
    public class WeaponSwayNBob : MonoBehaviour
    {
        public PlayerFPSController playerFPSController;

        [Header("Sway")]
        public float step = 0.01f;
        public float maxStepDistance = 0.06f;
        Vector3 swayPos;

        [Header("Sway Rotation")]
        public float rotationStep = 4f;
        public float maxRotationStep = 5f;
        Vector3 swayEulerRot;

        public float smooth = 10f;
        float smoothRot = 12f;

        [Header("Bobbing")]
        public float speedCurve;
        float curveSin { get => Mathf.Sin(speedCurve); }
        float curveCos { get => Mathf.Cos(speedCurve); }

        public Vector3 travelLimit = Vector3.one * 0.025f;
        public Vector3 bobLimit = Vector3.one * 0.01f;
        Vector3 bobPosition;

        public float bobExaggeration;

        [Header("Bob Rotation")]
        public Vector3 multiplier;
        Vector3 bobEulerRotation;

        Vector2 walkInput;
        // Vector2 lookInput;

        private void Update()
        {
            CompositePositionRotation();
        }

        // void GetInput()
        // {
        //     walkInput.x = Input.GetAxis("Horizontal");
        //     walkInput.y = Input.GetAxis("Vertical");
        //     walkInput = walkInput.normalized;

        //     lookInput.x = Input.GetAxis("Mouse X");
        //     lookInput.y = Input.GetAxis("Mouse Y");
        // }


        public void Sway(Vector2 LookInput)
        {
            Vector3 invertLook = LookInput * -step;
            invertLook.x = Mathf.Clamp(invertLook.x, -maxStepDistance, maxStepDistance);
            invertLook.y = Mathf.Clamp(invertLook.y, -maxStepDistance, maxStepDistance);

            swayPos = invertLook;
        }

        public void SwayRotation(Vector2 LookInput)
        {
            Vector2 invertLook = LookInput * -rotationStep;
            invertLook.x = Mathf.Clamp(invertLook.x, -maxRotationStep, maxRotationStep);
            invertLook.y = Mathf.Clamp(invertLook.y, -maxRotationStep, maxRotationStep);
            swayEulerRot = new Vector3(invertLook.y, invertLook.x, invertLook.x);
        }

        public void CompositePositionRotation()
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, swayPos + bobPosition, Time.deltaTime * smooth);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(swayEulerRot) * Quaternion.Euler(bobEulerRotation), Time.deltaTime * smoothRot);
        }

        public void BobOffset(Vector2 WalkInput)
        {
            speedCurve += Time.deltaTime * (playerFPSController.Player.isGrounded ? (Input.GetAxis("Horizontal") + Input.GetAxis("Vertical")) * bobExaggeration : 1f) + 0.01f;

            bobPosition.x = (curveCos * bobLimit.x * (playerFPSController.Player.isGrounded ? 1 : 0)) - (walkInput.x * travelLimit.x);
            bobPosition.y = (curveSin * bobLimit.y) - (Input.GetAxis("Vertical") * travelLimit.y);
            bobPosition.z = -(WalkInput.y * travelLimit.z);
        }

        public void BobRotation(Vector2 WalkInput)
        {
            bobEulerRotation.x = (WalkInput != Vector2.zero ? multiplier.x * (Mathf.Sin(2 * speedCurve)) : multiplier.x * (Mathf.Sin(2 * speedCurve) / 2));
            bobEulerRotation.y = (WalkInput != Vector2.zero ? multiplier.y * curveCos : 0);
            bobEulerRotation.z = (WalkInput != Vector2.zero ? multiplier.z * curveCos * WalkInput.x : 0);
        }

    }

}

