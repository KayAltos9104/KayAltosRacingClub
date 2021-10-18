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
        protected Vector2 sizePoint;
        public Label(Vector2 _pos, float _layer, Dictionary<string, Texture2D> _loadTextList, SpriteFont _font, string[] _valuesArray, int _currentIndex, Vector2 _sizePoint, Scene parentScene) : base(_pos, _layer, _loadTextList, _font, _valuesArray, _currentIndex, parentScene)
        {
            sizePoint = _sizePoint;
        }

        public override void Update(int _time)
        {
           
        }

        public override void drawObject(SpriteBatch _spriteBatch, int _time)//Метод отрисовки объекта
        {
            //Vector2 stringLength = font.MeasureString(valuesArray[currentIndex]);
            //_spriteBatch.Draw(currentImage, new Rectangle((int)pos.X, (int)pos.Y, (int)stringLength.X, 
            //    (int)stringLength.Y* valuesArray.Length), new Rectangle((int)pos.X, (int)pos.Y, currentImage.Width, currentImage.Height), 
            //    colDraw, 0, Vector2.Zero, SpriteEffects.None, layer);
            //for (int i = 0; i < valuesArray.Length; i++)
            //    _spriteBatch.DrawString(font, valuesArray[i], new Vector2(pos.X, (int)(stringLength.Y*i*1.1)+pos.Y), Color.Black);
            //_spriteBatch.Draw(currentImage, pos, null, colDraw, MathHelper.ToRadians(angle), Vector2.Zero, scale, SpriteEffects.None, layer);
            _spriteBatch.Draw(currentImage, new Rectangle((int)pos.X, (int)pos.Y, (int)(sizePoint.X), (int)(sizePoint.Y)), new Rectangle(0, 0, (int)(0 +
                    currentImage.Width), (int)(0 + currentImage.Height)), colDraw, 0, Vector2.Zero, SpriteEffects.None, layer);
            for (int i = 0; i < valuesArray.Length; i++)
            {
                Vector2 stringLength = font.MeasureString(valuesArray[i]);
                _spriteBatch.DrawString(font, valuesArray[i], new Vector2(pos.X + (sizePoint.X - stringLength.X) / 2,
                    pos.Y + (sizePoint.Y - stringLength.Y) / 2+ stringLength.Y * i), Color.Black);
            }
               

        }
    }
}
