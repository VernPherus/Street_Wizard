using UnityEngine;
using System.Collections;

public class ElevatorScript : MonoBehaviour
{
    [SerializeField] private GameObject ElevatorBody;
    [SerializeField] private GameObject door;
    [SerializeField] private Transform entityCollider;

    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float size;
    [SerializeField] private float speed = 0.5f;
    [SerializeField] private float doorCloseDelay = 2.0f;

    [SerializeField] private Vector3 endPoint;

    private bool playerInsideElevator;
    private bool isMoving;
    private Vector3 targetPosition;
    private Vector3 startPoint;

    private void Start()
    {
        startPoint = ElevatorBody.transform.position;
        targetPosition = endPoint;
        OpenDoor();
    }

    private void Update()
    {
        CheckPlayerPresence();

        if (playerInsideElevator && !isMoving)
        {
            StartCoroutine(CloseDoorWithDelay());
        }
    }

    private void CheckPlayerPresence()
    {
        playerInsideElevator = Physics.CheckBox(entityCollider.position, Vector3.one * size / 2, Quaternion.identity, playerLayer);
    }

    private IEnumerator CloseDoorWithDelay()
    {
        yield return new WaitForSeconds(doorCloseDelay);
        CloseDoor();
        MoveElevator();
    }

    private void MoveElevator()
    {
        isMoving = true;
        StartCoroutine(MoveElevatorCoroutine());
    }

    private IEnumerator MoveElevatorCoroutine()
    {
        float distance = Vector3.Distance(startPoint, endPoint);
        float remainingDistance = distance;
        while (remainingDistance > 0)
        {
            ElevatorBody.transform.position = Vector3.Lerp(startPoint, endPoint, Mathf.Clamp01(1 - (remainingDistance / distance)));

            remainingDistance -= speed * Time.deltaTime;

            yield return null;
        }

        ElevatorBody.transform.position = targetPosition;
        isMoving = false;

        OpenDoor();
        SwapTargetPosition();
    }

    private void CloseDoor()
    {
        door.SetActive(true);
    }

    private void OpenDoor()
    {
        door.SetActive(false);
    }

    private void SwapTargetPosition()
    {
        targetPosition = (targetPosition == endPoint) ? startPoint : endPoint;
    }
}
