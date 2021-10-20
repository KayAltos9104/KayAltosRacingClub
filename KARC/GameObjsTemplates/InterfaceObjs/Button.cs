using KARC.GameObjsTemplates.InterfaceObjs.Controls;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace KARC.GameObjsTemplates.InterfaceObjs
{
    class Button:Control, IObjectUI
    {
        
        public event ClickHandler AcceptClick;
        
      
        public Button (string name) : base (name)
        {

        }        
        
        
        public void PerformClick()
        {
            AcceptClick.Invoke(this);
        }
        
    }
}
