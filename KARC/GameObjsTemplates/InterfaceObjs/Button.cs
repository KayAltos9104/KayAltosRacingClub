using System;
using System.Collections.Generic;
using System.Text;

namespace KARC.GameObjsTemplates.InterfaceObjs
{
    class Button:GameObject, IObject
    {
        public delegate void ClickHandler(object sender, KeyBoardEventArgs e);
        public event ClickHandler AcceptClick;
        public event ClickHandler Choosed;
        bool IsChoosed { get; set; }

        public string Name { get; private set; }

        public Button (string name)
        {
            Name = name;
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
        
    }
}
