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
    class Menu:Scene//Сцена, которая содержит интерфейс
    {
        public delegate void CursorHandler(object sender, CursorEventArgs e);
        public event CursorHandler Accept;//Событие подтверждения
        public event CursorHandler ChooseElement;//Событие изменения выбранного элемента
        public event CursorHandler ChangeProperty;//Событие изменения параметров выбранного элемента

        //Количество столбцов и сток разметки
        protected int _columns; 
        protected int _rows;
        

        Dictionary<int, (string,Control)> _uiDict;//Список элемнтов интерфейса
        int _cursor;//Текущее положение курсора       
        int _tabIndex;//Текущее количество элементов интерфейса
        public Menu ()
        {
            _cursor = 0;
            _tabIndex = 0;
            _uiDict = new Dictionary<int, (string, Control)>();    

            //На событие подтверждения стоит активация клика кнопки, если курсор на кнопке
            Accept += (object sender, CursorEventArgs e) =>
            {
                if (_uiDict[_cursor].Item2.GetType() == typeof(Button)) //Точное соответствие                
                {
                    var btn = (Button)_uiDict[_cursor].Item2;
                    btn.PerformClick();
                }
            };

            //Если произошло событие изменения, то двигаем курсор
            ChooseElement += (object sender, CursorEventArgs e) =>
            {
                MoveCursor(e.Dir);
            };

        }
        public override void InitializeScene()
        {
            _columns = 1;
            _rows = 1;
        }

        public override void Update()
        {
            //Выключаем состояние выбора на элементах интерфейса и включаем на том, на котором курсор
            foreach (var ui in _uiDict)
            {
                ui.Value.Item2.IsChoosed = false;
            }
            _uiDict[_cursor].Item2.IsChoosed = true;
            base.Update();
        }

        public override void UpdateGraphics(int windowWidth, int windowHeight)
        {
            base.UpdateGraphics(windowWidth, windowHeight);
            foreach (var ui in _uiDict)
            {
                PlaceElement(ui.Value.Item2, ui.Value.Item2.Row, ui.Value.Item2.Column);
            }
        }
        //Разместить элемент интерфейса с учетом его разметки
        protected void PlaceElement (Control element, int row, int column)
        {
            element.ChangePlace(GetCoord(row, column));//Получение координаты из разметки
            float stretchCoef = 1.0f*(_windowWidth / _columns)/element.GetImageSize().Item1;
            element.Stretch(stretchCoef);//Растягиваем элемент так, чтобы он уместился во всю ширину столбца
            element.SaveTracking(row, column);//Сохраняем разметку элемента
            
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

        private Vector2 GetCoord(int row, int column)//Получить векторную координату из разметки
        {
            int x = column * GetColumnWidth();
            int y = row * GetRowHeight();
            return new Vector2(x, y);
        }

        private int GetRowHeight()
        {
            return _windowHeight / _rows;
        }

        private int GetColumnWidth()
        {
            return _windowWidth / _columns;
        }

        protected void LoadUI()//Сохраняем интерфейсные объекты в отдельный список и присваиваем каждому его tabIndex
        {
            foreach (var obj in _objDict)
            {
                if (obj.Value is Control)//Включает наследников
                {
                    AddUI((Control)obj.Value);
                }
            }
        }
        private void AddUI(Control newUI)//Добавить новый элемент интерфейса в список
        {
            _uiDict.Add(_tabIndex, (newUI.Name, newUI));
            _tabIndex++;

        }
        private void RemoveUI(int index)
        {
            _uiDict.Remove(index);
            _tabIndex--;
        }

        //Активация событий, чтобы можно было делать это извне
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
