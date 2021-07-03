using KARC.Logic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;

namespace KARC
{

    public enum GameMode
    {
        mainMenu,
        options,
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

        public static GameMode mode;
        string currentSceneKey;
        Scene currentScene;

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
        bool initial = true;

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
            //graphics.PreferredBackBufferWidth = 840;
            //graphics.PreferredBackBufferHeight = 700;
            graphics.PreferredBackBufferWidth = 840;
            graphics.PreferredBackBufferHeight = 750;
            //graphics.PreferredBackBufferHeight = 768;

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

            List<Object> objList = new List<Object>();
            Dictionary<string, Texture2D> textureDict = new Dictionary<string, Texture2D>();
            textureDict.Add("background", Content.Load<Texture2D>("MenuBackGround"));

            SpriteFont gameName = Content.Load<SpriteFont>("Title");
            Dictionary<string, SpriteFont> fontDict = new Dictionary<string, SpriteFont>();
            fontDict.Add("Title", gameName);
            fontDict.Add("ManualFont", Content.Load<SpriteFont>("ManualFont"));

            BackGround backGround = new BackGround(Vector2.Zero, 1.0f, textureDict, 1, false, fontDict);
            objList.Add(backGround);

            //TODO: период лучше в сцену вставлять. Или туда и туда
            textureDict = new Dictionary<string, Texture2D>();
            textureDict.Add("light", Content.Load<Texture2D>("Start_Select"));
            textureDict.Add("dark", Content.Load<Texture2D>("StartButton"));
            Button btnStart = new Button(new Vector2(windoWidth / 2 - 30, windowHeight / 2 - 100), 0.9f, textureDict, 2, 0);
            btnStart.check = true;
            objList.Add(btnStart);

            textureDict = new Dictionary<string, Texture2D>();
            textureDict.Add("light", Content.Load<Texture2D>("Options_Select"));
            textureDict.Add("dark", Content.Load<Texture2D>("OptionsButton"));
            Button btnOptions = new Button(new Vector2(windoWidth / 2 - 30, windowHeight / 2 - 40), 0.9f, textureDict, 3, 1);
            objList.Add(btnOptions);


            textureDict = new Dictionary<string, Texture2D>();
            textureDict.Add("light", Content.Load<Texture2D>("Exit_Select"));
            textureDict.Add("dark", Content.Load<Texture2D>("Exit"));
            Button btnExit = new Button(new Vector2(windoWidth / 2 - 30, windowHeight / 2 + 20), 0.9f, textureDict, 4, 2);
            objList.Add(btnExit);

            InterfaceMenu mainMenu = new InterfaceMenu(map, 600, objList, 100);
            //mainMenu.song = Content.Load<Song>("ME");
            //song = Content.Load<Song>("ME");
            scenesDict.Add("MainMenu", mainMenu);
            //==============================Конец

            //Загрузка экрана опций
            map = new int[1, 1];

            objList = new List<Object>();
            textureDict = new Dictionary<string, Texture2D>();
            textureDict.Add("background", Content.Load<Texture2D>("Garage_Options"));

            gameName = Content.Load<SpriteFont>("Title");
            fontDict = new Dictionary<string, SpriteFont>();
            fontDict.Add("Title", gameName);
            fontDict.Add("ManualFont", Content.Load<SpriteFont>("ManualFont"));

            backGround = new BackGround(Vector2.Zero, 1.0f, textureDict, 1, true,fontDict);
            objList.Add(backGround);

            //TODO: период лучше в сцену вставлять. Или туда и туда
            //textureDict = new Dictionary<string, Texture2D>();
            //textureDict.Add("light", Content.Load<Texture2D>("Start_Select"));
            //textureDict.Add("dark", Content.Load<Texture2D>("StartButton"));
            //Button btnStart = new Button(new Vector2(windoWidth / 2 - 30, windowHeight / 2 - 100), 0.9f, textureDict, 2, 0);
            //btnStart.check = true;
            //objList.Add(btnStart);

            //textureDict = new Dictionary<string, Texture2D>();
            //textureDict.Add("light", Content.Load<Texture2D>("Options_Select"));
            //textureDict.Add("dark", Content.Load<Texture2D>("OptionsButton"));
            //Button btnOptions = new Button(new Vector2(windoWidth / 2 - 30, windowHeight / 2 - 40), 0.9f, textureDict, 3, 1);
            //objList.Add(btnOptions);


            //textureDict = new Dictionary<string, Texture2D>();
            //textureDict.Add("light", Content.Load<Texture2D>("Exit_Select"));
            //textureDict.Add("dark", Content.Load<Texture2D>("Exit"));
            //Button btnExit = new Button(new Vector2(windoWidth / 2 - 30, windowHeight / 2 + 20), 0.9f, textureDict, 4, 2);
            //objList.Add(btnExit);

            InterfaceMenu Options = new InterfaceMenu(map, 600, objList, 100);
            //mainMenu.song = Content.Load<Song>("ME");
            //song = Content.Load<Song>("ME");
            scenesDict.Add("Options", Options);

            //Загрузка игры
            LoadLevel();

            currentSceneKey = "MainMenu";
            currentScene = scenesDict[currentSceneKey];


        }

        private void LoadLevel()
        {
            //Тестовый уровень            
            int[,] map = new int[1, 10] { { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 } };
            List<Object> objList = new List<Object>();
            Dictionary<string, Texture2D> textureDict = new Dictionary<string, Texture2D>();
            scenesDict.Remove("level0");

            objList.Clear();
            textureDict.Clear();
            textureDict.Add("Road", Content.Load<Texture2D>("mapTiles/Road1"));

            for (int i = 0; i < map.GetLength(1); i++)
            {
                if (map[0, i] != 0)
                {
                    objList.Add(new BackGround(new Vector2(0, i * 800), 0.9f, textureDict, 0, false));
                }
            }

            Animation carExplosion = new Animation(Content.Load<Texture2D>("Animations/boom3"), 128, 128, new Point(8, 8), Vector2.Zero, false);
            carExplosion.scale = 2.0f;
            SoundEffect explosionSound = Content.Load<SoundEffect>("Sound/DeathFlash");

            //Генерация игрока
            textureDict.Clear();
            textureDict.Add("MainModel", Content.Load<Texture2D>("carModels/Model1"));
            textureDict.Add("CrushedModel", Content.Load<Texture2D>("carModels/Model1_Crushed"));
            Car Player = new Logic.Car(new Vector2(420, -800 - 200), 0.2f, textureDict, 1, new Vector2(0, 0), 5000);
            Player.player = true;
            Player.animationDict.Add("explosion", carExplosion);
            Player.soundEffectsDict.Add("explosion", explosionSound);
            objList.Add(Player);

            //Загрузка текстур моделей машин
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

            Level testLevel = new Level(map, 800, objList, true, textureDict);
            scenesDict.Add("level0", testLevel);

            gameFont = Content.Load<SpriteFont>("ManualFont");
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            song = Content.Load<Song>("ME");
            if (mode == GameMode.mainMenu)
            {
                //MediaPlayer.Play(song);
                // повторять после завершения
                //MediaPlayer.IsRepeating = true;               
            }
        }


        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            currentScene = scenesDict[currentSceneKey];

            switch (currentSceneKey)
            {
                case "MainMenu":
                    {
                        InterfaceMenu currentForm = (InterfaceMenu)currentScene;
                        if (Keyboard.GetState().IsKeyDown(Keys.Up))
                        {
                            currentForm.updateScene(Keys.Up, gameTime.ElapsedGameTime.Milliseconds);
                        }
                        else if (Keyboard.GetState().IsKeyDown(Keys.Down))
                        {
                            currentForm.updateScene(Keys.Down, gameTime.ElapsedGameTime.Milliseconds);
                        }
                        else
                            currentForm.updateScene(gameTime.ElapsedGameTime.Milliseconds);

                        if (Keyboard.GetState().IsKeyDown(Keys.Space) || Keyboard.GetState().IsKeyDown(Keys.Enter))
                        {
                            switch (currentForm.cursor)
                            {
                                case 0:
                                    {
                                        //mode = GameMode.game;
                                        load = 0;
                                        currentSceneKey = "level0";
                                        currentScene = scenesDict[currentSceneKey];
                                        
                                        break;
                                    }
                                case 1:
                                    {
                                        load = 0;
                                        currentSceneKey = "Options";
                                        currentScene = scenesDict[currentSceneKey];
                                        break;
                                    }
                                case 2:
                                    {
                                        this.Exit();
                                        break;
                                    }
                            }

                        }

                        break;
                    }
                case "Options":
                    {

                        break;
                    }
                case "level0":
                    {
                        Level currentLevel = (Level)currentScene;
                        //Управление машинкой========================================================
                        if (initial)
                        {
                            currentLevel.scroll(new Vector2(0, -800 * 8));
                            currentLevel.objectList[playerId].pos.Y = (int)(windoWidth * 0.7);
                            initial = false;
                        }

                        Car Player = (Car)currentLevel.objectList[playerId];
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
                        else
                        {
                            if (Keyboard.GetState().IsKeyDown(Keys.R))
                            {
                                ReloadLevel();
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
                        if (!initial)
                        {
                            foreach (var object1 in currentLevel.objectList)
                            {
                                if (object1.Value.physical)
                                {
                                    foreach (var object2 in currentLevel.objectList)
                                    {
                                        if (object2.Value.physical && object1.Key != object2.Key)
                                        {
                                            LPhysics((PhysicalObject)object1.Value, (PhysicalObject)object2.Value, new int[2] { (int)(Player.pos.Y - 1000), (int)(Player.pos.Y + 1000) });
                                        }
                                    }
                                    LPhysics((PhysicalObject)object1.Value, currentLevel);
                                }

                            }

                            currentLevel.updateScene(gameTime.ElapsedGameTime.Milliseconds);
                            Vector2 scrollVector = new Vector2(0, -Player.Speed.Y);
                            currentLevel.scroll(scrollVector);//Скроллинг
                        }
                        //Обработка столкновений============================================================= 
                        break;
                    }
            }

            base.Update(gameTime);
        }

        private void LPhysics(PhysicalObject obj1, PhysicalObject obj2, int[] collisionArea)
        {
            if (obj1.CheckNeighborhood(obj2) && obj1.pos.Y > collisionArea[0] && obj1.pos.Y < collisionArea[1] && obj2.pos.Y > collisionArea[0] && obj2.pos.Y < collisionArea[1])
                obj1.collision(obj2);
        }

        private void LPhysics(PhysicalObject obj, Level level)
        {
            if (obj.pos.X > level.rightBorder - obj.hitBox.Width || obj.pos.X < level.leftBorder)
                obj.live = false;
        }

        private void ReloadLevel()
        {
            initial = true;
            LoadLevel();
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin(SpriteSortMode.BackToFront);

            //spriteBatch.Begin();

            switch (currentSceneKey)
            {
                case "MainMenu":
                    {
                        int period = 50;
                        currentTime += gameTime.ElapsedGameTime.Milliseconds;

                        string gameName = "           K.A.R.C.\n Adrenaline Racing";
                        foreach (var obj in currentScene.objectList)
                        {

                            obj.Value.colDraw = new Color(load, load, load);
                            obj.Value.drawObject(spriteBatch, gameTime.ElapsedGameTime.Milliseconds);


                            if (load >= 255)
                            {
                                BackGround title = (BackGround)scenesDict["MainMenu"].objectList[1];
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
                                BackGround pressStart = (BackGround)scenesDict["MainMenu"].objectList[1];
                                pressStart.drawString("ManualFont", "Нажмите пробел для выбора", new Vector2(windoWidth / 2 - 120, windowHeight - 300), Color.FloralWhite, spriteBatch);

                            }
                            else
                            {
                                currentTime = 0;
                            }
                        }
                        break;

                    }
                case "Options":
                    {                       
                        currentTime += gameTime.ElapsedGameTime.Milliseconds;

                        foreach (var obj in currentScene.objectList)
                        {

                            obj.Value.colDraw = new Color(load, load, load);
                            obj.Value.drawObject(spriteBatch, gameTime.ElapsedGameTime.Milliseconds);
                            load += 3;
                        }
                        break;
                    }
                case "level0"://Можно обобщить
                    {


                        if (!songSwitched)
                        {
                            //MediaPlayer.Play(musicList[1]);
                            songSwitched = true;
                        }
                        else
                        {
                            MediaPlayer.Resume();
                            MediaPlayer.Volume = 1.0f;
                        }


                        foreach (var obj in currentScene.objectList)
                        {
                            if (obj.Value.pos.Y > -1500 && obj.Value.pos.Y < 1500)
                                obj.Value.drawObject(spriteBatch, gameTime.ElapsedGameTime.Milliseconds);
                            if (showhitBox && obj.Value.physical)
                            {
                                PhysicalObject hb = (Logic.PhysicalObject)obj.Value;
                                spriteBatch.Draw(Content.Load<Texture2D>("hitBoxBlank"), hb.hitBox, null, Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, 0.3f);
                            }
                        }

                        Car Player = (Car)currentScene.objectList[playerId];
                        spriteBatch.DrawString(gameFont, "Скорость: " + (-Player.Speed.Y)/*Player.Speed.Length()*/, Vector2.Zero, Color.Yellow);
                        spriteBatch.DrawString(gameFont, "Управление:\nСтрелки - движение \nLeftCtrl - Показать хитбоксы \nR - Перезагрузить", new Vector2(0, 400), Color.Red);



                        break;
                    }
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
