using UnityEngine;

public class IdleState : MovementBaseState
{
    public override void EnterState(MovementStateManager movementStateManager)
    {
        // Implementation of EnterState
    }

    public override void UpdateState(MovementStateManager movementStateManager)
    {
        if (movementStateManager.dir.magnitude > 0.1f)
        {
            if (Input.GetKey(KeyCode.LeftShift)) movementStateManager.SwitchState(movementStateManager.Run);
            else movementStateManager.SwitchState(movementStateManager.Walk);
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            movementStateManager.SwitchState(movementStateManager.Crouch);
        }
    }
}