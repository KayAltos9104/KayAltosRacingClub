using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KARC.GameObjsTemplates
{
    public abstract partial class GameObject : IBehaviour
    {
        private Dictionary<string, Texture2D> imagesDict = new Dictionary<string, Texture2D>();//Изображения, которые может иметь объект
        private Texture2D _currentImage; //Текущее изображение 
        protected float _layer; //Слой отрисовки
        protected float _scale;
        protected Color _colDraw;        
        protected int _angle;
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture:_currentImage,position: pos,sourceRectangle: null, 
                color: _colDraw,rotation: MathHelper.ToRadians(_angle),origin: Vector2.Zero,
                scale: _scale,effects: SpriteEffects.None,layerDepth: _layer);
            
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
            _layer -= step;
        }
        public void LayerToBack(float step)
        {
            _layer += step;
        }
        protected void SwitchImage (string key)
        {
            _currentImage = imagesDict[key];
        }

        protected virtual void Animate ()
        {

        }

        public virtual void InitializeGraphics ()
        {
            _layer = 1.0f;
            _scale = 1.0f;
            _colDraw = Color.White;
            _angle = 0;
            _currentImage = imagesDict.First().Value;
        }

        public void Stretch (float coef)
        {
            _scale =1.0f*coef;
        }

        public (int, int) GetImageSize ()
        {
            return (_currentImage.Width, _currentImage.Height);
        }
    }
}
