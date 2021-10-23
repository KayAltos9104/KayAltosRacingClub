using System;
using System.Collections.Generic;
using System.Text;

namespace KARC.ScenesTemplates
{
    class Level : Scene
    {
        public delegate void KeyBoardHandler(object sender, KeyBoardEventArgs e);
        public event KeyBoardHandler Push;

        protected int[][,] map;//[Кол-во экранов][Карта экрана]
        protected bool toroidal;
        protected (float width, float height) scale;

        public Level ()
        {
            Push += ButtonPush;
        }      

        public void PerformPush (object sender, KeyBoardEventArgs e)
        {
            Push.Invoke(sender, e);
        }
        protected virtual void ButtonPush (object sender, KeyBoardEventArgs e)
        {

        }
        public enum ObjectCode : int
        {
            empty = 0,
            player = 1,
            civilCar = 2
        }
    }
}
