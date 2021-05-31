using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KARC.Logic
{
    class Level:Scene
    {
        protected int currentTime; //Текущее время игры
        protected int period;
        bool toroidal;//Зацикливается ли карта
        protected int enemiesNum;
        protected int enemiesCur;

        public Level (int[,] _map, int _scale, List<Object> _objList, bool _toroidal) :base(_map, _scale, _objList)
        {
            toroidal = _toroidal;
            
        }

        public void initMapGen ()
        {
            
        }
    }
}
