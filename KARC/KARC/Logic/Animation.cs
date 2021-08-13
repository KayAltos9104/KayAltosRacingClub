using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KARC.Logic
{
    class Animation
    {
        public Texture2D image;
        bool cycled;
        public Vector2 objectPos;
        int frameWidth;
        int frameHeight;
        Point currentFrame;//Задает положение начального фрейма в файле анимации и потом задает положение нового фрейма
        Point spriteSize;//Через эту точку задается кол-во спрайтов - 4 по ширине и 3 по высоте

        public float layer;
        public float scale = 1.0f;


        int currentTime = 0; // сколько времени прошло
        public int period = 20; // период обновления в миллисекундах


        public bool ended = false;

        public Animation(Texture2D _animImage, int _frameWidth, int _frameHeight, Point _spriteSize, Vector2 _pos, bool _cycle)
        {
            image = _animImage;
            frameWidth = _frameWidth;
            frameHeight = _frameHeight;
            spriteSize = _spriteSize;
            currentFrame = new Point(0, 0);
            objectPos = _pos;
            cycled = _cycle;
        }



        public void scrollFrame(Vector2 curPos, int _time)
        {
            currentTime += _time;
            if (currentTime > period)
            {
                currentTime -= period;
                if (!cycled && ended)
                {

                }
                else
                {
                    objectPos = curPos;
                    ++currentFrame.X;//Переходим к следующему кадру по горизонтали
                    if (currentFrame.X >= spriteSize.X)
                    {
                        //Переходим на следующую строку
                        currentFrame.X = 0;
                        ++currentFrame.Y;
                        if (currentFrame.Y >= spriteSize.Y)
                        {
                            currentFrame.Y = 0;
                            ended = true;
                        }
                    }
                }
            }
        }

        public virtual void drawObject(SpriteBatch _spriteBatch, int _time)//Метод отрисовки объекта
        {
            if (!ended||cycled)
            {
                _spriteBatch.Draw(image, objectPos, new Rectangle(currentFrame.X * frameWidth, currentFrame.Y * frameHeight, frameWidth, frameHeight), Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, layer);
                scrollFrame(objectPos, _time);
            }
            
        }
    }
}
