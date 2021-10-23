using System;
using System.Collections.Generic;
using System.Text;
using KARC.GameObjsTemplates;
namespace KARC.Prefabs
{
    class MenuMainBackGround:BackGround
    {
        private int initTime;
        public MenuMainBackGround ():base()
        {
            initTime = 0;
            AddImage("MenuMainBackGround", ResourcesStorage.GetImage("MenuMainBackGround"));
            this.InitializeGraphics();
        }
        public override void Update()
        {
            if(initTime < 255)
                initTime += 1;
            Animate();
        }      
        protected override void Animate ()
        {
            this.ChangeFilter(initTime, initTime, initTime);
        }
    }
}
