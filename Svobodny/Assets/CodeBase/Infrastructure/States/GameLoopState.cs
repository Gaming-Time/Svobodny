using CodeBase.Infrastructure.Services.Factories.GameFactory;
using CodeBase.Infrastructure.Services.WindowService;

namespace CodeBase.Infrastructure.States
{
    public class GameLoopState : IState
    {
        private readonly IGameFactory _gameFactory;
        private readonly IWindowService _windowService;

        public GameLoopState(IGameFactory gameFactory, IWindowService windowService)
        {
            _gameFactory = gameFactory;
            _windowService = windowService;
        }

        public void Enter()
        {
            
        }

        public void Exit()
        {
            _gameFactory.Cleanup();
            _windowService.Cleanup();
        }
    }
}
