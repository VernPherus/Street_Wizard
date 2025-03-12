using UnityEngine;

interface Interactable
{
    public delegate void OnInteractEvent();
    public event OnInteractEvent OnInteract;

}