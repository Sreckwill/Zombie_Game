
public abstract class ZombieStates 
{
    public ZombieStateMechine stateMechine;

    public ZombieStates(ZombieStateMechine stateMechine)
    {
        this.stateMechine = stateMechine;
    }

    public abstract void EnterState();
    public abstract void UpdateState();
    public abstract void ExitState();
}
