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

        public delegate void push(object sender, ButtonEventArgs e);

        public event push Push;
        public string orderKey;

        public Button (Vector2 _pos, float _layer, Dictionary<string, Texture2D> _loadTextList, Scene parentScene) :base(_pos, _layer, _loadTextList, parentScene)
        {            
            period = 50;
        }

        public override void Update(int _time)
        {            
                if (check)
                    currentImage = images["light"];
                else
                    currentImage = images["dark"];  
        }
        public void PushButton()
        {
            Push.Invoke(this, new ButtonEventArgs());
        }
    }

    class ButtonEventArgs
    {

    }
}
