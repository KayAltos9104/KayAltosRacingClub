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
        Dictionary<string, SpriteFont> fontDict=new Dictionary<string, SpriteFont>();

        public BackGround(Vector2 _pos, float _layer, Dictionary<string, Texture2D> _loadTextList, int _Id, int _period) : base(_pos, _layer, _loadTextList, _Id, _period)
        {           
            type = objType.background;
        }

        public BackGround (Vector2 _pos, float _layer, Dictionary<string, Texture2D> _loadTextList, int _Id, Dictionary<string, SpriteFont> _fontDict, int _period) :base (_pos,_layer,_loadTextList, _Id, _period)
        {
            foreach (var f in _fontDict)
            {
                fontDict.Add(f.Key, f.Value);
            }
            type = objType.background;
        }

        public void drawString (string _fontName, string row,Vector2 _pos,Color color,SpriteBatch _spriteBatch)
        {
            _spriteBatch.DrawString(fontDict[_fontName], row, _pos, color);

        }
    }
}
