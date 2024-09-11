

public class WalkState : MovementBaseState
{
    public override void EnterState(MovementStateManager movementStateManager)
    {
        movementStateManager.animator.SetBool("Walking",true);
    }

    public override void UpdateState(MovementStateManager movementStateManager)
    {
        
    }
}
