using KARC.Logic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
            Rectangle[] neighborhood = new Rectangle[4];
            neighborhood[0] = new Rectangle((int)driver.pos.X, (int)(driver.pos.Y + 3 * driver.hitBox.Height), driver.hitBox.Width, 3 * driver.hitBox.Height);//Смотрим вверх
            neighborhood[1] = new Rectangle((int)(driver.pos.X+driver.hitBox.Width), (int)driver.pos.Y, 3*driver.hitBox.Width, driver.hitBox.Height);//Смотрим вправо
            neighborhood[2] = new Rectangle((int)driver.pos.X, (int)(driver.pos.Y - driver.hitBox.Height), driver.hitBox.Width, 4 * driver.hitBox.Height);//Смотрим вниз
            neighborhood[3] = new Rectangle((int)(driver.pos.X - 3*driver.hitBox.Width), (int)driver.pos.Y, 3 * driver.hitBox.Width, driver.hitBox.Height);//Смотрим влево
            int[] dangerArray = new int[4];
            PhysicalObject dangerous = _obj[0];
            int danger = 0;
            
            if (driver.Tag == "Player")
                return driver.Speed;
            foreach (var currentObj in _obj)
            {
                int direction = 0;
                while (!currentObj.hitBox.Intersects(neighborhood[direction]))
                {
                    direction++;
                    if (direction == neighborhood.Length)
                    {
                        
                        break;
                    }

                }
                if (direction == neighborhood.Length)
                    break;
                float sumSpeedX = driver.Speed.X + currentObj.Speed.X;
                float sumSpeedY = driver.Speed.Y + currentObj.Speed.Y;
                double distance = driver.CountDistance(currentObj);
                
                for (int i = 1; i <=8;i++)
                {
                    Vector2 futurePosOther = currentObj.pos + new Vector2(currentObj.Speed.X*i*3, currentObj.Speed.Y*i*3);
                    Vector2 futurePosThis = driver.pos + new Vector2(driver.Speed.X*i*3, driver.Speed.Y*i*3);
                    double futureDistance = Math.Sqrt((futurePosOther.X - futurePosThis.X) * (futurePosOther.X - futurePosThis.X) + (futurePosOther.Y - futurePosThis.Y) * (futurePosOther.Y - futurePosThis.Y));
                    if (futureDistance >= distance)
                    {
                        dangerArray[direction] = 0;
                        break;
                    }
                    else
                    {
                        Rectangle hitBoxThis = new Rectangle((int)futurePosThis.X, (int)futurePosThis.Y, driver.currentImage.Width, driver.currentImage.Height);
                        Rectangle hitBoxOther = new Rectangle((int)futurePosOther.X, (int)futurePosOther.Y, currentObj.currentImage.Width, currentObj.currentImage.Height);
                        if (hitBoxThis.Intersects(hitBoxOther))
                        {
                            if (dangerArray [direction]< 9-i)
                            {
                                dangerArray[direction] = 9 - i;
                                //dangerous = currentObj;
                                
                            }
                        }                            
                    }
                }
                
                //}
            }
            if (dangerArray[0] < 4)
            {
                if (driver.orientation == SpriteEffects.None)
                {
                    changeSpeed += new Vector2(0, dangerArray[0]);
                }
                else
                {
                    changeSpeed += new Vector2(dangerArray[0], dangerArray[0]*2);
                }
            }
            else
            {
                if (driver.orientation == SpriteEffects.None)
                {
                    //int seed = Game1.rnd.Next(0, 2);
                    //if (seed == 0)
                        changeSpeed += new Vector2(-10, -driver.Speed.Y);
                   // else
                   //     changeSpeed += new Vector2(-10, 0);
                }
                else
                {
                    changeSpeed += new Vector2(10, -driver.Speed.Y);
                }
            }

            if (dangerArray[2] < 4)
            {
                if (driver.orientation == SpriteEffects.None)
                {
                    changeSpeed += new Vector2(-dangerArray[2], -dangerArray[2]);
                }
                else
                {
                    changeSpeed += new Vector2(dangerArray[2], dangerArray[2]);

                }
            }
            else
            {
                if (driver.orientation == SpriteEffects.None)
                {
                    changeSpeed += new Vector2(-10, -dangerArray[2]);

                }
                else
                {
                    //int seed = Game1.rnd.Next(0, 2);
                    //if (seed == 0)
                        changeSpeed += new Vector2(10, -driver.Speed.Y);
                   // else
                   //     changeSpeed += new Vector2(10, 0);

                }
            }
            return driver.Speed+changeSpeed;
        }
    }
}
