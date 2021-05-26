using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KARC.Logic
{
    class Button:Object
    {
        bool check;//Наведен ли курсор на кнопку
        int tabIndex;

        public override void Update(int _time)
        {
            if (check)
                currentImage = images["light"];
        }

        private
    }
}
