using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLogic
{
    public interface IGameplayView
    {
        event EventHandler<ControlsEventArgs> ControlButtonPressed;
        event EventHandler Paused;
        event EventHandler<PlayerUpdateArgs> PlayerUpdated;
        void Update(GameTime gameTime);
        void Draw(GameTime gameTime);
    }

    public class PlayerUpdateArgs:EventArgs
    {
    }

    public class ControlsEventArgs : EventArgs
    {
        public Controls Action { get; set; }
        public enum Controls : byte
        {
            forward,
            backward,
            right,
            left
        }
    }
}
