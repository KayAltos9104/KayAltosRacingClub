using KARC.Prefabs.Objects;
using System;
using System.Collections.Generic;
using System.Text;

namespace KARC.Controllers
{
    class StreetCarFabric:CarFabric
    {
        readonly Random rnd = new Random();
        public override Car CreateGeneral()
        {
            string key = String.Format("CivilCar", rnd.Next(1, 5));
            return new Car(key);
        }
        public override Car CreatePlayer()
        {
            string key = "PlayerCar1";
            return new Car(key, "Player");
        }
        public override Car CreatePlayer(string key)//TODO: Мб, потом переделать только под номера
        {            
            return new Car(key, "Player");
        }
        public override Car CreateEnemy()
        {
            string key = String.Format("RacerCar", rnd.Next(1, 4));
            return new Car(key,"EnemyCar");
        }
    }
}
