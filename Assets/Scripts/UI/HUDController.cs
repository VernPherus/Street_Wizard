using UnityEngine;
using Managers;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;


public class HUDController : MonoBehaviour
{

    [SerializeField] private WeaponManager WeaponManager;
    [SerializeField] private SummonManager SummonManager;
    [SerializeField] private playerSummonInput summonInput;
    [SerializeField] private PlayerFPSController PlayerFPSController;
    [SerializeField] private PlayerStats PlayerStats;

    [SerializeField] private PlayerDashScript playerDashScript;

    [Header("UI Icons")]
    [SerializeField] private List<Sprite> RuneIcons = new();

    [Header("UI Elements")]

    [Header("Dash")]
    [SerializeField] private List<Image> DashThreshold = new();

    [Header("Weapons")]
    [SerializeField] private TextMeshProUGUI AmmoCounter = null;
    [SerializeField] private TextMeshProUGUI WeaponName = null;

    [Header("Runes")]
    [SerializeField] private GameObject SummonStats = null;
    [SerializeField] private Image RuneContainer = null;
    [SerializeField] private Image SpellSequence = null;
    [SerializeField] private TextMeshProUGUI InputSequenceTemp = null;

    [Header("Player Stats")]
    [SerializeField] private TextMeshProUGUI HealthCounter = null;
    [SerializeField] private TextMeshProUGUI ManaCounter = null;

    [SerializeField] private Image RedKey;
    [SerializeField] private Image GreenKey;

    [Header("Dialogue Box")]
    [SerializeField] private GameObject DialogueBox = null;
    [SerializeField] private TextMeshProUGUI DialogueText = null;

    private void Awake()
    {
        // WeaponManager = GetComponent<WeaponManager>();
        // SummonManager = GetComponent<SummonManager>();
        // PlayerFPSController = PlayerFPSController.Instance;

        DialogueBox.SetActive(false);
        SummonStats.SetActive(false);

        RedKey.fillAmount = 0;
        GreenKey.fillAmount = 0;

        Debug.Log("UI Setup complete.");
    }

    private void Update()
    {
        HandleWeaponStats();
        HandleDashThreshold();
        HandlePlayerStats();
        HandleInputCounter();
        HandleRuneContainer();
        HandleUnlockedKeysContainer();
    }

    // #############################################################################################################
    //* ## Dash display logic ##        
    // #############################################################################################################

    public void HandleDashThreshold()
    {
        int currentDashes = playerDashScript.GetDashNumber();

        for (int i = 0; i < DashThreshold.Count; i++)
        {
            DashThreshold[i].fillAmount = i < currentDashes ? 1f : 0f;
        }
    }

    // #############################################################################################################
    //* ## Weapon Stats logic ##        
    // #############################################################################################################

    public void HandleWeaponStats()
    {
        AmmoCounter.SetText($"{WeaponManager.ActiveWeapon.AmmoConfig.CurrentAmmo}");
        WeaponName.SetText($"{WeaponManager.ActiveWeapon.weaponName}");
    }

    // #############################################################################################################
    //* ## Rune Stats logic ##        
    // #############################################################################################################

    public void HandleRuneContainer()
    {
        if (SummonManager.RuneIsActive())
        {
            SummonStats.SetActive(true);
        }
    }

    // #############################################################################################################
    //* ## Player stats display logic ##        
    // #############################################################################################################

    public void HandlePlayerStats()
    {
        HealthCounter.SetText($"{PlayerStats.health.CurrentHealth}");
        ManaCounter.SetText($"{PlayerStats.mana.CurrentMana}");
    }

    public void HandleManaCounter()
    {
        RuneContainer.sprite = RuneIcons[0];
    }

    public void HandleInputCounter()
    {
        //InputSequenceTemp.SetText($"");
    }

    public void HandleUnlockedKeysContainer()
    {
        if (PlayerStats.hasRedKey)
        {
            RedKey.fillAmount = 1;
        }

        if (PlayerStats.hasGreenKey)
        {
            GreenKey.fillAmount = 1;
        }
    }

    // #############################################################################################################
    //* ## DialogueBox logic ##        
    // #############################################################################################################

    public void HandleDialogueBox(string Dialogue, int ActiveTime)
    {
        DialogueText.SetText(Dialogue);
        StartCoroutine(DialogueBoxTimer(ActiveTime));
    }

    private IEnumerator DialogueBoxTimer(int CountDown)
    {
        DialogueBox.SetActive(true);
        yield return new WaitForSeconds(CountDown);
        DialogueBox.SetActive(false);
    }

}
