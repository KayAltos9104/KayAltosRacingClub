using System;
using System.Collections.Generic;
using System.Text;
using KARC.GameObjsTemplates;
using KARC.GameObjsTemplates.InterfaceObjs;
using KARC.GameObjsTemplates.InterfaceObjs.Controls;
using Microsoft.Xna.Framework;
using static KARC.ScenesTemplates.Menu;

namespace KARC.ScenesTemplates
{
    class Menu:Scene
    {
        public delegate void CursorHandler(object sender, CursorEventArgs e);
        public event CursorHandler Accept;
        public event CursorHandler ChooseElement;
        public event CursorHandler ChangeProperty;

        protected int _columns;
        protected int _rows;
        

        Dictionary<int, (string,IObjectUI)> uiDict;
        int _cursor;       
        int _tabIndex;
        public Menu ()
        {
            _tabIndex = 0;
            uiDict = new Dictionary<int, (string, IObjectUI)>();            
            _cursor = 0;

            Accept += (object sender, CursorEventArgs e) =>
            {
                if (uiDict[_cursor].Item2.GetType() == typeof(Button)) //Точное соответствие
                //if (uiDict[_cursor].Item2.GetType() is IObjectUI) 
                {
                    var btn = (Button)uiDict[_cursor].Item2;
                    btn.PerformClick();
                }
            };

            ChooseElement += (object sender, CursorEventArgs e) =>
            {
                MoveCursor(e.Dir);
            };

        }
        
        protected void LoadUI()
        {
            
            foreach (var obj in _objDict)
            {
                if(obj.Value is IObjectUI)//Включает наследников
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

        

        

        protected void PlaceElement (Control element, int row, int column)
        {
            element.ChangePlace(GetCoord(row, column));
            float stretchCoef = 1.0f*(_windowWidth / _columns)/element.GetImageSize().Item1;
            var t = element.GetImageSize().Item1;
            element.Stretch(stretchCoef);
        }
        private Vector2 GetCoord (int row, int column)
        {
            int x = column * GetColumnWidth();
            int y = row * GetRowHeight();
            return new Vector2(x, y);
        }

        private int GetRowHeight ()
        {
            return _windowHeight / _rows;
        }

        private int GetColumnWidth()
        {
            return _windowWidth / _columns;
        }

        private void MoveCursor (CursorDirection dir)
        {
            if (dir == CursorDirection.up)
            {
                _cursor--;
                if (_cursor < 0)
                    _cursor = _tabIndex - 1;
               
            }
            if (dir == CursorDirection.down)
            {
                _cursor++;
                if (_cursor >= _tabIndex)
                    _cursor = 0;
            }
        }

        public override void InitializeScene()
        {
            _columns = 1;
            _rows = 1;
        }
        public void AcceptPerform(object sender, CursorEventArgs e)
        {
            Accept.Invoke(sender, e);
        }

        public void ChoosePerform(object sender, CursorEventArgs e)
        {
            ChooseElement.Invoke(sender, e);
        }

        public void ChangePropertyPerform(object sender, CursorEventArgs e)
        {
            ChangeProperty.Invoke(sender, e);
        }

        public enum CursorDirection: byte
        {
            up = 0,
            down = 1,
            right = 2,
            left = 3
        }
    }

    class CursorEventArgs
    {
        public CursorDirection Dir { get; }
        public CursorEventArgs (CursorDirection dir)
        {
            Dir = dir;
        }
    }

}
