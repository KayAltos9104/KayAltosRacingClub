using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KARC.Logic
{
    abstract class Level:Scene
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

        Random rnd = new Random();

        public Level (int[,] _map, int _scale, bool _toroidal, Dictionary<string, Texture2D> _textures, int _leftBorder, int _rightBorder) :base(_map, _scale)
        {
            toroidal = _toroidal;
            texturesDict = new Dictionary<string, Texture2D>();
            foreach (var texture in _textures)
            {
                texturesDict.Add(texture.Key, texture.Value);
            }
            enemiesNum = 12;
            leftBorder = _leftBorder;
            rightBorder = _rightBorder;
            oncomingLane[0] = 180+leftBorder-139;
            oncomingLane[1] = 410 + leftBorder-139;
            lane[0] = 430 + leftBorder-139;
            lane[1] = 660 + leftBorder-139;
            
            initMapGen();      
        }

        public override void updateScene(int _time)
        {
            base.updateScene(_time);    
        }

        protected Car CarGeneration (int _x, int _y, string _mainKey, string _crushedKey, Vector2 _speed)
        {
            Dictionary<string, Texture2D> carTexturesDict = new Dictionary<string, Texture2D>();
            carTexturesDict.Add("MainModel", texturesDict[_mainKey]);
            carTexturesDict.Add("CrushedModel", texturesDict[_crushedKey]);
            
            Car car = new Car(new Vector2(_x, _y), 0.2f, carTexturesDict, _speed, 5000, Id, "Civlil", this);
            if (_speed.Y >0)
                car.orientation = SpriteEffects.FlipVertically;
            Animation carExplosion = new Animation(texturesDict["explosion"], 128, 128, new Point(8, 8), Vector2.Zero, false);
            carExplosion.scale = 2.0f;
            car.animationDict.Add("explosion", carExplosion);

            return car;
        }

        protected virtual void initMapGen ()
        {
           
        }
    }
}
