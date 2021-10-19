using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace KARC.Controllers
{
    static class SceneController
    {
        public static void Update (object sender, KeyBoardEventArgs e)
        {            
            if (e.GetPushedButtons()[0] == Keys.Escape)
                MainCycle.isOff = true;

        }
    }
}
