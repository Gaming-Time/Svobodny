using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.AssetProvider;

namespace CodeBase.Infrastructure.States
{
    public class BootstrapState : IState
    {
        private const string Initial = "Initial";
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly AllServices _services;

        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, AllServices allServices)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _services = allServices;

            RegisterServices();
            ConfigFactories();
        }

        public void Exit()
        {
            
        }

        public void Enter()
        {
            _sceneLoader.Load(Initial, () => _stateMachine.Enter<LoadProgressState>());
        }
        
        private void RegisterServices()
        {
            _services.RegisterSingle(_stateMachine);
            _services.RegisterSingle<IAssets>(new AssetProvider());
            
        }
        private void ConfigFactories()
        {
            
        }
    }
}