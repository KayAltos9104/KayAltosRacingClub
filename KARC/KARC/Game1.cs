using KARC.Logic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;

namespace KARC
{

    enum GameMode
    {
        mainMenu,
        game,
        final
    }



    public enum objType : byte
    {
        background = 0,
        button = 1,
        car = 2
    }


    public class Game1 : Game
    {
        int load = 0;//Время загрузки заставки

        

        Dictionary<string, Texture2D> texturesDict;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        GameMode mode;
        Song song;
        Song[] musicList;
        Dictionary<string, Logic.Scene> scenesDict = new Dictionary<string, Logic.Scene>();

        public static int windoWidth;
        public static int windowHeight;

        string titleLoad = "";
        int nameIndex = 1;

        int currentTime = 0;
        bool songSwitched = false;

        bool showhitBox = false;
        public static int playerId;
        bool pushed = false;
        int currentTimeforAccel;
        int periodForAccel;

        SpriteFont gameFont;
        bool initial=true;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content/Resources";
            texturesDict = new Dictionary<string, Texture2D>();
            mode = GameMode.mainMenu;
        }




        protected override void Initialize()
        {
            base.Initialize();
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferWidth = 840;
            //graphics.PreferredBackBufferHeight = 768;
            graphics.PreferredBackBufferHeight = 700;
            graphics.ApplyChanges();

            windoWidth = Window.ClientBounds.Width;
            windowHeight = Window.ClientBounds.Height;

            musicList = new Song[2];
            musicList[0] = Content.Load<Song>("ME");
            musicList[1] = Content.Load<Song>("DemonSpeeding");


            currentTimeforAccel = 0;
            periodForAccel = 500;

            //===================Загрузка начального экрана
            int[,] map = new int[1, 1];

            List<Logic.Object> objList = new List<Logic.Object>();
            Dictionary<string, Texture2D> textureDict = new Dictionary<string, Texture2D>();
            textureDict.Add("background", Content.Load<Texture2D>("MenuBackGround"));

            SpriteFont gameName = Content.Load<SpriteFont>("Title");
            Dictionary<string, SpriteFont> fontDict = new Dictionary<string, SpriteFont>();
            fontDict.Add("Title", gameName);
            fontDict.Add("ManualFont", Content.Load<SpriteFont>("ManualFont"));

           


            Logic.BackGround backGround = new Logic.BackGround(Vector2.Zero, 1.0f, textureDict, 1, fontDict);
            objList.Add(backGround);

            //TODO: период лучше в сцену вставлять. Или туда и туда
            textureDict = new Dictionary<string, Texture2D>();
            textureDict.Add("light", Content.Load<Texture2D>("Start_Select"));
            textureDict.Add("dark", Content.Load<Texture2D>("StartButton"));
            Logic.Button btnStart = new Logic.Button(new Vector2(windoWidth / 2 - 30, windowHeight / 2 - 100), 0.9f, textureDict, 2, 0);
            //Logic.Button btnStart = new Logic.Button(new Vector2(100, 100), 0.9f, textureDict, 2, 0, 50);
            btnStart.check = true;
            objList.Add(btnStart);


            textureDict = new Dictionary<string, Texture2D>();
            textureDict.Add("light", Content.Load<Texture2D>("Exit_Select"));
            textureDict.Add("dark", Content.Load<Texture2D>("Exit"));
            Logic.Button btnExit = new Logic.Button(new Vector2(windoWidth / 2 - 30, windowHeight / 2 - 40), 0.9f, textureDict, 3, 1);
            objList.Add(btnExit);

            Logic.InterfaceMenu mainMenu = new Logic.InterfaceMenu(map, 600, objList, 100);
            //mainMenu.song = Content.Load<Song>("ME");
            //song = Content.Load<Song>("ME");
            scenesDict.Add("MainMenu", mainMenu);
            //==============================Конец




            //===================Загрузка игры

            //Тестовый уровень
            //map =  new int[1, 10] { { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 } };
            map = new int[1, 10] { { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 } };
            objList.Clear();
            textureDict.Clear();
            textureDict.Add("Road", Content.Load<Texture2D>("mapTiles/Road1"));
           
            for (int i = 0; i < map.GetLength(1);i++)
            {
                if (map[0,i]!=0)
                {
                    objList.Add(new Logic.BackGround(new Vector2(0,i * 800), 0.9f, textureDict, 0));
                }
            }

            Animation carExplosion = new Animation(Content.Load<Texture2D>("Animations/boom3"), 128, 128, new Point(8, 8), Vector2.Zero, false);
            carExplosion.scale = 2.0f;
            SoundEffect explosionSound = Content.Load<SoundEffect>("Sound/DeathFlash");


            textureDict.Clear();
            textureDict.Add("MainModel", Content.Load<Texture2D>("carModels/Model1"));
            textureDict.Add("CrushedModel", Content.Load<Texture2D>("carModels/Model1_Crushed"));
            Logic.Car Player = new Logic.Car(new Vector2(420, -800-200 ), 0.2f, textureDict, 1, new Vector2(0, 0), 5000);
            Player.player = true;
            Player.animationDict.Add("explosion", carExplosion);
            Player.soundEffectsDict.Add("explosion",explosionSound);
            objList.Add(Player);
           

            textureDict.Clear();
            textureDict.Add("MainModel2", Content.Load<Texture2D>("carModels/Model2"));
            textureDict.Add("CrushedModel2", Content.Load<Texture2D>("carModels/Model2_Crushed"));
            textureDict.Add("MainModel3", Content.Load<Texture2D>("carModels/Model3"));
            textureDict.Add("CrushedModel3", Content.Load<Texture2D>("carModels/Model3_Crushed"));
            textureDict.Add("MainModel4", Content.Load<Texture2D>("carModels/Model4"));
            textureDict.Add("CrushedModel4", Content.Load<Texture2D>("carModels/Model4_Crushed"));
            textureDict.Add("MainModel5", Content.Load<Texture2D>("carModels/Model5"));
            textureDict.Add("CrushedModel5", Content.Load<Texture2D>("carModels/Model5_Crushed"));
            textureDict.Add("MainModel6", Content.Load<Texture2D>("carModels/Model6"));
            textureDict.Add("CrushedModel6", Content.Load<Texture2D>("carModels/Model6_Crushed"));
            textureDict.Add("MainModel7", Content.Load<Texture2D>("carModels/Model7"));
            textureDict.Add("CrushedModel7", Content.Load<Texture2D>("carModels/Model7_Crushed"));
            textureDict.Add("MainModel8", Content.Load<Texture2D>("carModels/Model8"));
            textureDict.Add("CrushedModel8", Content.Load<Texture2D>("carModels/Model8_Crushed"));
            
            textureDict.Add("explosion", Content.Load<Texture2D>("Animations/boom3"));
            //textureDict.Add("MainModel", Content.Load<Texture2D>("carModels/Model2"));
            //textureDict.Add("CrushedModel", Content.Load<Texture2D>("carModels/Model2_Crushed"));
            //Car enemy = new Logic.Car(new Vector2(420, -800 - 400), 0.2f, textureDict, 1, new Vector2(0, 0), 5000);

            //carExplosion = new Animation(Content.Load<Texture2D>("Animations/boom3"), 128, 128, new Point(8, 8), Vector2.Zero, false);
            //carExplosion.scale = 2.0f;

            //enemy.animationDict.Add("explosion", carExplosion);
            //explosionSound = Content.Load<SoundEffect>("Sound/DeathFlash");
            //enemy.soundEffectsDict.Add("explosion", explosionSound);

            //objList.Add(enemy);




            Logic.Level testLevel = new Logic.Level(map, 800, objList, true, textureDict);
            scenesDict.Add("level0", testLevel);
            //==============================Конец

            gameFont = Content.Load<SpriteFont>("ManualFont");
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            song = Content.Load<Song>("ME");
            if (mode == GameMode.mainMenu)
            {
                MediaPlayer.Play(song);
                // повторять после завершения
                MediaPlayer.IsRepeating = true;               
            }   
        }


        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            switch (mode)
            {
                case GameMode.mainMenu:
                    {

                        if (Keyboard.GetState().IsKeyDown(Keys.Up))
                        {
                            scenesDict["MainMenu"].updateScene(Keys.Up, gameTime.ElapsedGameTime.Milliseconds);
                        }
                        else if (Keyboard.GetState().IsKeyDown(Keys.Down))
                        {
                            scenesDict["MainMenu"].updateScene(Keys.Down, gameTime.ElapsedGameTime.Milliseconds);
                        }
                        else
                            scenesDict["MainMenu"].updateScene(gameTime.ElapsedGameTime.Milliseconds);
                        
                        if (Keyboard.GetState().IsKeyDown(Keys.Space) || Keyboard.GetState().IsKeyDown(Keys.Enter))
                        {
                            Logic.InterfaceMenu menu = (Logic.InterfaceMenu)scenesDict["MainMenu"];
                            switch (menu.cursor)
                            {
                                case 0:
                                    {
                                        mode = GameMode.game;
                                        break;
                                    }
                                case 1:
                                    {
                                        this.Exit();
                                        break;
                                    }
                            }

                        }

                        //if (Keyboard.GetState().IsKeyDown(Keys.Right))
                        //{
                        //    scenesDict["MainMenu"].scroll(new Vector2(1, 1));
                        //}
                        
                        break;
                    }
                case GameMode.game:
                    {
                        //Управление машинкой========================================================
                        if (initial)
                        {
                            scenesDict["level0"].scroll(new Vector2(0, -840*8));
                            scenesDict["level0"].objectList[playerId].pos.Y = 400;
                            initial = false;
                        }
                        
                        Logic.Car Player = (Logic.Car)scenesDict["level0"].objectList[playerId];
                        if (Player.live)
                        {
                            if (Keyboard.GetState().IsKeyDown(Keys.LeftControl))
                            {
                                if (showhitBox)
                                    showhitBox = false;
                                else
                                    showhitBox = true;
                            }

                            if (Keyboard.GetState().IsKeyDown(Keys.Up))
                            {
                                //scenesDict["level0"].objectList[playerId].pos.Y -= 1;
                                if (!pushed)
                                {
                                    Player.Speed += new Vector2(0, -Player.acceleration);
                                    pushed = true;
                                }

                            }

                            //if (Keyboard.GetState().IsKeyDown(Keys.Down))
                            //{                           
                            //    if (!pushed)
                            //    {
                            //        Player.Speed += new Vector2(0, Player.acceleration);
                            //        pushed = true;
                            //    }
                            //}
                            if (Keyboard.GetState().IsKeyDown(Keys.Right))
                            {
                                if (Player.Speed.Y != 0)
                                {
                                    Player.Speed += new Vector2(Player.maneuver, 0);
                                }
                            }

                            if (Keyboard.GetState().IsKeyDown(Keys.Left))
                            {
                                if (Player.Speed.Y != 0)
                                {
                                    Player.Speed += new Vector2(-Player.maneuver, 0);
                                }
                            }
                        }
                       

                        //Управление машинкой===========Конец=============================================
                        //Отжатие ускорения===============================================================
                        currentTimeforAccel += gameTime.ElapsedGameTime.Milliseconds;
                        if (currentTimeforAccel > periodForAccel)
                        {
                            pushed = false;
                            currentTimeforAccel -= periodForAccel;

                        }
                        //Обработка столкновений=============================================================
                       
                        foreach (var object1 in scenesDict["level0"].objectList)
                        {
                            if (object1.Value.physical&& object1.Value.pos.Y<1000&&object1.Value.pos.Y>-1000)
                            {
                                foreach (var object2 in scenesDict["level0"].objectList)
                                {
                                    if (object2.Value.physical&& object1.Key!=object2.Key)
                                    {
                                        Logic.PhysicalObject obj1 = (Logic.PhysicalObject)object1.Value;
                                        Logic.PhysicalObject obj2 = (Logic.PhysicalObject)object2.Value;

                                        if (obj1.CheckNeighborhood(obj2))
                                            obj1.collision(obj2);
                                    }
                                }
                            }                            
                        }


                        //for (int i =1; i <= scenesDict["level0"].objectList.Count; i++)
                        //{
                        //    if (scenesDict["level0"].objectList.ContainsKey(i) && scenesDict["level0"].objectList[i].physical)
                        //    {
                        //        for (int j = i+1; j <= scenesDict["level0"].objectList.Count; j++)
                        //        {
                        //            if (scenesDict["level0"].objectList.ContainsKey(j)&& scenesDict["level0"].objectList[j].physical)
                        //            //&& scenesDict["level0"].objectList[i].id!= scenesDict["level0"].objectList[j].id)
                        //            {
                        //                Logic.PhysicalObject obj1 = (Logic.PhysicalObject)scenesDict["level0"].objectList[i];
                        //                Logic.PhysicalObject obj2 = (Logic.PhysicalObject)scenesDict["level0"].objectList[j];

                        //                if (obj1.CheckNeighborhood(obj2))
                        //                    obj1.collision(obj2);
                        //            }
                        //        }
                        //    }
                            
                        //    //scenesDict["level0"].objectList[i].Update(gameTime.ElapsedGameTime.Milliseconds);                            

                        //}
                        //pushed = false;
                        //Logic.Level upd = (Logic.Level)scenesDict["level0"];
                        scenesDict["level0"].updateScene(gameTime.ElapsedGameTime.Milliseconds);
                        Vector2 scrollVector = new Vector2(0, -Player.Speed.Y);
                        scenesDict["level0"].scroll(scrollVector);//Скроллинг
                        

                        break;
                    }
            }

            base.Update(gameTime);
        }




        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin(SpriteSortMode.BackToFront);

            //spriteBatch.Begin();

            switch (mode)
            {

                case GameMode.mainMenu:
                    {
                        int period = 50;
                        currentTime += gameTime.ElapsedGameTime.Milliseconds;
                       
                        string gameName = "           K.A.R.C.\n Adrenaline Racing";
                        foreach (var obj in scenesDict["MainMenu"].objectList)
                        {
                            
                                obj.Value.colDraw = new Color(load, load, load);
                                obj.Value.drawObject(spriteBatch, gameTime.ElapsedGameTime.Milliseconds);
                            
                            
                            if (load >= 255)
                            {
                                Logic.BackGround title = (Logic.BackGround)scenesDict["MainMenu"].objectList[1];
                                title.drawString("Title", titleLoad, new Vector2(windoWidth / 2 - 165, windowHeight / 2 - 220), new Color(load, 0, 0), spriteBatch);


                            }


                        }
                        if (titleLoad != gameName)
                        {
                            if (currentTime > period)
                            {
                                currentTime = 0;
                                if (load < 255)
                                {
                                    load += 3;
                                }
                                else
                                {
                                    if (nameIndex <= gameName.Length)
                                    {
                                        titleLoad = gameName.Substring(0, nameIndex);
                                        nameIndex++;
                                    }
                                }

                            }
                        }
                        else
                        {
                            period = 300;
                            if (currentTime < period)
                            {

                            }
                            else if (currentTime > period && currentTime < 2 * period)
                            {
                                Logic.BackGround pressStart = (Logic.BackGround)scenesDict["MainMenu"].objectList[1];
                                pressStart.drawString("ManualFont", "Нажмите пробел для выбора", new Vector2(windoWidth / 2 - 120, windowHeight - 300), Color.FloralWhite, spriteBatch);

                            }
                            else
                            {
                                currentTime = 0;
                            }
                        }




                        break;

                    }
                case GameMode.game:
                    {
                        

                        if (!songSwitched)
                        {
                            MediaPlayer.Play(musicList[1]);
                            songSwitched = true;
                        }
                        else
                        {
                            MediaPlayer.Resume();
                            MediaPlayer.Volume = 1.0f;
                        }


                        foreach (var obj in scenesDict["level0"].objectList)
                        {                 
                            if (obj.Value.pos.Y>-1500&& obj.Value.pos.Y<1500)           
                            obj.Value.drawObject(spriteBatch, gameTime.ElapsedGameTime.Milliseconds);
                            if (showhitBox&& obj.Value.physical)
                            {
                                Logic.PhysicalObject hb = (Logic.PhysicalObject)obj.Value;
                                spriteBatch.Draw(Content.Load<Texture2D>("hitBoxBlank"), hb.hitBox, null, Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, 0.3f);
                            }
                        }

                        Logic.Car Player = (Logic.Car)scenesDict["level0"].objectList[playerId];
                        spriteBatch.DrawString(gameFont, "Скорость: " + (-Player.Speed.Y)/*Player.Speed.Length()*/, Vector2.Zero, Color.Yellow);
                        spriteBatch.DrawString(gameFont, "Управление:\nСтрелки - движение \nLeftCtrl - Показать хитбоксы", new Vector2(0,500), Color.Red);



                        break;
                    }
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
