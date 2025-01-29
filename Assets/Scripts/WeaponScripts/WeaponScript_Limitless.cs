using UnityEngine;

public class WeaponScriptLimitless : MonoBehaviour
{

    [Header("Gun Config")]
    [SerializeField] public float damage = 10f;
    [SerializeField] public float range = 100f;

    [SerializeField] public float fireRate = 1f;

    private float nextTimeToFire = 0f;

    private PlayerInputHandler playerInput;
    private PlayerFPSController playerController;


    private Camera mainCam;

    private void Awake()
    {
        playerController = PlayerFPSController.Instance;
        playerInput = PlayerInputHandler.Instance;
        mainCam = playerController.PlayerCamera;
    }

    private void Update()
    {
        if (playerInput.FireTriggered && Time.time >= nextTimeToFire)
        {   
            nextTimeToFire = Time.time + 1f/fireRate;
            Shoot();
            Debug.Log(nextTimeToFire);
        }

    }

    void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(mainCam.transform.position, mainCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            DummyTarget target = hit.transform.GetComponent<DummyTarget>();
            if (target != null){
                target.TakeDamage(damage);
            }
        }
    }
}