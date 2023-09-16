using System;
using CodeBase.Infrastructure.States;
using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour
    {
        private Game _game;
        public LoadingCurtain CurtainPrefab;

        private void Awake()
        {
            _game = new Game(Instantiate(CurtainPrefab));
            _game.StateMachine.Enter<BootstrapState>();
            
            DontDestroyOnLoad(this);
        }

        private void OnApplicationQuit() => _game.StateMachine.ShutDown();

        public void ChangeLevel(){}
    }
}