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
        //protected Rectangle drawRect;
        protected bool isSized=false;
        protected Vector2 sizePoint;

        public BackGround(Vector2 _pos, float _layer, Dictionary<string, Texture2D> _loadTextList, bool isSized, Vector2 _sizePoint) : base(_pos, _layer, _loadTextList)
        { 
            this.isSized = isSized;
            sizePoint = _sizePoint;
            if (isSized)
            {
                //float maxImage = Math.Max(currentImage.Width, currentImage.Height);
                //float maxDimension = Math.Max(Game1.windoWidth, Game1.windowHeight);
                //scale = maxDimension / maxImage;
            }
        }

        public BackGround (Vector2 _pos, float _layer, Dictionary<string, Texture2D> _loadTextList, bool isSized, Dictionary<string, SpriteFont> _fontDict, Vector2 _sizePoint) :base (_pos,_layer,_loadTextList)
        {
            this.isSized = isSized;
            sizePoint = _sizePoint;
            if (isSized)
            {
                //float maxImage = Math.Max(currentImage.Width, currentImage.Height);
                //float maxDimension = Math.Max(Game1.windoWidth, Game1.windowHeight);
                //scale = maxDimension / maxImage;
            }
            foreach (var f in _fontDict)
            {
                fontDict.Add(f.Key, f.Value);
            }           
        }
        public override void drawObject(SpriteBatch _spriteBatch, int _time)//Метод отрисовки объекта
        {
            if (isSized)
                //_spriteBatch.Draw(currentImage, pos, new Rectangle((int)pos.X, (int)pos.Y, Game1.windoWidth, Game1.windowHeight), colDraw, 0, Vector2.Zero, 1.0f, SpriteEffects.None, layer);
                //_spriteBatch.Draw(currentImage, new Rectangle((int)pos.X, (int)pos.Y, (int)(pos.X+sizePoint.X), (int)(pos.Y+sizePoint.Y)), new Rectangle(0, 0, (int)(0 + 
                //    currentImage.Width), (int)(0 + currentImage.Height)), colDraw, 0, Vector2.Zero, SpriteEffects.None, layer);
                _spriteBatch.Draw(currentImage, new Rectangle((int)pos.X, (int)pos.Y, (int)(sizePoint.X), (int)(sizePoint.Y)), new Rectangle(0, 0, (int)(0 +
                    currentImage.Width), (int)(0 + currentImage.Height)), colDraw, 0, Vector2.Zero, SpriteEffects.None, layer);

            else
                _spriteBatch.Draw(currentImage, pos, null, colDraw, MathHelper.ToRadians(angle), Vector2.Zero, scale, SpriteEffects.None, layer);
            
        }
        public void drawString (string _fontName, string row,Vector2 _pos,Color color,SpriteBatch _spriteBatch)
        {
            _spriteBatch.DrawString(fontDict[_fontName], row, _pos, color);

        }
    }
}
