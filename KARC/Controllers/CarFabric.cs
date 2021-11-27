using KARC.Prefabs.Objects;
using System;
using System.Collections.Generic;
using System.Text;

namespace KARC.Controllers
{
    abstract class CarFabric
    {
        public abstract Car CreateGeneral();
        public abstract Car CreatePlayer();
        public abstract Car CreatePlayer(string key);
        public abstract Car CreateEnemy();

        public Car CreateIndividual(string key, string name)
        {
            return new Car(key, name);
        }
    }
}
