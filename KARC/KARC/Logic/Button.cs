using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KARC.Logic
{
    class Button:Object
    {
        public bool check;//Наведен ли курсор на кнопку
        public int tabIndex;

        public Button (Vector2 _pos, float _layer, Dictionary<string, Texture2D> _loadTextList, int _Id, int _tabIndex) :base(_pos, _layer, _loadTextList,_Id)
        {
            tabIndex = _tabIndex;
            type = objType.button;
            period = 50;
        }

        public override void Update(int _time)
        {
            
                if (check)
                    currentImage = images["light"];
                else
                    currentImage = images["dark"];
            
            
        }
        

    }
}
