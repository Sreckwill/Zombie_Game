using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WeaponManager : MonoBehaviour
{
    [Header("Fire Rate")]
    [SerializeField] private float fireRate;
    public float fireRateTimer;
    [SerializeField] private bool semiAuto;

    [Header("Bullet Properties")] 
    [SerializeField] private GameObject bullet;

    [SerializeField] private Transform barrelPos;
    [SerializeField] private float bullectVelcoity;
    [SerializeField] private int bulletPerShot;
    AimStateManager aimStateManager;

    [SerializeField] private AudioClip gunShot;
    private AudioSource audioSource;
    private WeaponAmmo weaponAmmo;
    private ActionStateManager _statesActionManager;

    private WeaponRecoil recoil;

    [SerializeField] private Light MuzzleFlashLight;
    [SerializeField] ParticleSystem MuzzleFlashParticles;
    [SerializeField]private float lightIntensity;
    [SerializeField] private float lightReturnSpeed=20;
    
    void Start()
    {
        recoil = GetComponent<WeaponRecoil>();
        audioSource = GetComponent<AudioSource>();
        aimStateManager = GetComponentInParent<AimStateManager>();
        weaponAmmo = GetComponent<WeaponAmmo>();
        _statesActionManager = GetComponentInParent<ActionStateManager>();
        MuzzleFlashLight = GetComponentInChildren<Light>();
        lightIntensity = MuzzleFlashLight.intensity;
        MuzzleFlashLight.intensity = 0;
        MuzzleFlashParticles = GetComponentInChildren<ParticleSystem>();
        
        fireRateTimer = fireRate;
    }

    // Update is called once per frame
    void Update()
    {
        if(ShouldFire())Fire();
        Debug.Log(weaponAmmo.currAmmo);
        MuzzleFlashLight.intensity = Mathf.Lerp(MuzzleFlashLight.intensity, 0, lightReturnSpeed * Time.deltaTime);
    }

    bool ShouldFire()
    {
        fireRateTimer += Time.deltaTime;
        if (fireRateTimer < fireRate) return false;
        if (weaponAmmo.currAmmo == 0) return false;
        if (_statesActionManager.currState == _statesActionManager.Reload) return false;
        if (semiAuto & Input.GetKeyDown(KeyCode.Mouse0)) return true;
        if (!semiAuto & Input.GetKey(KeyCode.Mouse0)) return true;
        return false;

    }

   
    void Fire()
    {
        fireRateTimer = 0;
        barrelPos.LookAt(aimStateManager.aimPos);
        audioSource.PlayOneShot(gunShot);
        recoil.TriggerRecoil();
        
        weaponAmmo.currAmmo--;
        for (int i = 0; i < bulletPerShot; i++)
        {
            GameObject currentBullet = Instantiate(bullet, barrelPos.position, barrelPos.rotation);
            Rigidbody rb = currentBullet.GetComponent<Rigidbody>();
            rb.AddForce(barrelPos.forward*bullectVelcoity,ForceMode.Impulse);
        }
        TriggerMuzzleFlash();
        
    }

    void TriggerMuzzleFlash()
    {
        MuzzleFlashParticles.Play();
        MuzzleFlashLight.intensity = lightIntensity;
    }
}

