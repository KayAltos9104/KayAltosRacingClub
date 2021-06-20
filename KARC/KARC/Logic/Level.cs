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
            enemiesNum = 10;
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
                    EnemiesCur--;
                    
                }

                for (int i = EnemiesCur; i < enemiesNum; i++)
                {
                    int x = rnd.Next(120, 630);
                    int y = rnd.Next(-(map.GetLength(1) - 1) * scale, -(map.GetLength(1) - 4) * scale);

                    int seed = rnd.Next(2, 9);
                    string carMainKey = "MainModel" + seed;
                    string carCrushedKey = "CrushedModel" + seed;
                    Vector2 speed = Vector2.Zero;
                    if (x < 350)
                        speed = new Vector2(0, rnd.Next(0, 21));
                    else
                        speed = new Vector2(0, rnd.Next(-20, 0));

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
            
            Car car = new Car(new Vector2(_x, _y), 0.2f, carTexturesDict, Id, _speed, 5000);
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
                int x = rnd.Next(120, 630);
                int y = rnd.Next(-(map.GetLength(1) - 1)*scale, -(map.GetLength(1) - 4)*scale);
                //int y = rnd.Next(-10000, -5000);
                int seed = rnd.Next(2, 9);
                string carMainKey = "MainModel" + seed;
                string carCrushedKey = "CrushedModel" + seed;
                Vector2 speed = Vector2.Zero;
                if (x<350)
                    speed = new Vector2(0, rnd.Next(0,21));
                else
                    speed = new Vector2(0, rnd.Next(-20, 0));
                Car car = CarGeneration(x, y, carMainKey, carCrushedKey, speed);
                       
                objectList.Add(Id,car);
                Id++;
                EnemiesCur++;
            }
        }
    }
}
