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
    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        aimStateManager = GetComponentInParent<AimStateManager>();
        fireRateTimer = fireRate;
    }

    // Update is called once per frame
    void Update()
    {
        if(ShouldFire())Fire();
    }

    bool ShouldFire()
    {
        fireRateTimer += Time.deltaTime;
        if (fireRateTimer < fireRate) return false;
        if (semiAuto & Input.GetKeyDown(KeyCode.Mouse0)) return true;
        if (!semiAuto & Input.GetKey(KeyCode.Mouse0)) return true;
        return false;

    }

    // ReSharper disable Unity.PerformanceAnalysis
    void Fire()
    {
        fireRateTimer = 0;
        barrelPos.LookAt(aimStateManager.aimPos);
        audioSource.PlayOneShot(gunShot);
        for (int i = 0; i < bulletPerShot; i++)
        {
            GameObject currentBullet = Instantiate(bullet, barrelPos.position, barrelPos.rotation);
            Rigidbody rb = currentBullet.GetComponent<Rigidbody>();
            rb.AddForce(barrelPos.forward*bullectVelcoity,ForceMode.Impulse);
        }
        Debug.Log("Fire");
    }
}
