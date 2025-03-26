using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class linedef_apartment : MonoBehaviour
{
    [SerializeField] private GameObject falseDoor;
    private void OnTriggerEnter(Collider other)
    {
        falseDoor.SetActive(false);
    }
}
