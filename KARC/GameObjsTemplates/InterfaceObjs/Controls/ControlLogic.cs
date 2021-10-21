using System;
using System.Collections.Generic;
using System.Text;

namespace KARC.GameObjsTemplates.InterfaceObjs.Controls
{
    partial class Control : GameObject, IObjectUI
    {
        public delegate void ClickHandler(object sender);//Обработчик клика по элементу
        public bool IsChoosed { get; set; } //Находится ли курсор на объекте
        public string Name { get; private set; } //Строковое имя элемента. Аналогично Name в WinForms или WPF
        //Разметка объекта
        public int Row { get; private set; }
        public int Column { get; private set; }
        public Control (string name)
        {
            Name = name;
            IsChoosed = false;
        }
        public override void Update()
        {
            base.Update();
            //Меняем картинку элемента, если он выбран
            string keyImage;
            if (IsChoosed)
            {
                keyImage = Name + "_Light";
            }
            else
            {
                keyImage = Name + "_Dark";
            }
            SwitchImage(keyImage);
        }

        //Генерация ключа изображения для разных случаев. Все названия изображений элементов должны быть унифицированы
        public string StatusKeyGen (ControlStatus status)
        {
            switch (status)
            {
                case ControlStatus.dark:
                    {
                        return Name + "_Dark";
                    }
                case ControlStatus.light:
                    {
                        return Name + "_Light";
                    }
                default:
                    {
                        return Name + "_Def";
                    }
            }
        }

        public void SaveTracking (int row, int col)
        {
            Row = row;
            Column = col;
        }

        public enum ControlStatus:byte
        {
            light = 0,
            dark = 1
        }
    }
}
