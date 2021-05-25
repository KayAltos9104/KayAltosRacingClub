using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KARC.Logic
{
    class Object
    {
        Dictionary<string, Texture2D> images = new Dictionary<string, Texture2D>();//Изображения, которые имеет объект
        Texture2D currentImage;
        Vector2 pos;

        float layer = 1.0f; //Слой отрисовки

        public Object()
        {

        }

        public Object (Vector2 _pos, float _layer)
        {
            layer = _layer;
            pos = _pos;
            
        }


        public void loadImages(string _key, Texture2D _image)
        {
            images.Add(_key, _image);
        }

        protected virtual void drawObject (SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(currentImage,pos,null, Color.White,0, Vector2.Zero,1.0f, SpriteEffects.None,layer);
        }

        public virtual void Update (SpriteBatch _spriteBatch)
        {
            currentImage = images.ElementAt(0).Value;
            drawObject(_spriteBatch);
        }

        public virtual void Update(SpriteBatch _spriteBatch, Keys key)
        {
            drawObject(_spriteBatch);
        }


    }
}
