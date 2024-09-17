using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRecoil : MonoBehaviour
{
    [SerializeField] private Transform recoilFollowPos; // The transform that will follow the recoil position
    [SerializeField] private float kickBackAmount = -1; // The amount of kickback when recoil is triggered
    [SerializeField] private float kickBackSpeed = 10, returnSpeed = 20; // Speed of kickback and return to original position
    private float currRecoilPosotion, finalRecoilPosition; // Current and final recoil positions

    void Update()
    {
        // Gradually reduce the current recoil position to zero over time
        currRecoilPosotion = Mathf.Lerp(currRecoilPosotion, 0, returnSpeed * Time.deltaTime);
        // Smoothly interpolate("interpolate" refers to the process of calculating intermediate values between two points. This is typically done to create smooth transitions) the final recoil position based on the current recoil position
        finalRecoilPosition = Mathf.Lerp(finalRecoilPosition, currRecoilPosotion, kickBackSpeed * Time.deltaTime);
        // Update the local position of the recoil follow transform
        recoilFollowPos.localPosition = new Vector3(0, 0, finalRecoilPosition);
    }

// Method to trigger recoil by adding the kickback amount to the current recoil position
    public void TriggerRecoil() => currRecoilPosotion += kickBackAmount;
}
