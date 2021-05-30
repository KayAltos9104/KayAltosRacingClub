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

        public PhysicalObject(Vector2 _pos, float _layer, Dictionary<string, Texture2D> _loadTextList, int _Id, int _period, int _weight) :base(_pos, _layer, _loadTextList, _Id, _period)
        {
            physical = true;
            weight = _weight;
            hitBox = new Rectangle((int)pos.X, (int)pos.Y, currentImage.Width, currentImage.Height);
        }

        public virtual void collision (Logic.PhysicalObject _object)//Обработка столкновения
        {
            if (hitBox.Intersects(_object.hitBox))
            {
                //if (movable&&_object.movable)
                //{

                //}
                Vector2 intersecVect = pos - _object.pos;
                pos -= intersecVect;
            }
        }
    }
}
