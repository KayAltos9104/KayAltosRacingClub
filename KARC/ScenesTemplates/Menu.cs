using System;
using System.Collections.Generic;
using System.Text;
using KARC.GameObjsTemplates;

namespace KARC.ScenesTemplates
{
    class Menu:Scene
    {        
        Dictionary<int, (string,IObjectUI)> uiDict;
        int _cursor;       
        int _tabIndex;
        public Menu ()
        {
            _tabIndex = 0;
            uiDict = new Dictionary<int, (string, IObjectUI)>();            
            _cursor = 0;
        }
        
        protected void LoadUI()
        {
            foreach (var obj in _objDict)
            {
                if(obj.GetType()==typeof(IObjectUI))
                {                   
                    AddUI((IObjectUI)obj.Value);
                }
            }
        }
        private void AddUI(IObjectUI newUI)
        {
            uiDict.Add(_tabIndex, (newUI.Name, newUI));
            _tabIndex++;
           
        }
        private void RemoveUI(int index)
        {
            uiDict.Remove(index);
            _tabIndex--;            
        }

        public override void Update()
        {
            foreach (var ui in uiDict)
            {
                ui.Value.Item2.IsChoosed = false;
            }
            uiDict[_cursor].Item2.IsChoosed = true;
            base.Update();            
        }

        public void MoveCursor (CursorDirection dir)
        {
            if (dir == CursorDirection.up)
            {
                _cursor--;
                if (_cursor < 0)
                    _cursor = _tabIndex - 1;
                if (_cursor >= _tabIndex)
                    _cursor = 0;
            }
        }

        public enum CursorDirection: byte
        {
            up = 0,
            down = 1,
            right = 2,
            left = 3
        }
    }

}
