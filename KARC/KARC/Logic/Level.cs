using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KARC.Logic
{
    class Level:Scene
    {
        Dictionary<string, Texture2D> texturesDict;//Словарь картинок, которые будут использоваться на уровне

        protected int currentTime; //Текущее время игры
        protected int period;
        bool toroidal;//Зацикливается ли карта
        protected int enemiesNum;
        protected int enemiesCur;

        public int leftBorder { get; }//Левый бортик трассы
        public int rightBorder { get; }//Правый бортик трассы
        private int[] oncomingLane = new int[2];
        private int[] lane = new int[2];


        public int EnemiesCur
        {
            set
            {
                if (value < 0)
                    enemiesCur = 0;
                else
                    enemiesCur = value;
            }
            get
            {
                return enemiesCur;
            }
        }

        //protected List<Rectangle> tileMap;
        int tileScale;

        Random rnd = new Random();

        public Level (int[,] _map, int _scale, List<Object> _objList, bool _toroidal, Dictionary<string, Texture2D> _textures) :base(_map, _scale, _objList)
        {
            toroidal = _toroidal;
            texturesDict = new Dictionary<string, Texture2D>();
            foreach (var texture in _textures)
            {
                texturesDict.Add(texture.Key, texture.Value);
            }
            enemiesNum = 15;
            oncomingLane[0] = 180;
            oncomingLane[1] = 410;
            lane[0] = 430;
            lane[1] = 660;
            leftBorder = 139;
            rightBorder = 702;
            initMapGen();      
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
                        if (obj.Value.GetType() == typeof(BackGround))
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
                        if (obj.Value.GetType() == typeof(BackGround))
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
                    EnemiesCur--;
                    
                }

                for (int i = EnemiesCur; i < enemiesNum; i++)
                {
                    int oncoming = rnd.Next(0, 2);
                    int left = 0;
                    int right = 0;
                    Vector2 speed = Vector2.Zero;
                    if (oncoming == 1)
                    {
                        left = oncomingLane[0];
                        right = oncomingLane[1];
                        speed = new Vector2(0, rnd.Next(5, 21));
                    }
                    else
                    {
                        left = lane[0];
                        right = lane[1];
                        speed = new Vector2(0, rnd.Next(-20, -5));
                    }

                    int seed = rnd.Next(2, 9);
                    string carMainKey = "MainModel" + seed;
                    string carCrushedKey = "CrushedModel" + seed;
                    int x = rnd.Next(left, right - texturesDict[carMainKey].Width);
                    int y = rnd.Next(-(map.GetLength(1) - 1) * scale, -(map.GetLength(1) - 4) * scale);

                    Car car = CarGeneration(x, y, carMainKey, carCrushedKey, speed);

                    objectList.Add(Id, car);
                    Id++;
                    EnemiesCur++;
                }
            }
            
           
        }

        private Car CarGeneration (int _x, int _y, string _mainKey, string _crushedKey, Vector2 _speed)
        {
            Dictionary<string, Texture2D> carTexturesDict = new Dictionary<string, Texture2D>();
            carTexturesDict.Add("MainModel", texturesDict[_mainKey]);
            carTexturesDict.Add("CrushedModel", texturesDict[_crushedKey]);
            
            Car car = new Car(new Vector2(_x, _y), 0.2f, carTexturesDict, _speed, 5000, Id);
            if (_speed.Y >0)
                car.orientation = SpriteEffects.FlipVertically;
            Animation carExplosion = new Animation(texturesDict["explosion"], 128, 128, new Point(8, 8), Vector2.Zero, false);
            carExplosion.scale = 2.0f;
            car.animationDict.Add("explosion", carExplosion);

            return car;
        }

        public void initMapGen ()
        {
            

            for (int i = EnemiesCur; i < enemiesNum; i++)
            {
                int oncoming = rnd.Next(0, 2);
                int left = 0;
                int right = 0;
                Vector2 speed = Vector2.Zero;
                if (oncoming == 1)
                {
                    left = oncomingLane[0];
                    right = oncomingLane[1];
                    speed = new Vector2(0, rnd.Next(5, 21));
                }
                else
                {
                    left = lane[0];
                    right = lane[1];
                    speed = new Vector2(0, rnd.Next(-20, -5));
                }                 
                              
                int seed = rnd.Next(2, 9);
                string carMainKey = "MainModel" + seed;
                string carCrushedKey = "CrushedModel" + seed;
                int x = rnd.Next(left, right - texturesDict[carMainKey].Width);
                int y = rnd.Next(-(map.GetLength(1) - 1) * scale, -(map.GetLength(1) - 4) * scale);

                Car car = CarGeneration(x, y, carMainKey, carCrushedKey, speed);
                       
                objectList.Add(Id,car);
                Id++;
                EnemiesCur++;
            }
        }
    }
}
