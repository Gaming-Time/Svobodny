using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.AssetProvider;
using CodeBase.Infrastructure.Services.Factories.EnemyFactory;
using CodeBase.Infrastructure.Services.Factories.GameFactory;
using CodeBase.Infrastructure.Services.Factories.NpcFactory;
using CodeBase.Infrastructure.Services.Input;
using CodeBase.Infrastructure.Services.Progress;
using CodeBase.Infrastructure.Services.StaticData;

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
            _services.RegisterSingle<IInputService>(new DesktopInputService());
            _services.RegisterSingle(GetLoadedStaticData());
            _services.RegisterSingle<INpcFactory>(new NpcFactory(_services.GetSingle<IAssets>()));
            _services.RegisterSingle<IEnemyFactory>(new EnemyFactory(_services.GetSingle<IAssets>()));
            _services.RegisterSingle<IGameFactory>(new GameFactory(_services.GetSingle<IAssets>(),
                _services.GetSingle<IEnemyFactory>(), _services.GetSingle<INpcFactory>(), _services.GetSingle<IInputService>()));
            _services.RegisterSingle<IProgressService>(new ProgressService());
        }
        private void ConfigFactories()
        {
            
        }

        private IStaticDataService GetLoadedStaticData()
        {
            IStaticDataService staticDataService = new StaticDataService();
            staticDataService.LoadStaticData();
            return staticDataService;
        }
    }
}