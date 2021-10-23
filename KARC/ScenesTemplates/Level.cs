using System;
using System.Collections.Generic;
using System.Text;

namespace KARC.ScenesTemplates
{
    class Level : Scene
    {
        protected int[][,] map;//[Кол-во экранов][Карта экрана]
        protected bool toroidal;
        protected (float width, float height) scale;
        public enum ObjectCode: int
        {
            empty = 0,
            player = 1,
            civilCar = 2
        }
    }
}
