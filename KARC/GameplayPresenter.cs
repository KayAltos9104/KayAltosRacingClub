using System;

namespace KARC
{
    public class GameplayPresenter
    {
        private IGameplayView _gameplayView = null;
        private IGameplayModel _gameplayModel = null;

        public GameplayPresenter (IGameplayView gameplayView, IGameplayModel gameplayModel)
        {
            _gameplayView = gameplayView;
            _gameplayModel = gameplayModel;

            _gameplayView.CycleFinished += ViewModelUpdate;
            _gameplayModel.Updated += ModelViewUpdate;
        }

        private void ModelViewUpdate(object sender, GameplayEventArgs e)
        {
            //Будет только передавать параметры, потому что сам цикл там автоматом крутится
        }


        private void ViewModelUpdate(object sender, EventArgs e)
        {
            _gameplayModel.Update();
        }
    }
}
