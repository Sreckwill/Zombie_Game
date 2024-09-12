

using UnityEngine;

public class WalkState : MovementBaseState
{
    public override void EnterState(MovementStateManager movementStateManager)
    {
        movementStateManager.animator.SetBool("Walking",true);
    }

    public override void UpdateState(MovementStateManager movementStateManager)
    {
        if(Input.GetKey(KeyCode.LeftShift))ExitState(movementStateManager,movementStateManager.Run);
        else if(Input.GetKeyDown(KeyCode.C))ExitState(movementStateManager,movementStateManager.Crouch);
        else if(movementStateManager.dir.magnitude<0.1f)ExitState(movementStateManager,movementStateManager.Idle);

        if (movementStateManager.vInput < 0) movementStateManager.currMoveSpeed = movementStateManager.walkBackSpeed;
        else movementStateManager.currMoveSpeed = movementStateManager.walkSpeed;
    }

    void ExitState(MovementStateManager movementStateManager,MovementBaseState state)
    {
        movementStateManager.animator.SetBool("Walking",false);
        movementStateManager.SwitchState(state);
    }
}
