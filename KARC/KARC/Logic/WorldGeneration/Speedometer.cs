using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KARC.Logic
{
    class Speedometer:Object
    {
        public Texture2D arrowImage;
        
        public Speedometer (Vector2 _pos, float _layer, Dictionary<string, Texture2D> _loadTextList, Scene parentScene) : base (_pos, _layer, _loadTextList, parentScene)
        {
            arrowImage = images.ElementAt(1).Value;
            angle = 180;
        }
        public override void Update(int _time)
        {            
            if ((angle - 180) <= Game1.curSpeed)
            {               
                angle++;
            }
            else
                angle--;            
            
        }
        public override void drawObject(SpriteBatch _spriteBatch, int _time)//Метод отрисовки объекта
        {
            _spriteBatch.Draw(currentImage, pos, null, colDraw, 0, Vector2.Zero, scale, SpriteEffects.None, layer);
            Vector2 arrowPos = new Vector2(pos.X + currentImage.Width / 2, pos.Y+ currentImage.Height-arrowImage.Height);
            _spriteBatch.Draw(arrowImage, arrowPos, null, colDraw, MathHelper.ToRadians(angle), Vector2.Zero, scale, SpriteEffects.None, layer-0.01f);

            //_spriteBatch.Draw(currentImage, pos, new Rectangle((int)pos.X,(int)pos.Y,Game1.windoWidth,Game1.windowHeight), colDraw, 0, Vector2.Zero, 1.0f, SpriteEffects.None, layer);
        }
    }
}
