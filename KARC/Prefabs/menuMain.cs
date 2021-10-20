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

            _rows = 3;
            _columns = 3;

            Button btnSelect = new Button("SelectButton");
            btnSelect.AddImage(btnSelect.StatusKeyGen(Button.ControlStatus.dark), ResourcesStorage.GetImage("SelectButton"));
            btnSelect.AddImage(btnSelect.StatusKeyGen(Button.ControlStatus.light), ResourcesStorage.GetImage("SelectButton_Light"));
            btnSelect.InitializeGraphics();
            PlaceElement(btnSelect, 0, 0);
            btnSelect.AcceptClick += btnSelect_Click;
            this.AddObject(btnSelect);

            Button btnSelect2 = new Button("SelectButton2");
            btnSelect2.AddImage(btnSelect2.StatusKeyGen(Button.ControlStatus.dark), ResourcesStorage.GetImage("SelectButton"));
            btnSelect2.AddImage(btnSelect2.StatusKeyGen(Button.ControlStatus.light), ResourcesStorage.GetImage("SelectButton_Light"));
            btnSelect2.InitializeGraphics();
            PlaceElement(btnSelect2, 1, 2);
            btnSelect2.AcceptClick += btnSelect_Click;
            this.AddObject(btnSelect2);

            this.LoadUI();
        }

        private void btnSelect_Click(object sender)
        {
            System.Windows.Forms.MessageBox.Show("Заглушка");
        }
    }
}
