using System;
using System.Collections.Generic;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Factories.GameFactory;
using CodeBase.Infrastructure.Services.Factories.UIFactory;
using CodeBase.Infrastructure.Services.Progress;
using CodeBase.Infrastructure.Services.StaticData;
using CodeBase.Infrastructure.Services.WindowService;
using CodeBase.Infrastructure.States;

namespace CodeBase.Infrastructure
{
    public class GameStateMachine : IService
    {
        private readonly Dictionary<Type, IExitableState> _states;
        private IExitableState _activeState;

        public GameStateMachine(SceneLoader sceneLoader, LoadingCurtain curtain, AllServices services)
        {
            _states = new Dictionary<Type, IExitableState>
            {
                [typeof(MainMenuState)] = new MainMenuState(curtain, sceneLoader, this),
                [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader, services),
                [typeof(LoadProgressState)] = new LoadProgressState(this,
                    services.Single<IStaticDataService>(), services.Single<IProgressService>()),
                [typeof(LoadLevelState)] = new LoadLevelState(this, sceneLoader, curtain,
                    services.Single<IGameFactory>(),
                    services.Single<IStaticDataService>(), services.Single<IProgressService>(),
                    services.Single<IUIFactory>()),
                [typeof(GameLoopState)] =
                    new GameLoopState(services.Single<IGameFactory>(), services.Single<IWindowService>()),
            };
        }

        public void Enter<TState>() where TState : class, IState
        {
            IState state = ChangeState<TState>();
            state.Enter();
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
        {
            TState state = ChangeState<TState>();
            state.Enter(payload);
        }

        public void ShutDown() => _activeState.Exit();

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _activeState?.Exit();
            TState state = GetState<TState>();
            _activeState = state;

            return state;
        }

        private TState GetState<TState>() where TState : class, IExitableState => _states[typeof(TState)] as TState;
    }
}