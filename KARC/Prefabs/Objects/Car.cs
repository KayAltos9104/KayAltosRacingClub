using KARC.GameObjsTemplates;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace KARC.Prefabs.Objects
{
    class Car:GameObject
    {
        bool _live;
        (float X, float Y) _speedVect;
        string Name { get;}
        RigidBody _physicalBody;

        public Car ():base()
        {
            _live = true;
            _speedVect = (X:0,Y:0);
            Name = "GeneralCar";            
        }

        public void PhysicsInit (int width, int height)
        {
            _physicalBody = new RigidBody(weight: 5000, 
                new Point((int)this.GetPos().X, (int)this.GetPos().Y), new Point(width, height));
        }

        public override void Update()
        {            
            var xNew = GetPos().X + _speedVect.X;
            var yNew = GetPos().Y + _speedVect.Y;
            ChangePlace(new Vector2(xNew, yNew));
            _physicalBody.Move(new Point((int)this.GetPos().X, (int)this.GetPos().Y));
        }


    }
}
