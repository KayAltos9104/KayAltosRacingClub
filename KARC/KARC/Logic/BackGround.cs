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
        Dictionary<string, SpriteFont> fontDict;

        public BackGround (Vector2 _pos, float _layer, Dictionary<string, Texture2D> _loadTextList, int _Id, Dictionary<string, SpriteFont> _fontDict) :base (_pos,_layer,_loadTextList, _Id)
        {
            foreach (var f in _fontDict)
            {
                fontDict.Add(f.Key, f.Value);
            }
        }
    }
}
