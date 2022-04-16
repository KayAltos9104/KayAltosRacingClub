using System;

namespace KARC
{
    public interface IGameplayModel
    {
        event EventHandler<GameplayEventArgs> Updated;
        void Update();

    }

    public class GameplayEventArgs
    {

    }
}