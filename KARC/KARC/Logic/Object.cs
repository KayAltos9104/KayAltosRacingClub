﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
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
        //Графика
        public Dictionary<string, Texture2D> images = new Dictionary<string, Texture2D>();//Изображения, которые может иметь объект
        public Texture2D currentImage; //Текущее изображение        
        private Scene _parentScene;//К какой сцене относится объект
        protected float layer = 1.0f; //Слой отрисовки
        protected float scale = 1.0f;


        public string Tag
        {
            set
            {

            }
            get
            {
                return tag;
            }
        }
        protected string tag;

        public Color colDraw = Color.White;
        public bool player = false;
        protected int angle = 0;
        public Dictionary<string, SoundEffect> soundEffectsDict = new Dictionary<string, SoundEffect>();
        


       public Dictionary<string, Animation> animationDict = new Dictionary<string, Animation>();
        //Физика
        public Vector2 pos; //Текущая позиция

        //Технические данные
        protected int currentTime; //Текущее время игры
        protected int period;
        public int id; //Id объекта
        

        public bool physical;


        public Object()
        {

        }

        public Object (Vector2 _pos, float _layer, Dictionary <string, Texture2D> _loadTextList, Scene parentScene)
        {
            layer = _layer;
            pos = _pos;   
            foreach (var img in _loadTextList)
            {
                loadImages(img.Key, img.Value);
            }  
            currentImage = images.ElementAt(0).Value;
            _parentScene = parentScene;
            physical = false;
        }

       

        public virtual void Update(int _time) //Обновление состояния объекта
        {            
            currentTime += _time;            
        }

        public virtual void Update(Keys key, int _time)//Обновление состояния объекта с учетом нажатой клавиши
        {            
            currentTime += _time;           
        }


        protected virtual void Teleport (Vector2 newPos)//Поместить объект на заданное место
        {
            pos = newPos;
        }

        protected virtual void Move(Vector2 newPos)//Сместить объект на заданный вектор (сложить текущий вектор и заданный)
        {
            pos.X += newPos.X;
            pos.Y += newPos.Y;
        }

        private void loadImages(string _key, Texture2D _image)//Добавить изображение к списку изображений
        {
            images.Add(_key, _image);
        }

        public virtual void drawObject (SpriteBatch _spriteBatch, int _time)//Метод отрисовки объекта
        {
            _spriteBatch.Draw(currentImage,pos,null, colDraw, MathHelper.ToRadians(angle), Vector2.Zero,scale, SpriteEffects.None,layer);
        }

       

    }
}
