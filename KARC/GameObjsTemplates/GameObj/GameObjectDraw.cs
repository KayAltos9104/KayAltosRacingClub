using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace KARC.GameObjsTemplates
{
    public abstract partial class GameObject : IBehaviour
    {
        private Dictionary<string, Texture2D> imagesDict = new Dictionary<string, Texture2D>();//Изображения, которые может иметь объект
        private Texture2D currentImage; //Текущее изображение 
        protected float layer; //Слой отрисовки
        protected float scale;
        protected Color colDraw;        
        protected int angle;
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(currentImage, pos, null, colDraw, MathHelper.ToRadians(angle), Vector2.Zero, scale, SpriteEffects.None, layer);
        }

        public void AddImage (string key, Texture2D image)
        {
            imagesDict.Add(key, image);
        }
        public void RemoveImage(string key)
        {
            imagesDict.Remove(key);            
        }

        public void LayerToFront (float step)
        {
            layer -= step;
        }
        public void LayerToBack(float step)
        {
            layer += step;
        }
        protected void SwitchImage (string key)
        {
            currentImage = imagesDict[key];
        }

        protected virtual void Animate ()
        {

        }

        protected virtual void InitializeGraphics ()
        {
            layer = 1.0f;
            scale = 1.0f;
            colDraw = Color.White;
            angle = 0;
        }
    }
}
