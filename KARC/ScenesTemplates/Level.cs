using KARC.Controllers;
using KARC.Prefabs.Objects;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace KARC.ScenesTemplates
{
    abstract class Level : Scene
    {
        protected CarFabric fabric;

        public delegate void KeyBoardHandler(object sender, KeyBoardEventArgs e);
        public event KeyBoardHandler Push;

        protected int[][,] map;//[Кол-во экранов][Карта экрана]
        protected int[] background;
        protected bool toroidal;
        protected (float width, float height) scale;
        public Level ()
        {
            Push += ButtonPush;            
            GenerateFabric();            
        }  
        public void PerformPush (object sender, KeyBoardEventArgs e)
        {
            Push.Invoke(sender, e);
        }
        protected abstract void GenerateFabric();
        protected void InitializeMap()
        {
            for (int screen = 0; screen < map.Length; screen++)
            {
                for (int y = 0; y < map[screen].GetLength(1);y++)
                    for (int x = 0; x < map[screen].GetLength(0); x++)
                    {
                        if (map[screen][x,y]!= (int)ObjectCode.empty)
                        {
                            float xPos = x * scale.width;
                            float yPos = y * scale.height;
                            Car car = null;
                            if (map[screen][x, y] == (int)ObjectCode.civilCar)
                            {
                                car = fabric.CreateGeneral();
                            }
                            else if (map[screen][x, y] == (int)ObjectCode.player)
                            {
                                car = fabric.CreatePlayer();
                            }
                            car.ChangePlace(new Vector2(xPos, yPos));
                            AddObject(car);
                        }                        
                    }
            }
        }
        protected abstract void ButtonPush(object sender, KeyBoardEventArgs e);         
        public enum ObjectCode : int
        {
            empty = 0,
            player = 1,
            civilCar = 2
        }
    }
}
