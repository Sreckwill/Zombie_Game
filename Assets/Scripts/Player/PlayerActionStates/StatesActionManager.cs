using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class StatesActionManager : MonoBehaviour
{
    // The current state of the action manager
    [HideInInspector]public BaseActionState currState;

// Instances of different states
    public ReloadState Reload = new ReloadState();
    public DefaultState Default = new DefaultState();

    public GameObject currWeapon;
    [HideInInspector] public WeaponAmmo Ammo;
    public AudioSource _audioSource;
    
    [HideInInspector] public Animator animator;
    
    public MultiAimConstraint rightHandAim;
    public TwoBoneIKConstraint LeftHandIK;
    

    void Start()
    {
        // Initialize the state to Default at the start
        SwitchState(Default);
        Ammo = currWeapon.GetComponent<WeaponAmmo>();
        _audioSource = currWeapon.GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Update the current state every frame
        currState.UpdateState(this);
        
    }

    public void SwitchState(BaseActionState state)
    {
        // Switch to the new state
        currState = state;
        // Call the EnterState method of the new state
        currState.EnterState(this);
    }

    public void WeaponReloaded()
    {
        Ammo.Reload();
        SwitchState(Default);
    }

    public void MagineOut()
    {
        _audioSource.PlayOneShot(Ammo.magineOutSound);
    }

    public void MaginIn()
    {
        _audioSource.PlayOneShot(Ammo.magineInSound);

    }

    public void SlideRelease()
    {
        _audioSource.PlayOneShot(Ammo.releaseSlideSound);


    }
}
