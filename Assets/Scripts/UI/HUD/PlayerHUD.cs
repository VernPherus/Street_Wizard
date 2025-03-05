using TMPro;
using UnityEngine;
using Managers;

[DisallowMultipleComponent]
public class PlayerHUD : MonoBehaviour
{
    [SerializeField]
    private WeaponManager weaponManager;
    private TextMeshProUGUI ammoText;
    private TextMeshProUGUI dashText;
    [SerializeField]
    private PlayerFPSController playerFPSController;

    private void Awake()
    {

        ammoText = GetComponentInChildren<TextMeshProUGUI>();

        // GameObject dashCounter = GameObject.Find("Dash");
        // dashText = dashCounter.GetComponent<TextMeshProUGUI>();

    }

    private void Update()
    {
        ammoText.SetText($"Ammo: {weaponManager.ActiveWeapon.AmmoConfig.CurrentAmmo}");
        // dashText.SetText($"Dash: {playerFPSController.GetDashNumber()}");
    }
}
