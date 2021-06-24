using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KARC.Logic
{
    class Car : PhysicalObject
    {
        Vector2 speed = Vector2.Zero;
        public bool explode=false;
        public int acceleration = 5;
        public int maneuver = 5;
        public SpriteEffects orientation = SpriteEffects.None;

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

                if (value.Y > 40)
                    speed.Y = 40;
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

        public Car(Vector2 _pos, float _layer, Dictionary<string, Texture2D> _loadTextList, int _Id, Vector2 _speed, int _weight) : base(_pos, _layer, _loadTextList, _Id, _weight)
        {
            Speed = _speed;
            movable = true;
            type = objType.car;
            period = 10;
            live = true;
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
                    //pos.X -= _object.Speed.X;
                    //_object.pos.X += Speed.X;
                    pos.X += 2;
                    _object.pos.X -= 2;
                }

                else if (pos.X + hitBox.Width / 2 - _object.pos.X - _object.hitBox.Width / 2 == 0)
                { }
                else
                {
                    //pos.X += _object.Speed.X;
                    //_object.pos.X -= Speed.X;
                    pos.X -= 2;
                    _object.pos.X += 2;

                }


                if (pos.Y + hitBox.Height / 2 - _object.pos.Y - _object.hitBox.Height / 2 > 0)
                {
                    //pos.Y += _object.Speed.Y;
                    //_object.pos.Y -= Speed.Y;
                    pos.Y += 2;
                    _object.pos.Y -= 2;
                }
                else if (pos.Y + hitBox.Height / 2 - _object.pos.Y - _object.hitBox.Height / 2 == 0)
                { }
                else
                {
                    //pos.Y -= _object.Speed.Y;
                    //_object.pos.Y += Speed.Y;
                    pos.Y -= 2;
                    _object.pos.Y += 2;

                }
                Vector2 buf = speed;
                speed = _object.Speed;
                _object.Speed = buf;

                live = false;
                _object.live = false;

            }
        }

        public override void Update(int _time)
        {
            if (!live)
            {
                if (speed.Y > 0)
                {
                    speed.Y -= 1;                    
                }

                else if (speed.Y < 0)
                {
                    speed.Y += 1;                   
                }

                else
                {
                    speed.Y = 0;                    
                }

                if (speed.X > 0)
                {                    
                    speed.X -= 1;
                }

                else if (speed.X < 0)
                {                    
                    speed.X += 1;
                }
                else
                {                    
                    speed.X = 0;
                }

                currentImage = images["CrushedModel"];
                //SoundEffectInstance soundEffectInstance = soundEffectsDict["explosion"].CreateInstance();
                //soundEffectInstance.IsLooped = false;
                //if (!explode)
                //{
                //    soundEffectInstance.Play();
                //    explode = true;
                //}
                //else
                //{
                //    soundEffectInstance.Resume();
                //}
            }

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
            if (currentTime > period)
            {
                currentTime = 0;
                move();
            }
            if (live)
                speed.X = 0;

        }

        public override void move()
        {
            pos += Speed;
        }

        public override void drawObject(SpriteBatch _spriteBatch, int _time)//Метод отрисовки объекта
        {
            _spriteBatch.Draw(currentImage, pos, null, colDraw, MathHelper.ToRadians(angle), Vector2.Zero, 1.0f, orientation, layer);
            if (!live)
            {
                //animationDict["explosion"].objectPos = this.pos;
                animationDict["explosion"].objectPos.X = this.pos.X- 82;
                animationDict["explosion"].objectPos.Y = this.pos.Y - 180;
                animationDict["explosion"].drawObject(_spriteBatch, _time);
            }

        }
    }
}
