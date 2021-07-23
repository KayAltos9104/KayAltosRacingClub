using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KARC.Logic
{
    class PhysicalObject:Object
    {
        public Rectangle hitBox;
        public bool movable;
        public int weight;//Вес объекта в килограммах
        public bool live;
        Vector2 speed = Vector2.Zero;

        public virtual Vector2 Speed
        {
            set
            {
                if (value.X > 1)
                    speed.X = 1;
                else if (value.X < -1)
                    speed.X = -1;
                else
                    speed.X = value.X;

                if (value.Y > 1)
                    speed.Y = 1;
                else if (value.Y < -1)
                    speed.Y = -1;
                else
                    speed.Y = value.Y;

            }
            get
            {
                return speed;
            }
        }


        public PhysicalObject(Vector2 _pos, float _layer, Dictionary<string, Texture2D> _loadTextList, int _weight) :base(_pos, _layer, _loadTextList)
        {
            physical = true;
            weight = _weight;
            hitBox = new Rectangle((int)pos.X, (int)pos.Y, currentImage.Width, currentImage.Height);
            
        }

        public override void Update(int _time)
        {
            hitBox = new Rectangle((int)pos.X, (int)pos.Y, currentImage.Width, currentImage.Height);
            base.Update(_time);
            
        }

        public virtual bool collision (Logic.PhysicalObject _object)//Обработка столкновения
        {
            if (hitBox.Intersects(_object.hitBox))
            {
                //if (movable&&_object.movable)
                //{

                //}
                Rectangle inter;
                Rectangle.Intersect(ref hitBox, ref _object.hitBox, out inter);
                Vector2 intersecVect = pos - _object.pos;
                //Vector2 intersecVect = pos - _object.pos;
                //pos += intersecVect;
                int xShift = inter.Width;
                int yShift = inter.Height;
                if (pos.X + hitBox.Width / 2 - _object.pos.X - _object.hitBox.Width / 2 > 0)
                {
                    pos.X += 1;
                    _object.pos.X -= 1;
                }

                else if (pos.X + hitBox.Width / 2 - _object.pos.X - _object.hitBox.Width / 2 == 0)
                { }
                else
                {
                    pos.X -= 1;
                    _object.pos.X += 1;
                }


                if (pos.Y + hitBox.Height / 2 - _object.pos.Y - _object.hitBox.Height / 2 > 0)
                {
                    pos.Y += 1;
                    _object.pos.Y -= 1;
                }
                else if (pos.Y + hitBox.Height / 2 - _object.pos.Y - _object.hitBox.Height / 2 == 0)
                { }
                else
                {
                    pos.Y -= 1;
                    _object.pos.Y += 1;
                }
                return true;
            }
            else
                return false;
        }
        public double CountDistance(PhysicalObject _CheckObj)//Возвращает расстояние между этим объектом и объектом в аргументе
        {
            return Math.Sqrt(Math.Pow(hitBox.Center.X - _CheckObj.hitBox.Center.X, 2) + Math.Pow(hitBox.Center.Y - _CheckObj.hitBox.Center.Y, 2));
        }

        public bool CheckNeighborhood (PhysicalObject _CheckObj)
        {
            return (CountDistance(_CheckObj) < Math.Max(this.hitBox.Width * 5, this.hitBox.Height * 5));
        }

        public virtual void move()
        {

        }

        public Vector2 calcCenter()
        {
            return new Vector2((pos.X + hitBox.Width) / 2, (pos.Y + hitBox.Height) / 2);//TODO: Можно как-то через точку напрямую сделать
        }
    }
}
