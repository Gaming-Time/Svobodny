using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.AssetProvider;
using CodeBase.Infrastructure.Services.ButtonMediator;
using CodeBase.Infrastructure.Services.Factories.EnemyFactory;
using CodeBase.Infrastructure.Services.Factories.GameFactory;
using CodeBase.Infrastructure.Services.Factories.NpcFactory;
using CodeBase.Infrastructure.Services.Factories.UIFactory;
using CodeBase.Infrastructure.Services.Factories.UsableObjectFactory;
using CodeBase.Infrastructure.Services.Input;
using CodeBase.Infrastructure.Services.Mediator;
using CodeBase.Infrastructure.Services.Progress;
using CodeBase.Infrastructure.Services.StaticData;
using CodeBase.Infrastructure.Services.WindowService;

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
            _services.RegisterSingle<INpcFactory>(new NpcFactory(_services.Single<IAssets>()));
            _services.RegisterSingle<IEnemyFactory>(new EnemyFactory(_services.Single<IAssets>()));
            _services.RegisterSingle<IUsableObjectFactory>(new UsableObjectFactory(_services.Single<IAssets>()));
            _services.RegisterSingle<IButtonMediator>(new ButtonMediator());
            _services.RegisterSingle<IUIFactory>(new UIFactory(_services.Single<IAssets>(),
                _services.Single<IButtonMediator>()));
            _services.RegisterSingle<IWindowService>(new WindowService(_services.Single<IUIFactory>()));
            _services.RegisterSingle<IMediator>(new Mediator(_services.Single<IButtonMediator>(),
                _services.Single<IWindowService>(), _sceneLoader, _stateMachine));
            _services.RegisterSingle<IGameFactory>(new GameFactory(_services.Single<IAssets>(),
                _services.Single<IEnemyFactory>(), _services.Single<INpcFactory>(),
                _services.Single<IInputService>(), _services.Single<IStaticDataService>(),
                _services.Single<IUsableObjectFactory>(), _services.Single<IWindowService>()));
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