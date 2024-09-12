

using UnityEngine;

public class CrouchState : MovementBaseState
{
   

    public override void EnterState(MovementStateManager movementStateManager)
    {
        movementStateManager.animator.SetBool("Crouching",true);
    }

    public override void UpdateState(MovementStateManager movementStateManager)
    {
        if(Input.GetKey(KeyCode.LeftShift))ExitState(movementStateManager,movementStateManager.Run);
        if (Input.GetKeyDown(KeyCode.C))
        {
            if(movementStateManager.dir.magnitude<0.1f)ExitState(movementStateManager,movementStateManager.Idle);
            else ExitState(movementStateManager,movementStateManager.Walk);
        }
        if (movementStateManager.vInput < 0) movementStateManager.currMoveSpeed = movementStateManager.crouchBackSpeed;
        else movementStateManager.currMoveSpeed = movementStateManager.crouchSpeed;
       
    }

    void ExitState(MovementStateManager movementStateManager,MovementBaseState state)
    {
        movementStateManager.animator.SetBool("Crouching",false);
        movementStateManager.SwitchState(state);
    }
}
