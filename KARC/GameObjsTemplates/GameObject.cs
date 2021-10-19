using KARC.GameObjsTemplates;
using System;
using System.Collections.Generic;
using System.Text;

namespace KARC.GameObjsTemplates
{
    abstract class GameObject: IBehaviour
    {
        public bool Live { get; private set; }
        public virtual void Update ()
        {

        }
    }
}
