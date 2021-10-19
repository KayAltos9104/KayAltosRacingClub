using KARC.GameObjsTemplates;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace KARC.GameObjsTemplates
{
    public abstract partial class GameObject: IBehaviour
    {
        public bool ToDelete { get; private set; }
        private Vector2 pos; //Текущая позиция

        public GameObject ()
        {
            InitializeGraphics();
        }

        public virtual void Update ()
        {
           
        }

        public void Teleport (Vector2 newPlace)
        {
            pos = newPlace;
        }
       
    }
}
