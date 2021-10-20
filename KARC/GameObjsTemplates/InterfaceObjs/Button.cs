using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace KARC.GameObjsTemplates.InterfaceObjs
{
    class Button:GameObject, IObjectUI
    {
        public delegate void ClickHandler(object sender);
        public event ClickHandler AcceptClick;
        
        public bool IsChoosed { get; set; }

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
        
        public void PerformClick()
        {
            AcceptClick.Invoke(this);
        }
        
    }
}
