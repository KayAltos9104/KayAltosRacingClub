using System;
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
            PlaceElement(btnStart, 1, 3);
            btnStart.AcceptClick += btnStart_Click;
            this.AddObject(btnStart);

            Button btnOptions = new Button("OptionsButton");
            btnOptions.AddImage(btnOptions.StatusKeyGen(Button.ControlStatus.dark), ResourcesStorage.GetImage("OptionsButton"));
            btnOptions.AddImage(btnOptions.StatusKeyGen(Button.ControlStatus.light), ResourcesStorage.GetImage("OptionsButton_Light"));
            btnOptions.InitializeGraphics();
            PlaceElement(btnOptions, 2, 3);
            btnOptions.AcceptClick += btnOptions_Click;
            this.AddObject(btnOptions);

            Button btnExit = new Button("ExitButton");
            btnExit.AddImage(btnExit.StatusKeyGen(Button.ControlStatus.dark), ResourcesStorage.GetImage("ExitButton"));
            btnExit.AddImage(btnExit.StatusKeyGen(Button.ControlStatus.light), ResourcesStorage.GetImage("ExitButton_Light"));
            btnExit.InitializeGraphics();
            PlaceElement(btnExit, 3, 3);
            btnExit.AcceptClick += btnExit_Click;
            this.AddObject(btnExit);

            this.LoadUI();
        }

        private void btnStart_Click(object sender)
        {
            System.Windows.Forms.MessageBox.Show("Заглушка на начало игры");
        }

        private void btnOptions_Click(object sender)
        {
            System.Windows.Forms.MessageBox.Show("Заглушка на опции");
        }
        private void btnExit_Click(object sender)
        {
            System.Windows.Forms.MessageBox.Show("Заглушка на выход");
        }
    }
}
