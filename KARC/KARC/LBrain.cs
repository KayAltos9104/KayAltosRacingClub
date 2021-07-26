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
        public Rectangle[] neighborhood = new Rectangle[4];
        int maxSpeed;
        public LBrain (Car _driver)
        {
            driver = _driver;
            maxSpeed = Game1.rnd.Next(10, 21);
        }
        public Vector2 Act(List<PhysicalObject> _obj)
        {
           
            Vector2 changeSpeed = Vector2.Zero;
            if (!driver.live)
                return driver.Speed;
            if (_obj.Count==0)
                return driver.Speed;
            
            neighborhood[0] = new Rectangle((int)(driver.pos.X), (int)(driver.pos.Y - 3 * driver.hitBox.Height), (int)(driver.hitBox.Width), 3 * driver.hitBox.Height);//Смотрим вверх
            neighborhood[1] = new Rectangle((int)(driver.pos.X+driver.hitBox.Width), (int)(driver.pos.Y), 3*driver.hitBox.Width, (int)(driver.hitBox.Height));//Смотрим вправо
            neighborhood[2] = new Rectangle((int)(driver.pos.X), (int)(driver.pos.Y + driver.hitBox.Height), (int)(driver.hitBox.Width), 4 * driver.hitBox.Height);//Смотрим вниз
            neighborhood[3] = new Rectangle((int)(driver.pos.X - 3*driver.hitBox.Width), (int)(driver.pos.Y), 3 * driver.hitBox.Width, (int)(driver.hitBox.Height));//Смотрим влево
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
                        Rectangle hitBoxThis = new Rectangle((int)futurePosThis.X-5, (int)futurePosThis.Y-5, driver.currentImage.Width+5, driver.currentImage.Height+5);
                        Rectangle hitBoxOther = new Rectangle((int)futurePosOther.X-5, (int)futurePosOther.Y-5, currentObj.currentImage.Width+5, currentObj.currentImage.Height+5);
                        if (hitBoxThis.Intersects(hitBoxOther))
                        {
                            if (dangerArray [direction]< 9-i)
                            {
                                dangerArray[direction] = 9 - i;
                                break;                                
                            }
                        }                            
                    }
                }
                
                //}
            }
            if (dangerArray[0]==0)
            {
                if (driver.orientation == SpriteEffects.None)
                {
                    if (Math.Abs(driver.Speed.Y)<maxSpeed)
                        changeSpeed += new Vector2(0, -driver.acceleration);

                }
                
            }
            else if (dangerArray[0] < 4)
            {
                if (driver.orientation == SpriteEffects.None)
                {

                    changeSpeed += new Vector2(-10, -driver.Speed.Y);

                }
                else
                {
                    changeSpeed += new Vector2(10, dangerArray[2]);
                }
            }
            else
            {
                if (driver.orientation == SpriteEffects.None)
                {
                    
                        changeSpeed += new Vector2(-10, -driver.Speed.Y);
                   
                }
                else
                {
                    changeSpeed += new Vector2(10, dangerArray[2]);
                }
            }
            if (dangerArray[2] == 0)
            {
                if (driver.orientation == SpriteEffects.FlipVertically)
                {

                    if (Math.Abs(driver.Speed.Y) < maxSpeed)
                        changeSpeed += new Vector2(0, driver.acceleration);

                }
            }
            else if (dangerArray[2] < 4)
            {
                if (driver.orientation == SpriteEffects.None)
                {
                    changeSpeed += new Vector2(-10, -dangerArray[2]);

                }
                else
                {

                    changeSpeed += new Vector2(10, -driver.Speed.Y);


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
                    
                        changeSpeed += new Vector2(10, -driver.Speed.Y);
                 

                }
            }
            return driver.Speed+changeSpeed;
        }
    }
}
