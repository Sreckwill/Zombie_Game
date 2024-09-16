using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAmmo : MonoBehaviour
{
    public int clipSize;
    public int extraAmmo;
    [HideInInspector]public int currAmmo;

    public AudioClip magineInSound;
    public AudioClip magineOutSound;
    public AudioClip releaseSlideSound;
   
    void Start()
    {
        currAmmo = clipSize;
    }

   
    public void Reload()
    {
        // Check if there is enough extra ammo to fully reload the clip
        if (extraAmmo >= clipSize)
        {
            // Calculate the amount of ammo needed to reload the clip
            int weaponAmmoToReload = clipSize - currAmmo;
            // Subtract the reloaded ammo from the extra ammo
            extraAmmo -= weaponAmmoToReload;
            // Add the reloaded ammo to the current ammo
            currAmmo += weaponAmmoToReload;
        }
        // If there is some extra ammo but not enough to fully reload the clip
        else if (extraAmmo > 0)
        {
            // Check if the total ammo (current + extra) exceeds the clip size
            if (extraAmmo + currAmmo > clipSize)
            {
                // Calculate the leftover ammo after reloading the clip
                int leftOverWeaponAmmo = extraAmmo + currAmmo - clipSize;
                // Set the extra ammo to the leftover amount
                extraAmmo = leftOverWeaponAmmo;
                // Fill the clip to its maximum capacity
                currAmmo = clipSize;
            }
            else
            {
                // Add all the extra ammo to the current ammo
                currAmmo += extraAmmo;
                // Set extra ammo to zero as it has all been used
                extraAmmo = 0;
            }
        }
    }

}
