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
        public PhysicalObject(Vector2 _pos, float _layer, Dictionary<string, Texture2D> _loadTextList, int _Id, int _period) :base(_pos, _layer, _loadTextList, _Id, _period)
        {
            physical = true;
            hitBox = new Rectangle((int)pos.X, (int)pos.Y, currentImage.Width, currentImage.Height);
        }
    }
}
