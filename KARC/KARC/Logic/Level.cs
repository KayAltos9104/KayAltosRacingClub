using Microsoft.Xna.Framework;
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
        //protected List<Rectangle> tileMap;
        int tileScale;

        public Level (int[,] _map, int _scale, List<Object> _objList, bool _toroidal) :base(_map, _scale, _objList)
        {
            toroidal = _toroidal;
            //tileScale = _scale / 2;//TODO: сделать в настройки
            //for (int y = 0; y < _map.GetLength(1);y++)
            //    for (int x = 0; x < _map.GetLength(0);y++)
            //    {
            //        for (int tileY=0;tileY < 2; tileY++)
            //            for (int tileX=0; tileX <2;tileX++)
            //            {
            //                Rectangle tile = new Rectangle(tileX * tileScale + x, tileY * tileScale + y, tileScale, tileScale);
            //                tileMap.Add(tile);
            //            }
            //    }             
        }

        public override void updateScene(int _time)
        {
            base.updateScene(_time);
            if (toroidal)
            {
                List<int> delObj = new List<int>();
                foreach (var obj in objectList)
                {
                    if (obj.Value.pos.Y>2*scale)
                    {
                        if (obj.Value.type == objType.background)
                        {
                            obj.Value.pos.Y = -(map.GetLength(1) - 3) * scale;
                        }
                        else
                        {
                            delObj.Add(obj.Key);
                        }
                    }

                    if (obj.Value.pos.Y < -(map.GetLength(1)+1) * scale)
                    {
                        if (obj.Value.type == objType.background)
                        {
                            obj.Value.pos.Y = 0;
                        }
                        else
                        {
                            delObj.Add(obj.Key);
                        }
                    }
                }

                foreach (var del in delObj)
                {
                    objectList.Remove(del);
                }
            }
            
           
        }

        public void initMapGen ()
        {
            
        }
    }
}
