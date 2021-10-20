using System;
using System.Collections.Generic;
using System.Text;

namespace KARC.GameObjsTemplates
{
    interface IObjectUI
    {      
        public string Name { get; }
        public bool IsChoosed { get; set; }
    }
}
