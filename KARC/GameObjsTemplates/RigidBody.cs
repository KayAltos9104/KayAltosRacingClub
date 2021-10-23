using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace KARC.GameObjsTemplates
{
    class RigidBody
    {
        private Rectangle _hitBox;        
        private int _weight;//Вес объекта в килограммах

        public RigidBody (int weight, Point p, Point size)
        {
            _hitBox = new Rectangle(p, size);
            _weight = weight;
        }
        public bool IsCollided (Rectangle anotherHitBox)
        {
            return _hitBox.Intersects(anotherHitBox);
        }

        public void Move(Point p)
        {
            _hitBox = new Rectangle(p, _hitBox.Size);
        }
    }
}
