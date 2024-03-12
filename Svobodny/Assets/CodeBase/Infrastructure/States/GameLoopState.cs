using CodeBase.Infrastructure.Services.Factories.GameFactory;

namespace CodeBase.Infrastructure.States
{
    public class GameLoopState : IState
    {
        private readonly IGameFactory _gameFactory;

        public GameLoopState(IGameFactory gameFactory)
        {
            _gameFactory = gameFactory;
        }

        public void Enter()
        {
            
        }

        public void Exit()
        {
            _gameFactory.Cleanup();
        }
    }
}
