
using UnityEngine;

public class DefaultState : BaseActionState
{
    public override void EnterState(StatesActionManager actionStateManager)
    {
        actionStateManager.rightHandAim.weight = 1;
        actionStateManager.LeftHandIK.weight = 1;
    }

    public override void UpdateState(StatesActionManager actionStateManager)
    {
        actionStateManager.rightHandAim.weight = Mathf.Lerp(actionStateManager.rightHandAim.weight, 1, 10 * Time.deltaTime);
        actionStateManager.LeftHandIK.weight = Mathf.Lerp(actionStateManager.LeftHandIK.weight, 1, 10 * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.R) && CanReload(actionStateManager))
        {
            actionStateManager.SwitchState(actionStateManager.Reload);
        }
    }

    bool CanReload(StatesActionManager actionStateManager)
    {
        if (actionStateManager.Ammo.currAmmo == actionStateManager.Ammo.clipSize) return false;
        else if (actionStateManager.Ammo.extraAmmo == 0)
        {
            return false;
        }

        return true;


    }
}
