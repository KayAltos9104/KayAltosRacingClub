using System;
using System.Collections.Generic;
using System.Text;

namespace KARC.GameObjsTemplates.InterfaceObjs
{
    class Button:GameObject, IObject
    {
        delegate void ClickHandler(object sender, KeyBoardEventArgs e);
        event ClickHandler AcceptClick;
        
    }
}
