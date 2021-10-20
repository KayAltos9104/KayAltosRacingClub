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
            Button btnSelect = new Button("SelectButton");
            btnSelect.AddImage("SelectButton_Dark", ResourcesStorage.GetImage("SelectButton"));
            btnSelect.AddImage("SelectButton_Light", ResourcesStorage.GetImage("SelectButton_Light"));
            btnSelect.InitializeGraphics();
            btnSelect.ChangePlace(new Vector2(100, 100));
            btnSelect.AcceptClick += btnSelect_Click;
            this.AddObject(btnSelect);

            Button btnSelect2 = new Button("SelectButton2");
            btnSelect2.AddImage("SelectButton2_Dark", ResourcesStorage.GetImage("SelectButton"));
            btnSelect2.AddImage("SelectButton2_Light", ResourcesStorage.GetImage("SelectButton_Light"));
            btnSelect2.InitializeGraphics();
            btnSelect2.ChangePlace(new Vector2(100, 300));
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
