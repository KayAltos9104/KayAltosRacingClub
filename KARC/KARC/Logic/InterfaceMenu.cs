using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace KARC.Logic
{
    class InterfaceMenu:Scene
    {
        Dictionary<int, Button> buttonDict = new Dictionary<int, Button>();
        int cursor;
        int maxCursor;

        protected int currentTime; //Текущее время игры
        protected int period;

        bool pushed = false;

        public InterfaceMenu (int[,] _map, int _scale, List<Object> _objList, int _period) :base(_map, _scale,_objList)
        {
            cursor = 0;
            foreach (var obj in _objList)
            {
                if (obj.type==objType.button)
                {
                    Button newBut = (Button)obj;
                    buttonDict.Add(newBut.tabIndex, newBut);
                }
            }
            maxCursor = buttonDict.Count;
            period = _period;
           
        }

        public override void updateScene(Keys key, int _time)
        {

            currentTime += _time;
            if (currentTime > period)
            //if (!pushed)
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
            //pushed = true;
            base.updateScene(key, _time);
            
        }
    }
}
