using GameLogic;
using System;

namespace KARC
{
    public class GameCyclePresenter
    {
        private IGameplayView _gameplayView = null;
        private IGameplayModel _gameplayModel = null;
        private bool _active = false;

        public GameCyclePresenter (IGameplayModel gameplayModel, IGameplayView gameplayView)
        {
            _gameplayView = gameplayView;
            _gameplayModel = gameplayModel;

            _gameplayView.Paused += PauseGame;
        }

        private void PauseGame(object sender, EventArgs e)
        {
            if (_active) _active = false;
            else _active = true;
        }
    }
}
