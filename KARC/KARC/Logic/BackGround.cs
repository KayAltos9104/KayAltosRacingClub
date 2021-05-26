using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KARC.Logic
{
    class BackGround:Object
    {
        public BackGround (Vector2 _pos, float _layer, Dictionary<string, Texture2D> _loadTextList, int _Id) :base (_pos,_layer,_loadTextList, _Id)
        {

        }
    }
}
