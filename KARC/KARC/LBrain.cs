using KARC.Logic;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KARC
{
    class LBrain
    {
        Car driver;
        string type;
        public LBrain (Car _driver)
        {
            driver = _driver;
        }
        public Vector2 Act(List<PhysicalObject> _obj)
        {
            Dictionary<PhysicalObject, int> dangerDict = new Dictionary<PhysicalObject, int>();
            Vector2 changeSpeed = Vector2.Zero;
            if (driver.Tag == "Player")
                return driver.Speed;
            foreach (var currentObj in _obj)
            {
                float sumSpeedX = driver.Speed.X + currentObj.Speed.X;
                float sumSpeedY = driver.Speed.Y + currentObj.Speed.Y;
                double distance = driver.CountDistance(currentObj);
                int danger = 4;

                Vector2 futurePosOther = currentObj.pos + new Vector2(currentObj.Speed.X, currentObj.Speed.Y);
                Vector2 futurePosThis = driver.pos + new Vector2(driver.Speed.X, driver.Speed.Y);
                double futureDistance = Math.Sqrt((futurePosOther.X - futurePosThis.X) * (futurePosOther.X - futurePosThis.X) + (futurePosOther.Y - futurePosThis.Y) * (futurePosOther.Y - futurePosThis.Y));

                

                if (futureDistance >= distance)
                    danger = 0;
                else
                {
                    Rectangle hitBoxThis = new Rectangle((int)futurePosThis.X, (int)futurePosThis.Y, driver.currentImage.Width, driver.currentImage.Height);
                    Rectangle hitBoxOther = new Rectangle((int)futurePosOther.X, (int)futurePosOther.Y, currentObj.currentImage.Width, currentObj.currentImage.Height);
                    if (hitBoxThis.Intersects(hitBoxOther))
                        changeSpeed += new Vector2(currentObj.Speed.X, currentObj.Speed.Y);

                }
                //else if (futureDistance < 2*(Math.Sqrt(driver.hitBox.Width* driver.hitBox.Width+ driver.hitBox.Height*driver.hitBox.Height)))

                //else if (futureDistance / distance < 0.8)
                //    danger = 1;
                //else if (futureDistance / distance < 0.5)
                //    danger = 2;
                //else if (futureDistance / distance < 0.2)
                //    danger = 3;
                //else if (futureDistance / distance < 0.1)
                //    danger = 4;


            }
            return driver.Speed+changeSpeed;
            //switch (type)
            //{
            //    case "general":
            //        {

            //            break;
            //        }
            //}

        }
    }
}
