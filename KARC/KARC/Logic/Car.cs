using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KARC.Logic
{
    class Car:PhysicalObject
    {
        Vector2 speed=Vector2.Zero;
        public Vector2 Speed
        {
            set
            {
                if (value.X > 40)
                    speed.X = 40;
                else if (value.X < -10)
                    speed.X = -10;
                else
                    speed.X = value.X;

                if (value.Y > 40)
                    speed.Y = 40;
                else if (value.Y < -10)
                    speed.Y = -10;
                else
                    speed.Y = value.Y;

            }
            get
            {
                return speed;
            }
        }

        public Car (Vector2 _pos, float _layer, Dictionary<string, Texture2D> _loadTextList, int _Id, int _period, Vector2 _speed, int _weight) :base(_pos, _layer, _loadTextList, _Id, _period, _weight)
        {
            Speed = _speed;
            movable = true;
            type = objType.car;
        }


    }
}
