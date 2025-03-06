using UnityEngine;
using Managers;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class HUDController : MonoBehaviour
{

    [SerializeField] private WeaponManager WeaponManager;
    [SerializeField] private SummonManager SummonManager;
    [SerializeField] private PlayerFPSController PlayerFPSController;

    [SerializeField] private PlayerDashScript playerDashScript;

    [Header("UI Elements")]
    [SerializeField] private List<Image> DashThreshold = new();
    [SerializeField] private TextMeshProUGUI AmmoCounter = null;

    private void Awake()
    {
        // WeaponManager = GetComponent<WeaponManager>();
        // SummonManager = GetComponent<SummonManager>();
        // PlayerFPSController = PlayerFPSController.Instance;

        Debug.Log("UI Setup complete.");
    }

    private void Update()
    {
        HandleAmmoCounter();
        HandleDashThreshold();
    }

    public void HandleDashThreshold()
    {
        int currentDashes = playerDashScript.GetDashNumber();

        for (int i = 0; i < DashThreshold.Count; i++)
        {
            DashThreshold[i].fillAmount = i < currentDashes ? 1f : 0f;
        }

    }

    public void HandleAmmoCounter()
    {
        AmmoCounter.SetText($"Ammo: {WeaponManager.ActiveWeapon.AmmoConfig.CurrentAmmo}");
    }

}
