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

    public class Game1 : Game
    {
        int load = 255;//Время загрузки заставки        

        Dictionary<string, Texture2D> texturesDict;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        delegate void PushAction(Keys key, int time);
        SoundEffect soundEffect;


        public static GameMode mode;
        string currentSceneKey;
        Scene currentScene;

        Song song;
        Song[] musicList;
        Dictionary<string, Scene> scenesDict = new Dictionary<string, Logic.Scene>();

        public static int windoWidth;
        public static int windowHeight;

        string titleLoad = "";
        int nameIndex = 1;

        int currentTime = 0;
        bool songSwitched = false;


        bool showhitBox = false;
        public static int playerId;
        public static int curSpeed;
        bool pushed = false;
        int currentTimePushed;
        int periodPushed;

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
            periodPushed = 100;
            graphics.IsFullScreen = false;

            graphics.PreferredBackBufferWidth = 1600;
            graphics.PreferredBackBufferHeight = 900;

            graphics.ApplyChanges();

            windoWidth = Window.ClientBounds.Width;
            windowHeight = Window.ClientBounds.Height;

            musicList = new Song[2];
            musicList[0] = Content.Load<Song>("ME");
            musicList[1] = Content.Load<Song>("DemonSpeeding");


            currentTimeforAccel = 0;
            periodForAccel = 100;
            //Загрузка текстур в общий пул
            texturesDict.Add("Grass1", Content.Load<Texture2D>("mapTiles/Grass"));
            texturesDict.Add("Road1", Content.Load<Texture2D>("mapTiles/Road1"));
            texturesDict.Add("MenuBackGround", Content.Load<Texture2D>("MenuBackGround"));
            texturesDict.Add("Start_Select", Content.Load<Texture2D>("Start_Select"));
            texturesDict.Add("StartButton", Content.Load<Texture2D>("StartButton"));
            texturesDict.Add("Options_Select", Content.Load<Texture2D>("Options_Select"));
            texturesDict.Add("OptionsButton", Content.Load<Texture2D>("OptionsButton"));
            texturesDict.Add("Exit_Select", Content.Load<Texture2D>("Exit_Select"));
            texturesDict.Add("ExitButton", Content.Load<Texture2D>("Exit"));
            texturesDict.Add("Garage_Options", Content.Load<Texture2D>("Garage_Options"));
            texturesDict.Add("SwitchBox_Select", Content.Load<Texture2D>("SwitchBox_Light"));
            texturesDict.Add("SwitchBox", Content.Load<Texture2D>("SwitchBox_Dark"));
            texturesDict.Add("Table", Content.Load<Texture2D>("Table"));
            texturesDict.Add("ApplyChanges_Select", Content.Load<Texture2D>("ApplyChanges_Select"));
            texturesDict.Add("ApplyChangesButton", Content.Load<Texture2D>("ApplyChanges_Button"));
            texturesDict.Add("BackButton_Select", Content.Load<Texture2D>("BackButton_Select"));
            texturesDict.Add("BackButton", Content.Load<Texture2D>("BackButton"));
            texturesDict.Add("Planhset2", Content.Load<Texture2D>("Planhset2"));
            texturesDict.Add("hitBoxBlank", Content.Load<Texture2D>("hitBoxBlank"));
            texturesDict.Add("Speedometer1", Content.Load<Texture2D>("Speedometer"));
            texturesDict.Add("Speedometer1_Arrow", Content.Load<Texture2D>("Speedometer_Arrow"));
            //===================Загрузка начального экрана
            LoadMainMenu();
            LoadOptions();


            //Загрузка игры
            //LoadLevel();

            currentSceneKey = "MainMenu";
            currentScene = scenesDict[currentSceneKey];


        }

        private void LoadMainMenu()
        {
            int[,] map = new int[1, 1];
            scenesDict.Remove("MainMenu");
            List<Object> objList = new List<Object>();
            Dictionary<string, Texture2D> textureDict = new Dictionary<string, Texture2D>();
            textureDict.Add("background", texturesDict["MenuBackGround"]);

            SpriteFont gameName = Content.Load<SpriteFont>("Title");
            Dictionary<string, SpriteFont> fontDict = new Dictionary<string, SpriteFont>();
            fontDict.Add("Title", gameName);
            fontDict.Add("ManualFont", Content.Load<SpriteFont>("ManualFont"));

            BackGround backGround = new BackGround(Vector2.Zero, 1.0f, textureDict, true, fontDict, new Vector2(windoWidth, windowHeight));
            objList.Add(backGround);

            textureDict = new Dictionary<string, Texture2D>();
            textureDict.Add("light", texturesDict["Start_Select"]);
            textureDict.Add("dark", texturesDict["StartButton"]);
            Vector2 place = InterfaceMenu.GetCoord(16, 17, 29, 50);
            Button btnStart = new Button(new Vector2(place.X - textureDict["light"].Width / 2, place.Y + textureDict["light"].Height / 2), 0.9f, textureDict, 0);
            btnStart.check = true;
            objList.Add(btnStart);

            textureDict = new Dictionary<string, Texture2D>();
            textureDict.Add("light", texturesDict["Options_Select"]);
            textureDict.Add("dark", texturesDict["OptionsButton"]);
            place = InterfaceMenu.GetCoord(16, 21, 29, 50);
            Button btnOptions = new Button(new Vector2(place.X - textureDict["light"].Width / 2, place.Y + textureDict["light"].Height / 2), 0.9f, textureDict, 1);
            objList.Add(btnOptions);


            textureDict = new Dictionary<string, Texture2D>();
            textureDict.Add("light", texturesDict["Exit_Select"]);
            textureDict.Add("dark", texturesDict["ExitButton"]);
            place = InterfaceMenu.GetCoord(16, 25, 29, 50);
            Button btnExit = new Button(new Vector2(place.X - textureDict["light"].Width / 2, place.Y + textureDict["light"].Height / 2), 0.9f, textureDict, 2);
            objList.Add(btnExit);

            InterfaceMenu mainMenu = new InterfaceMenu(map, 600, objList, 200, 10, 9);
            song = Content.Load<Song>("ME");
            scenesDict.Add("MainMenu", mainMenu);
        }

        private void LoadOptions()
        {
            int[,] map = new int[1, 1];
            scenesDict.Remove("Options");

            List<Object> objList = new List<Object>();
            Dictionary<string, Texture2D> textureDict = new Dictionary<string, Texture2D>();
            textureDict.Add("background", texturesDict["Garage_Options"]);

            SpriteFont gameName = Content.Load<SpriteFont>("Title");
            Dictionary<string, SpriteFont> fontDict = new Dictionary<string, SpriteFont>();
            fontDict.Add("Title", gameName);
            fontDict.Add("ManualFont", Content.Load<SpriteFont>("ManualFont"));

            BackGround backGround = new BackGround(Vector2.Zero, 1.0f, textureDict, true, fontDict, new Vector2(windoWidth, windowHeight));
            objList.Add(backGround);

            //Настройки экрана
            textureDict = new Dictionary<string, Texture2D>();
            textureDict.Add("light", texturesDict["SwitchBox_Select"]);
            textureDict.Add("dark", texturesDict["SwitchBox"]);

            string currentWidth = windoWidth.ToString();
            string[] screenArray = new string[] { "1440x900", "1600x900", "1920x1080" };
            int screenIndex = 0;
            while (currentWidth != screenArray[screenIndex].Split('x')[0])
            {
                screenIndex++;
            }
            Vector2 place = InterfaceMenu.GetCoord(21, 2, 29, 51);
            SwitchBox swbScreen = new SwitchBox(new Vector2(place.X, place.Y),
                0.9f, textureDict, 0, Content.Load<SpriteFont>("ManualFont"),
                screenArray, screenIndex);
            swbScreen.check = true;
            objList.Add(swbScreen);

            textureDict = new Dictionary<string, Texture2D>();
            textureDict.Add("light", texturesDict["Table"]);
            place = InterfaceMenu.GetCoord(13, 2, 29, 51);
            Vector2 tableSize = new Vector2((float)(windoWidth * 0.25), (float)(swbScreen.currentImage.Height));
            Label lblScreen = new Label(place, 0.9f, textureDict, 6, Content.Load<SpriteFont>("ManualFont"),
                new string[] { "Screen Resolution" }, 0, tableSize);
            objList.Add(lblScreen);

            //Настройки оконного/полноэкранного режима
            textureDict = new Dictionary<string, Texture2D>();
            textureDict.Add("light", texturesDict["SwitchBox_Select"]);
            textureDict.Add("dark", texturesDict["SwitchBox"]);

            int fullScreenIndex = 0;
            if (graphics.IsFullScreen)
                fullScreenIndex = 0;
            else
                fullScreenIndex = 1;
            place = InterfaceMenu.GetCoord(21, 6, 29, 51);
            SwitchBox swbFullScreen = new SwitchBox(place, 0.9f, textureDict, 1,
                Content.Load<SpriteFont>("ManualFont"), new string[] { "Yes", "No" }, fullScreenIndex);
            objList.Add(swbFullScreen);


            textureDict = new Dictionary<string, Texture2D>();
            textureDict.Add("light", texturesDict["Table"]);
            place = InterfaceMenu.GetCoord(13, 6, 29, 51);
            Label lblScreenFull = new Label(place, 0.9f, textureDict, 7, Content.Load<SpriteFont>("ManualFont"), new string[] { "Fullscreen" }, 0, tableSize);
            objList.Add(lblScreenFull);

            //Настройки громкости музыки
            textureDict = new Dictionary<string, Texture2D>();
            textureDict.Add("light", texturesDict["SwitchBox_Select"]);
            textureDict.Add("dark", texturesDict["SwitchBox"]);

            string currentVolumeMusic = (100 * MediaPlayer.Volume).ToString();
            string[] volumeArray = new string[] { "0", "25", "50", "75", "100" };
            int volumeIndex = 0;
            while (currentVolumeMusic != volumeArray[volumeIndex])
            {
                volumeIndex++;
            }
            place = InterfaceMenu.GetCoord(21, 10, 29, 51);
            SwitchBox swbMusic = new SwitchBox(place, 0.9f, textureDict, 2, Content.Load<SpriteFont>("ManualFont"),
                volumeArray, volumeIndex);
            objList.Add(swbMusic);

            textureDict = new Dictionary<string, Texture2D>();
            textureDict.Add("light", Content.Load<Texture2D>("Table"));
            place = InterfaceMenu.GetCoord(13, 10, 29, 51);
            Label lblMusic = new Label(place, 0.9f, textureDict, 8, Content.Load<SpriteFont>("ManualFont"), new string[] { "Music Volume" }, 0, tableSize);
            objList.Add(lblMusic);


            //Кнопка "Принять изменения"
            textureDict = new Dictionary<string, Texture2D>();
            textureDict.Add("light", Content.Load<Texture2D>("ApplyChanges_Select"));
            textureDict.Add("dark", Content.Load<Texture2D>("ApplyChanges_Button"));
            place = InterfaceMenu.GetCoord(21, 14, 29, 51);
            Button btnApplyChanges = new Button(place, 0.9f, textureDict, 3);
            objList.Add(btnApplyChanges);

            //Кнопка "Вернуться"
            textureDict = new Dictionary<string, Texture2D>();
            textureDict.Add("light", Content.Load<Texture2D>("BackButton_Select"));
            textureDict.Add("dark", Content.Load<Texture2D>("BackButton"));
            place = InterfaceMenu.GetCoord(21, 18, 29, 51);
            Button btnBack = new Button(place, 0.9f, textureDict, 4);
            objList.Add(btnBack);

            //Рисунок планшета и инструкции на нем
            textureDict = new Dictionary<string, Texture2D>();
            textureDict.Add("light", Content.Load<Texture2D>("Planhset2"));
            textureDict.Add("dark", Content.Load<Texture2D>("Planhset2"));
            Vector2 planshetActSize = new Vector2((float)(windoWidth * 0.5), (float)(windowHeight * 0.45));
            place = InterfaceMenu.GetCoord(14, 34, 29, 51);
            BackGround Planshet = new BackGround(place, 0.95f, textureDict, true, planshetActSize);
            objList.Add(Planshet);

            textureDict = new Dictionary<string, Texture2D>();
            textureDict.Add("light", Content.Load<Texture2D>("hitBoxBlank"));
            textureDict.Add("dark", Content.Load<Texture2D>("hitBoxBlank"));
            place = InterfaceMenu.GetCoord(75, 44, 100, 51);
            Label lblInstructions = new Label(place, 0.9f, textureDict, 5, Content.Load<SpriteFont>("ManualFont"),
                new string[] { "Press Up and Down arrows to choose", "Right and Left arrows to change value" }, 0, Vector2.Zero);
            objList.Add(lblInstructions);
            InterfaceMenu Options = new InterfaceMenu(map, 600, objList, 150, 4, 6);

            scenesDict.Add("Options", Options);
        }
        private void LoadLevel()
        {
            //Тестовый уровень
            initial = true;
            int[,] map = new int[1, 10] { { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 } };
            List<Object> objList = new List<Object>();
            Dictionary<string, Texture2D> textureDict = new Dictionary<string, Texture2D>();
            scenesDict.Remove("level0");

            objList.Clear();
            textureDict.Clear();
            textureDict.Add("Road", Content.Load<Texture2D>("mapTiles/Road1"));

            int shiftX = windoWidth / 2 - textureDict["Road"].Width / 2;
            for (int i = 0; i < map.GetLength(1); i++)
            {
                if (map[0, i] != 0)
                {
                    objList.Add(new BackGround(new Vector2(shiftX, i * (int)(textureDict["Road"].Height * 0.9)), 0.9f, textureDict, false, new Vector2(windoWidth, windowHeight)));

                }
            }
            textureDict.Clear();
            textureDict.Add("Grass", texturesDict["Grass1"]);
            for (int i = 0; i < map.GetLength(1); i++)
            {
                if (map[0, i] != 0)
                {
                    objList.Add(new BackGround(new Vector2(0, i * (int)(windowHeight * 0.9)), 0.91f, textureDict, true, new Vector2(windoWidth, windowHeight)));

                }
            }

            textureDict.Clear();
            textureDict.Add("Speedometer", texturesDict["Speedometer1"]);
            textureDict.Add("Arrow", texturesDict["Speedometer1_Arrow"]);
            int shiftY = windowHeight - textureDict["Speedometer"].Height;
            Speedometer playerSpeedometer = new Speedometer(new Vector2(0, shiftY), 0.89f, textureDict);
            objList.Add(playerSpeedometer);

            Animation carExplosion = new Animation(Content.Load<Texture2D>("Animations/boom3"), 128, 128, new Point(8, 8), Vector2.Zero, false);
            carExplosion.scale = 2.0f;
            SoundEffect explosionSound = Content.Load<SoundEffect>("Sound/DeathFlash");

            //Генерация игрока
            textureDict.Clear();
            textureDict.Add("MainModel", Content.Load<Texture2D>("carModels/Model1"));
            textureDict.Add("CrushedModel", Content.Load<Texture2D>("carModels/Model1_Crushed"));
            Vector2 place = InterfaceMenu.GetCoord(0, 0, 100, 100);
            Car Player = new Car(place, 0.2f, textureDict, new Vector2(0, 0), 5000, "Player");
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

            //Level testLevel = new Level(map, 840, objList, true, textureDict, shiftX+139, shiftX+702);
            Level testLevel = new Level(map, (int)(texturesDict["Road1"].Height * 0.9), objList, true, textureDict, shiftX + 139, shiftX + 702);
            scenesDict.Add("level0", testLevel);

            gameFont = Content.Load<SpriteFont>("ManualFont");
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            soundEffect = Content.Load<SoundEffect>("Sound/DeathFlash");
            song = Content.Load<Song>("ME");
            if (mode == GameMode.mainMenu)
            {
                MediaPlayer.Play(song);
                // повторять после завершения
                MediaPlayer.IsRepeating = true;
                MediaPlayer.Volume = 0.0f;
            }
        }


        protected override void UnloadContent()
        {

        }
        protected override void Update(GameTime gameTime)
        {
            //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            //    Exit();
            currentTime += gameTime.ElapsedGameTime.Milliseconds;
            currentScene = scenesDict[currentSceneKey];
            switch (currentSceneKey)
            {
                case "MainMenu":
                    {
                        InterfaceMenu currentForm = (InterfaceMenu)currentScene;
                        currentTimePushed += gameTime.ElapsedGameTime.Milliseconds;

                        if (currentTimePushed > periodPushed)
                        {
                            pushed = false;
                        }
                        PushAction update = currentForm.updateScene;
                        if (Keyboard.GetState().IsKeyDown(Keys.Up))
                        {
                            PushCalc(update, Keys.Up, gameTime.ElapsedGameTime.Milliseconds);
                        }
                        else if (Keyboard.GetState().IsKeyDown(Keys.Down))
                        {
                            PushCalc(update, Keys.Down, gameTime.ElapsedGameTime.Milliseconds);
                        }
                        else
                            currentForm.updateScene(gameTime.ElapsedGameTime.Milliseconds);

                        if (Keyboard.GetState().IsKeyDown(Keys.Space) || Keyboard.GetState().IsKeyDown(Keys.Enter))
                        {
                            if (!pushed)
                            {
                                switch (currentForm.cursor)
                                {
                                    case 0:
                                        {
                                            //load = 0;
                                            currentSceneKey = "level0";
                                            LoadLevel();
                                            currentScene = scenesDict[currentSceneKey];
                                            break;
                                        }
                                    case 1:
                                        {
                                            //load = 0;
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

                                pushed = true;
                                currentTimePushed = 0;
                            }
                        }
                        //TODO: Обобщить работу с интерфейсом
                        break;
                    }
                case "Options":
                    {
                        InterfaceMenu currentForm = (InterfaceMenu)currentScene;
                        currentTimePushed += gameTime.ElapsedGameTime.Milliseconds;
                        if (currentTimePushed > periodPushed)
                        {
                            pushed = false;
                        }
                        if (!pushed)
                        {
                            if (Keyboard.GetState().IsKeyDown(Keys.Up))
                            {
                                currentForm.updateScene(Keys.Up, gameTime.ElapsedGameTime.Milliseconds);
                                pushed = true;
                                currentTimePushed = 0;
                            }
                            else if (Keyboard.GetState().IsKeyDown(Keys.Down))
                            {
                                currentForm.updateScene(Keys.Down, gameTime.ElapsedGameTime.Milliseconds);
                                pushed = true;
                                currentTimePushed = 0;
                            }
                            else
                                currentForm.updateScene(gameTime.ElapsedGameTime.Milliseconds);

                            if (Keyboard.GetState().IsKeyDown(Keys.Right) || Keyboard.GetState().IsKeyDown(Keys.Left))
                            {
                                string dirValue = "";
                                if (Keyboard.GetState().IsKeyDown(Keys.Right))
                                    dirValue = "right";
                                if (Keyboard.GetState().IsKeyDown(Keys.Left))
                                    dirValue = "left";

                                switch (currentForm.cursor)
                                {
                                    case 0:
                                        {
                                            SwitchBox s = (SwitchBox)currentScene.objectList[2];//TODO: Да-да, я знаю
                                            s.ChangeIndex(dirValue, gameTime.ElapsedGameTime.Milliseconds);
                                            break;
                                        }
                                    case 1:
                                        {
                                            SwitchBox s = (SwitchBox)currentScene.objectList[4];//TODO: Да-да, я знаю
                                            s.ChangeIndex(dirValue, gameTime.ElapsedGameTime.Milliseconds);
                                            break;
                                        }
                                    case 2:
                                        {
                                            SwitchBox s = (SwitchBox)currentScene.objectList[6];//TODO: Да-да, я знаю
                                            s.ChangeIndex(dirValue, gameTime.ElapsedGameTime.Milliseconds);
                                            break;
                                        }
                                }
                                pushed = true;
                                currentTimePushed = 0;
                            }

                            if (Keyboard.GetState().IsKeyDown(Keys.Space) || Keyboard.GetState().IsKeyDown(Keys.Enter))
                            {
                                switch (currentForm.cursor)
                                {
                                    case 3://TODO: Да-да, это крайне плохо!
                                        {
                                            SwitchBox s = (SwitchBox)currentScene.objectList[2];
                                            int[] screenDimension;
                                            if (SwitchBox.ParseValue(s.GetValue(), out screenDimension))
                                            {
                                                if (graphics.PreferredBackBufferWidth != screenDimension[0] || graphics.PreferredBackBufferHeight != screenDimension[1])
                                                {
                                                    graphics.PreferredBackBufferWidth = screenDimension[0];
                                                    graphics.PreferredBackBufferHeight = screenDimension[1];

                                                    windoWidth = graphics.PreferredBackBufferWidth;
                                                    windowHeight = graphics.PreferredBackBufferHeight;
                                                    graphics.ApplyChanges();
                                                    LoadMainMenu();
                                                    LoadOptions();
                                                    LoadLevel();
                                                }
                                            }

                                            s = (SwitchBox)currentScene.objectList[4];
                                            bool fullScreen;
                                            if (SwitchBox.ParseValue(s.GetValue(), out fullScreen))
                                            {
                                                if (graphics.IsFullScreen != fullScreen)
                                                {
                                                    graphics.IsFullScreen = fullScreen;
                                                    graphics.ApplyChanges();
                                                }

                                            }

                                            s = (SwitchBox)currentScene.objectList[6];
                                            float musicVolume;
                                            if (SwitchBox.ParseValue(s.GetValue(), out musicVolume))
                                            {
                                                MediaPlayer.Volume = musicVolume;
                                            }

                                            break;
                                        }
                                    case 4:
                                        {
                                            //load = 0;
                                            titleLoad = "";
                                            nameIndex = 1;
                                            currentSceneKey = "MainMenu";
                                            currentScene = scenesDict[currentSceneKey];
                                            break;
                                        }

                                }
                                pushed = true;
                                currentTimePushed = 0;
                            }

                        }



                        break;
                    }
                case "level0":
                    {
                        Level currentLevel = (Level)currentScene;
                        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                        {
                            currentSceneKey = "MainMenu";
                            currentScene = scenesDict[currentSceneKey];
                        }
                        //Управление машинкой========================================================
                        if (initial)
                        {
                            currentLevel.scroll(new Vector2(0, -windowHeight * 5));
                            currentLevel.objectList[playerId].pos = InterfaceMenu.GetCoord(50, 70, 100, 100);
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
                                LoadLevel();
                            }
                        }
                        curSpeed = (int)System.Math.Abs(Player.Speed.Y * 3.6);
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
                                List<PhysicalObject> nearObj = new List<PhysicalObject>();
                                if (object1.Value.physical)
                                {
                                    foreach (var object2 in currentLevel.objectList)
                                    {
                                        if (object2.Value.physical && object1.Key != object2.Key)
                                        {
                                            LPhysics((PhysicalObject)object1.Value, (PhysicalObject)object2.Value, new int[2] { (int)(Player.pos.Y - 1000), (int)(Player.pos.Y + 1000) }, nearObj);
                                        }
                                    }
                                    if (object1.Value.GetType() == typeof(Car))
                                    {
                                        Car driver = (Car)object1.Value;
                                        driver.Speed = driver.AI.Act(nearObj);
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

        private void LPhysics(PhysicalObject obj1, PhysicalObject obj2, int[] collisionArea, List<PhysicalObject> nearList)
        {
            if (obj1.CheckNeighborhood(obj2) && obj1.pos.Y > collisionArea[0] && obj1.pos.Y < collisionArea[1] && obj2.pos.Y > collisionArea[0] && obj2.pos.Y < collisionArea[1])
                if (obj1.collision(obj2))
                {
                    SoundEffectInstance soundEffectInstance = soundEffect.CreateInstance();
                    soundEffectInstance.IsLooped = false;
                    soundEffectInstance.Play();
                }
                else
                    nearList.Add(obj2);



        }

        private void LPhysics(PhysicalObject obj, Level level)
        {
            if (obj.pos.X > level.rightBorder - obj.hitBox.Width || obj.pos.X < level.leftBorder)
                obj.live = false;
        }




        private void PushCalc(PushAction p, Keys k, int t)
        {
            PushAction act = p;
            if (!pushed)
            {
                act(k, t);
                pushed = true;
                currentTimePushed = 0;
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin(SpriteSortMode.BackToFront);

            //spriteBatch.Begin();
            if (load > 255)
                load = 255;//TODO: сделать свойство!
            switch (currentSceneKey)
            {
                case "MainMenu":
                    {
                        int period = 50;
                        currentTime += gameTime.ElapsedGameTime.Milliseconds;

                        string[] gameName = new string[] { "       K.A.R.C.", "Adrenaline Racing" };

                        foreach (var obj in currentScene.objectList)
                        {

                            obj.Value.colDraw = new Color(load, load, load);
                            obj.Value.drawObject(spriteBatch, gameTime.ElapsedGameTime.Milliseconds);


                            //if (load >= 255)
                            //{
                            //        BackGround title = (BackGround)scenesDict["MainMenu"].objectList[1];

                            //        Vector2 place = InterfaceMenu.GetCoord(12, 10, 29, 50);  
                            //        for (int i =0; i<gameName.Length;i++)
                            //        {
                            //            Vector2 stringLength = Content.Load<SpriteFont>("ManualFont").MeasureString(gameName[i]);
                            //            title.drawString("Title", gameName[i], new Vector2(place.X - stringLength.X / 2,
                            //place.Y - stringLength.Y / 2+(int)(stringLength.Y * i*1.7)), new Color(load, 0, 0), spriteBatch);
                            //        }



                            // }
                        }
                        /*if (titleLoad != gameName)
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
                        {*/
                        period = 300;
                        if (currentTime < period)
                        {

                        }
                        else if (currentTime > period && currentTime < 2 * period)
                        {
                            BackGround pressStart = (BackGround)scenesDict["MainMenu"].objectList[1];
                            Vector2 place = InterfaceMenu.GetCoord(16, 32, 29, 50);
                            string s = "Нажмите пробел для выбора";
                            Vector2 stringLength = Content.Load<SpriteFont>("ManualFont").MeasureString(s);
                            pressStart.drawString("ManualFont", s, new Vector2(place.X - stringLength.X / 2,
                place.Y - stringLength.Y / 2), Color.FloralWhite, spriteBatch);

                        }
                        else
                        {
                            currentTime = 0;
                        }
                        //}
                        break;

                    }
                case "Options":
                    {
                        currentTime += gameTime.ElapsedGameTime.Milliseconds;

                        foreach (var obj in currentScene.objectList)
                        {

                            obj.Value.colDraw = new Color(load, load, load);
                            obj.Value.drawObject(spriteBatch, gameTime.ElapsedGameTime.Milliseconds);
                            //load += 3;
                        }
                        break;
                    }
                case "level0"://Можно обобщить
                    {


                        //if (!songSwitched)
                        //{
                        //    MediaPlayer.Play(musicList[1]);
                        //    songSwitched = true;
                        //}
                        //else
                        //{
                        //    MediaPlayer.Resume();                           
                        //}


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
                        int speedKm = -(int)(Player.Speed.Y * 3.6);
                        //spriteBatch.DrawString(gameFont, "Скорость: " + speedKm +" км/ч", Vector2.Zero, Color.Yellow);
                        spriteBatch.DrawString(gameFont, "Управление:\nСтрелки - движение \nLeftCtrl - Показать хитбоксы \nR - Перезагрузить\nEsc - Выход в меню", new Vector2(0, 400), Color.Red);



                        break;
                    }
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
