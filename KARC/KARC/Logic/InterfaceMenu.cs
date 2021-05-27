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

        public InterfaceMenu (int[,] _map, int _scale, List<Object> _objList) :base(_map, _scale,_objList)
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
           
        }

        public override void updateScene(Keys key, int _time)
        {
            base.updateScene(key, _time);
            if (key==Keys.Down)
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
    }
}
