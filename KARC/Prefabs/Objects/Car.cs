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

        float _acceleration;
        string Name { get;}
        RigidBody _physicalBody;
        bool _player = false;

        public Car ():base()
        {
            _live = true;
            _speedVect = (X:0,Y:0);
            _acceleration = 1.0f;
            Name = "GeneralCar";            
        }         
        public Car (string carKey): this ()
        {
            AddImage(carKey, ResourcesStorage.GetImage(carKey));
            AddImage(String.Concat(carKey,"_crushed"), ResourcesStorage.GetImage(carKey));
            InitializeGraphics();
            PhysicsInit(this.GetImageSize().Item1, this.GetImageSize().Item1);
        }
        public Car (string carKey, string name):this(carKey)
        {
            Name = name.ToLower();
            Name = Name.Trim();
            if (Name.Contains("player"))
                _player = true;
        }

        public void PhysicsInit (int width, int height)
        {
            _physicalBody = new RigidBody(weight: 5000, 
                new Point((int)this.GetPos().X, (int)this.GetPos().Y), new Point(width, height));
        }

        public override void Update()
        {
            if (_speedVect.X > 0)
                _angle = 10;
            else if (_speedVect.X < 0)
                _angle = -10;
            else
                _angle = 0;

            var xNew = GetPos().X + _speedVect.X;
            var yNew = GetPos().Y + _speedVect.Y;
            ChangePlace(new Vector2(xNew, yNew));
            _physicalBody.Move(new Point((int)this.GetPos().X, (int)this.GetPos().Y));

            _angle = 0;
            _speedVect.X = 0;
        }

        public void Accelerate(bool forward)
        {
            if (forward)
                ChangeSpeed((0, _acceleration));
            else
                ChangeSpeed((0, -_acceleration));
        }

        public void Turn (bool right)
        {
            if (right)
                ChangeSpeed((10, 0));
            else
                ChangeSpeed((-10, 0));
        }

        private void ChangeSpeed ((float X, float Y) deltaSpeed)
        {
            _speedVect = (_speedVect.X + deltaSpeed.X, _speedVect.Y + deltaSpeed.Y);
            if (_speedVect.X > 10)
                _speedVect.X = 10;
            else if (_speedVect.X < -10)
                _speedVect.X = -10;

            if (_speedVect.Y > 50)
                _speedVect.Y = 50;
            else if (_speedVect.Y < -50)
                _speedVect.Y = -50;

        }


    }
}
