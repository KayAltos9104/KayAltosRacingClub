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
        }

        private void btnSelect_Click(object sender, KeyBoardEventArgs e)
        {
            System.Windows.Forms.MessageBox.Show("Заглушка");
        }
    }
}
