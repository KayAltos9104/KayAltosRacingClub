using System;

namespace KARC
{
    public interface IGameplayView
    {
        event EventHandler CycleFinished; //Включается в конце каждого цикла в GameCycle, чтобы обновить модель

    }
}
