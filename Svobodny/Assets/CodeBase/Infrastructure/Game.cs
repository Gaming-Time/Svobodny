namespace CodeBase.Infrastructure
{
    public class Game
    {
        public GameStateMachine StateMachine { get; private set; }

        public Game(LoadingCurtain curtain)
        {
            StateMachine = new GameStateMachine(curtain);
        }
    }
}