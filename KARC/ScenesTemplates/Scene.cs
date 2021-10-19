using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace KARC.ScenesTemplates
{
    abstract class Scene
    {
        private Keys[] _pushedButtons;//Нажатые клавиши в сцене
        
        public virtual void Update()
        {
            _pushedButtons = Keyboard.GetState().GetPressedKeys();

        }

        //private Keys[] GetPressedButtons()
        //{
        //    if (_pushedButtons != null)
        //        return (Keys[])_pushedButtons.Clone();
        //    else
        //        return null;
        //}
    }
}
