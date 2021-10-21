using KARC.GameObjsTemplates;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace KARC.GameObjsTemplates
{
    public abstract partial class GameObject: IBehaviour
    {
        public bool ToDelete { get; private set; }//Удалить ли объект на этом шаге игрового цикла
        private Vector2 pos; //Текущая позиция

        public GameObject ()
        {            
            pos = Vector2.Zero;
            ToDelete = false;
        }       

        public virtual void Update ()
        {
           
        }

        public void ChangePlace (Vector2 newPlace)
        {
            pos = newPlace;
        }
       
    }
}
