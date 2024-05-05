namespace CodeBase.Infrastructure.States
{
    public interface IUpdatableState : IState
    {
        void Update();
    }
}