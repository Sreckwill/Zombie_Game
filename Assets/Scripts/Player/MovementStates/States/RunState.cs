
using UnityEngine;

public class RunState : MovementBaseState
{
    public override void EnterState(MovementStateManager movementStateManager)
    {
        movementStateManager.animator.SetBool("Running",true);
    }

    public override void UpdateState(MovementStateManager movementStateManager)
    {
      if(Input.GetKey(KeyCode.LeftShift))ExitState(movementStateManager,movementStateManager.Walk);
       else if(movementStateManager.dir.magnitude<0.1f)ExitState(movementStateManager,movementStateManager.Idle);
      
      
      if (movementStateManager.vInput < 0) movementStateManager.currMoveSpeed = movementStateManager.runBackSpeed;
      else movementStateManager.currMoveSpeed = movementStateManager.runSpeed;
    }

    void ExitState(MovementStateManager movementStateManager, MovementBaseState state)
    {
        movementStateManager.animator.SetBool("Running",false);
        movementStateManager.SwitchState(state);
    }
}
