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
           
            Vector2 changeSpeed = Vector2.Zero;
            if (!driver.live)
                return driver.Speed;
            if (_obj.Count==0)
                return driver.Speed;
            int[] neighborhood = new int[4];
            PhysicalObject dangerous = _obj[0];
            int danger = 0;
            
            if (driver.Tag == "Player")
                return driver.Speed;
            foreach (var currentObj in _obj)
            {
                float sumSpeedX = driver.Speed.X + currentObj.Speed.X;
                float sumSpeedY = driver.Speed.Y + currentObj.Speed.Y;
                double distance = driver.CountDistance(currentObj);
                
                for (int i = 1; i <=8;i++)
                {
                    Vector2 futurePosOther = currentObj.pos + new Vector2(currentObj.Speed.X*i, currentObj.Speed.Y*i);
                    Vector2 futurePosThis = driver.pos + new Vector2(driver.Speed.X*i, driver.Speed.Y*i);
                    double futureDistance = Math.Sqrt((futurePosOther.X - futurePosThis.X) * (futurePosOther.X - futurePosThis.X) + (futurePosOther.Y - futurePosThis.Y) * (futurePosOther.Y - futurePosThis.Y));
                    if (futureDistance >= distance)
                    {
                        danger = 0;
                        break;
                    }
                    else
                    {
                        Rectangle hitBoxThis = new Rectangle((int)futurePosThis.X, (int)futurePosThis.Y, driver.currentImage.Width, driver.currentImage.Height);
                        Rectangle hitBoxOther = new Rectangle((int)futurePosOther.X, (int)futurePosOther.Y, currentObj.currentImage.Width, currentObj.currentImage.Height);
                        if (hitBoxThis.Intersects(hitBoxOther))
                        {
                            if (danger<9-i)
                            {
                                danger = 9 - i;
                                dangerous = currentObj;
                            }
                        }                            
                    }
                }
                if (danger==0)
                {

                }
                else
                {
                    if (dangerous.calcCenter().X < driver.calcCenter().X)
                        changeSpeed += new Vector2(5 * danger, 0);
                    else
                        changeSpeed += new Vector2(-5 * danger, 0);
                    if (dangerous.calcCenter().Y < driver.calcCenter().Y)
                        changeSpeed += new Vector2(0, 5 * danger);
                    else
                        changeSpeed += new Vector2(0, -5 * danger);
                }
            }
            return driver.Speed+changeSpeed;
        }
    }
}
