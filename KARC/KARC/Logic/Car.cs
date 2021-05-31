using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KARC.Logic
{
    class Car:PhysicalObject
    {
        Vector2 speed=Vector2.Zero;

        public int acceleration=5;
        public int maneuver = 5;

        public override Vector2 Speed
        {
            set
            {
                if (value.X > 10)
                    speed.X = 10;
                else if (value.X < -10)
                    speed.X = -10;
                else
                    speed.X = value.X;

                if (value.Y > 10)
                    speed.Y = 10;
                else if (value.Y < -40)
                    speed.Y = -40;
                else
                    speed.Y = value.Y;

            }
            get
            {
                return speed;
            }
        }

        public Car (Vector2 _pos, float _layer, Dictionary<string, Texture2D> _loadTextList, int _Id, Vector2 _speed, int _weight) :base(_pos, _layer, _loadTextList, _Id, _weight)
        {
            Speed = _speed;
            movable = true;
            type = objType.car;
            period = 10;
        }

        public override void collision(PhysicalObject _object)
        {
            if (hitBox.Intersects(_object.hitBox))
            {                
                Rectangle inter;
                Rectangle.Intersect(ref hitBox, ref _object.hitBox, out inter);
                Vector2 intersecVect = pos - _object.pos;
                
                if (pos.X + hitBox.Width / 2 - _object.pos.X - _object.hitBox.Width / 2 > 0)
                {                    
                    pos.X += _object.Speed.X;
                    _object.pos.X += Speed.X;

                }

                else if (pos.X + hitBox.Width / 2 - _object.pos.X - _object.hitBox.Width / 2 == 0)
                { }
                else
                {                   
                    pos.X -= _object.Speed.X;
                    _object.pos.X -= Speed.X;

                }


                if (pos.Y + hitBox.Height / 2 - _object.pos.Y - _object.hitBox.Height / 2 > 0)
                {
                    pos.Y += _object.Speed.Y;
                    _object.pos.Y += Speed.Y;
                }
                else if (pos.Y + hitBox.Height / 2 - _object.pos.Y - _object.hitBox.Height / 2 == 0)
                { }
                else
                {
                    pos.Y -= _object.Speed.Y;
                    _object.pos.Y -= Speed.Y;
                    
                }
            }
        }

        public override void Update(int _time)
        {
            if (speed.X > 0)
            {
                angle = 10;
                hitBox = new Rectangle((int)pos.X - 5, (int)pos.Y + 5, currentImage.Width, currentImage.Height);
            }

            else if (speed.X < 0)
            {
                angle = -10;
                hitBox = new Rectangle((int)pos.X + 5, (int)pos.Y - 5, currentImage.Width, currentImage.Height);
            }
            else
            {
                angle = 0;
                hitBox = new Rectangle((int)pos.X, (int)pos.Y, currentImage.Width, currentImage.Height);
            }

            currentTime += _time;
            if (currentTime>period)
            {
                currentTime = 0;
                move();
                
            }
            speed.X = 0;
        }
        public override void move()
        {
            pos += Speed;
        }

        


    }
}
