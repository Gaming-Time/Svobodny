using System;
using CodeBase.Infrastructure.States;
using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        private Game _game;
        public LoadingCurtain CurtainPrefab;

        private void Awake()
        {
            _game = new Game(this,Instantiate(CurtainPrefab));
            _game.StateMachine.Enter<BootstrapState>();
            
            DontDestroyOnLoad(this);
        }

        private void OnApplicationQuit() => _game.StateMachine.ShutDown();

        public void ChangeLevel(){}
    }
}