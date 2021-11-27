using KARC.Controllers;
using KARC.ScenesTemplates;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace KARC.Prefabs.Scenes
{
    class Level0:Level
    {
        public override void InitializeScene()
        {
            base.InitializeScene();
            map = new int[1][,];
            map[0] = new int[300, 100];
            scale = (width: _windowWidth / 300, height: _windowHeight / 100);
            map[0][150, 90] = (int)ObjectCode.player;
            map[0][150, 50] = (int)ObjectCode.civilCar;
            background = new int[map.Length];
            InitializeMap();
        }

        protected override void GenerateFabric()
        {
            fabric = new StreetCarFabric(); 
        }

        protected override void ButtonPush(object sender, KeyBoardEventArgs e)//TODO: Перенести клавиатуру на контроллер?
        {
            switch (e.GetPushedButtons()[0])
            {
                case Keys.Left:
                    {
                        
                        break;
                    }
            }
        }
    }
}
