﻿using Microsoft.Xna.Framework;
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
        protected Dictionary<string, Texture2D> images = new Dictionary<string, Texture2D>();//Изображения, которые может иметь объект
        protected Texture2D currentImage; //Текущее изображение        
        protected float layer = 1.0f; //Слой отрисовки
        
        //Физика
        protected Vector2 pos; //Текущая позиция

        //Технические данные
        protected int currentTime; //Текущее время игры
        protected int id; //Id объекта
        protected objType type; //Тип объекта

        public Object()
        {

        }

        public Object (Vector2 _pos, float _layer, Dictionary <string, Texture2D> _loadTextList, int _Id)
        {
            layer = _layer;
            pos = _pos;   
            foreach (var img in _loadTextList)
            {
                loadImages(img.Key, img.Value);
            }  
            currentImage = images.ElementAt(0).Value;
            id = _Id;
        }


        public virtual void Update(SpriteBatch _spriteBatch, int _time) //Обновление состояния объекта
        {            
            currentTime = _time;
            drawObject(_spriteBatch);
        }

        public virtual void Update(SpriteBatch _spriteBatch, Keys key, int _time)//Обновление состояния объекта с учетом нажатой клавиши
        {            
            currentTime = _time;
            drawObject(_spriteBatch);
        }


        protected virtual void teleport (Vector2 newPos)//Поместить объект на заданное место
        {
            pos = newPos;
        }

        protected virtual void move(Vector2 newPos)//Сместить объект на заданный вектор (сложить текущий вектор и заданный)
        {
            pos.X += newPos.X;
            pos.Y += newPos.Y;
        }

        private void loadImages(string _key, Texture2D _image)//Добавить изображение к списку изображение
        {
            images.Add(_key, _image);
        }

        protected virtual void drawObject (SpriteBatch _spriteBatch)//Метод отрисовки объекта
        {
            _spriteBatch.Draw(currentImage,pos,null, Color.White,0, Vector2.Zero,1.0f, SpriteEffects.None,layer);
        }

       


    }
}
