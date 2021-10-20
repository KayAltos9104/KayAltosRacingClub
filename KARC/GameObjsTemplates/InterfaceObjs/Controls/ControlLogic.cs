using System;
using System.Collections.Generic;
using System.Text;

namespace KARC.GameObjsTemplates.InterfaceObjs.Controls
{
    partial class Control : GameObject, IObjectUI
    {
        public delegate void ClickHandler(object sender);
        public bool IsChoosed { get; set; }
        public string Name { get; private set; }

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

        public void SetPlace (int row, int col)
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
