
using UnityEngine;

public class DefaultState : ActionBaseState
{
    public override void EnterState(ActionStateManager actionStateManager)
    {
        actionStateManager.rightHandAim.weight = 1;
        actionStateManager.LeftHandIK.weight = 1;
    }

    public override void UpdateState(ActionStateManager actionStateManager)
    {
        actionStateManager.rightHandAim.weight = Mathf.Lerp(actionStateManager.rightHandAim.weight, 1, 10 * Time.deltaTime);
        actionStateManager.LeftHandIK.weight = Mathf.Lerp(actionStateManager.LeftHandIK.weight, 1, 10 * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.R) && CanReload(actionStateManager))
        {
            actionStateManager.SwitchState(actionStateManager.Reload);
        }
    }

    bool CanReload(ActionStateManager actionStateManager)
    {
        Debug.Log("reload ani");
        if (actionStateManager.Ammo.currAmmo == actionStateManager.Ammo.clipSize) return false;
        else if (actionStateManager.Ammo.extraAmmo == 0)
        {
            return false;
        }

        return true;


    }
}
