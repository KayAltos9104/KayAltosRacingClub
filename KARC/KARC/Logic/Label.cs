using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KARC.Logic
{
    class Label:SwitchBox
    {
        public Label(Vector2 _pos, float _layer, Dictionary<string, Texture2D> _loadTextList, int _Id, int _tabIndex, SpriteFont _font, string[] _valuesArray, int _currentIndex) : base(_pos, _layer, _loadTextList, _Id, _tabIndex, _font, _valuesArray, _currentIndex)
        {
            
        }

        public override void Update(int _time)
        {
           
        }

        public override void drawObject(SpriteBatch _spriteBatch, int _time)//Метод отрисовки объекта
        {
            //string s = "";
            //for (int i = 0; i < valuesArray.Length; i++)
            //    s += valuesArray[i];
            Vector2 stringLength = font.MeasureString(valuesArray[currentIndex]);
            //_spriteBatch.Draw(currentImage, pos, null, colDraw, MathHelper.ToRadians(angle), Vector2.Zero, scale, SpriteEffects.None, layer);
            _spriteBatch.Draw(currentImage, new Rectangle((int)pos.X, (int)pos.Y, (int)stringLength.X, (int)stringLength.Y* valuesArray.Length), new Rectangle((int)pos.X, (int)pos.Y, currentImage.Width, currentImage.Height), colDraw, 0, Vector2.Zero, SpriteEffects.None, layer);
            for (int i = 0; i < valuesArray.Length; i++)
                _spriteBatch.DrawString(font, valuesArray[i], new Vector2(pos.X + (currentImage.Width - stringLength.X) / 2, (int)(stringLength.Y*i*1.1)+pos.Y), Color.Black);
        }
    }
}
