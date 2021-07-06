using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace KARC.Logic
{
    class InterfaceMenu:Scene
    {
        Dictionary<int, Button> buttonDict = new Dictionary<int, Button>();
        public int cursor;
        int maxCursor;

        int rowWidth;
        int colWidth;
        int rows;
        int columns;

        protected int currentTime; //Текущее время игры
        protected int period;

       

        public InterfaceMenu (int[,] _map, int _scale, List<Object> _objList, int _period, int _rows, int _columns) :base(_map, _scale,_objList)
        {
            cursor = 0;
            rows = _rows;
            columns = _columns;
            rowWidth = Game1.windoWidth / rows;
            colWidth = Game1.windowHeight / columns;

            foreach (var obj in _objList)
            {                
                if (obj.GetType()==typeof(Button))
                {
                    Button newBut = (Button)obj;
                    buttonDict.Add(newBut.tabIndex, newBut);
                }               
                if (obj.GetType() == typeof(SwitchBox))
                {
                    SwitchBox newBut = (SwitchBox)obj;
                    buttonDict.Add(newBut.tabIndex, newBut);
                }                
            }
            maxCursor = buttonDict.Count;
            period = _period;           
        }

        public static Vector2 GetCoord (int row, int column, int _rows, int _columns)
        {
            return new Vector2(row * (Game1.windoWidth / _rows), column * (Game1.windowHeight / _columns));
        }
        public Vector2 GetCoord (int row, int column)//TODO: Сделать проверку
        {
            return new Vector2(row * rowWidth, column * colWidth);
        }
        public override void updateScene(Keys key, int _time)
        {
            currentTime += _time;
            if (currentTime > period)            
            {
                currentTime = 0;
                
                if (key == Keys.Down)
                {
                    cursor++;
                    if (cursor >= maxCursor)

                        cursor = 0;
                }
                if (key == Keys.Up)
                {
                    cursor--;
                    if (cursor < 0)
                        cursor = maxCursor - 1;
                }
                foreach (var but in buttonDict)
                {
                    if (but.Key == cursor)
                        but.Value.check = true;
                    else
                        but.Value.check = false;
                }
            }
            
            base.updateScene(key, _time);
            
        }
    }
}
