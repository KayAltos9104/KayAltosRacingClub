﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KARC.Logic
{
    class SwitchBox:Button
    {
        SpriteFont font;
        string[] valuesArray;
        int currentIndex;
        public SwitchBox(Vector2 _pos, float _layer, Dictionary<string, Texture2D> _loadTextList, int _Id, int _tabIndex, SpriteFont _font, string [] _valuesArray) : base(_pos, _layer, _loadTextList, _Id, _tabIndex)
        {
            tabIndex = _tabIndex;
            type = objType.switchbox;
            period = 50;
            _valuesArray.CopyTo(valuesArray, 0);
            currentIndex = 0;
        }

        public override void Update(int _time)
        {
            if (check)
                currentImage = images["light"];
            else
                currentImage = images["dark"];            
        }
        public void ChangeIndex (string direction)
        {
            switch (direction)
            {
                case "right":
                    {
                        currentIndex++;
                        if (currentIndex >= valuesArray.Length)
                            currentIndex = 0;
                        break;
                    }
                case "left":
                    {
                        currentIndex--;
                        if (currentIndex <0)
                            currentIndex = valuesArray.Length-1;
                        break;
                    }
            }
        }

        public override void drawObject(SpriteBatch _spriteBatch, int _time)//Метод отрисовки объекта
        {
            _spriteBatch.Draw(currentImage, pos, null, colDraw, MathHelper.ToRadians(angle), Vector2.Zero, scale, SpriteEffects.None, layer);
            _spriteBatch.DrawString(font, valuesArray[currentIndex], pos, Color.White);            
        }

    }
}
