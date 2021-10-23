﻿using KARC.ScenesTemplates;
using System;
using System.Collections.Generic;
using System.Text;

namespace KARC.Prefabs.Menu
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
            map[0][150, 50] = (int)ObjectCode.player;
        }
    }
}
