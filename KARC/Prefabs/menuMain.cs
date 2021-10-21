﻿using System;
using System.Collections.Generic;
using System.Text;
using KARC.ScenesTemplates;
using KARC.GameObjsTemplates.InterfaceObjs;
using Microsoft.Xna.Framework;


namespace KARC.Prefabs
{
    class MenuMain : Menu
    {
        public override void InitializeScene()
        {
            base.InitializeScene();

            _rows = 7;
            _columns = 7;

            Button btnStart = new Button("StartButton");
            btnStart.AddImage(btnStart.StatusKeyGen(Button.ControlStatus.dark), ResourcesStorage.GetImage("StartButton"));
            btnStart.AddImage(btnStart.StatusKeyGen(Button.ControlStatus.light), ResourcesStorage.GetImage("StartButton_Light"));
            btnStart.InitializeGraphics();
            PlaceElement(btnStart, 2, 3);
            btnStart.LayerToFront(0.1f);
            btnStart.AcceptClick += btnStart_Click;
            this.AddObject(btnStart);

            Button btnOptions = new Button("OptionsButton");
            btnOptions.AddImage(btnOptions.StatusKeyGen(Button.ControlStatus.dark), ResourcesStorage.GetImage("OptionsButton"));
            btnOptions.AddImage(btnOptions.StatusKeyGen(Button.ControlStatus.light), ResourcesStorage.GetImage("OptionsButton_Light"));
            btnOptions.InitializeGraphics();
            PlaceElement(btnOptions, 3, 3);
            btnOptions.LayerToFront(0.1f);
            btnOptions.AcceptClick += btnOptions_Click;
            this.AddObject(btnOptions);

            Button btnExit = new Button("ExitButton");
            btnExit.AddImage(btnExit.StatusKeyGen(Button.ControlStatus.dark), ResourcesStorage.GetImage("ExitButton"));
            btnExit.AddImage(btnExit.StatusKeyGen(Button.ControlStatus.light), ResourcesStorage.GetImage("ExitButton_Light"));
            btnExit.InitializeGraphics();
            PlaceElement(btnExit, 4, 3);
            btnExit.LayerToFront(0.1f);
            btnExit.AcceptClick += btnExit_Click;
            this.AddObject(btnExit);

            MenuMainBackGround backGround = new MenuMainBackGround();
            backGround.InitializeGraphics();
            var coef = 1.0f * _windowWidth / backGround.GetImageSize().Item1;
            backGround.Stretch(coef);            
            this.AddObject(backGround);

            this.LoadUI();
        }

        private void btnStart_Click(object sender)
        {
            SceneChangePerform("Level0");
            
        }

        private void btnOptions_Click(object sender)
        {
            SceneChangePerform("Options");
        }
        private void btnExit_Click(object sender)
        {
            SceneChangePerform("Exit");
        }
    }
}
