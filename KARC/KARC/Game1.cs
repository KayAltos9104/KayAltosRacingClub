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
        SoundEffect soundEffect;
        public static System.Random rnd = new System.Random();
        
        public static int windoWidth;
        public static int windowHeight;
        bool initial = true;

        delegate void PushAction(int time);
        bool pushed = false;
        int currentTimePushed;
        int periodPushed;


        public static GameMode mode;
        
        Song song;
        Song[] musicList;


        bool showhitBox = false;

        public static int currentTime = 0; 
        
        public static int playerId;
        public static int curSpeed; 
        int currentTimeforAccel;
        int periodForAccel;

        SpriteFont gameFont;

        Dictionary<string, Scene> scenesDict = new Dictionary<string, Scene>();
        SceneController sceneController;
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

            texturesDict.Add("MainModel", Content.Load<Texture2D>("carModels/Model1"));
            texturesDict.Add("CrushedModel", Content.Load<Texture2D>("carModels/Model1_Crushed"));
            texturesDict.Add("MainModel2", Content.Load<Texture2D>("carModels/Model2"));
            texturesDict.Add("CrushedModel2", Content.Load<Texture2D>("carModels/Model2_Crushed"));
            texturesDict.Add("MainModel3", Content.Load<Texture2D>("carModels/Model3"));
            texturesDict.Add("CrushedModel3", Content.Load<Texture2D>("carModels/Model3_Crushed"));
            texturesDict.Add("MainModel4", Content.Load<Texture2D>("carModels/Model4"));
            texturesDict.Add("CrushedModel4", Content.Load<Texture2D>("carModels/Model4_Crushed"));
            texturesDict.Add("MainModel5", Content.Load<Texture2D>("carModels/Model5"));
            texturesDict.Add("CrushedModel5", Content.Load<Texture2D>("carModels/Model5_Crushed"));
            texturesDict.Add("MainModel6", Content.Load<Texture2D>("carModels/Model6"));
            texturesDict.Add("CrushedModel6", Content.Load<Texture2D>("carModels/Model6_Crushed"));
            texturesDict.Add("MainModel7", Content.Load<Texture2D>("carModels/Model7"));
            texturesDict.Add("CrushedModel7", Content.Load<Texture2D>("carModels/Model7_Crushed"));
            texturesDict.Add("MainModel8", Content.Load<Texture2D>("carModels/Model8"));
            texturesDict.Add("CrushedModel8", Content.Load<Texture2D>("carModels/Model8_Crushed"));

            texturesDict.Add("explosion", Content.Load<Texture2D>("Animations/boom3"));
            //===================Создание контроллера
            sceneController = new SceneController();
            //===================Загрузка начального экрана
            sceneController.AddScene("MainMenu", LoadMainMenu());
            sceneController.AddScene("Options", LoadOptions());
            
            sceneController.SwitchScene("MainMenu");
        }

        private InterfaceMenu LoadMainMenu()
        {
            int[,] map = new int[1, 1];
            scenesDict.Remove("MainMenu");
            InterfaceMenu mainMenu = new InterfaceMenu(map, 600, 200, 10, 9);

            List<Object> objList = new List<Object>();
            Dictionary<string, Texture2D> textureDict = new Dictionary<string, Texture2D>();
            textureDict.Add("background", texturesDict["MenuBackGround"]);

            SpriteFont gameName = Content.Load<SpriteFont>("Title");
            Dictionary<string, SpriteFont> fontDict = new Dictionary<string, SpriteFont>();
            fontDict.Add("Title", gameName);
            fontDict.Add("ManualFont", Content.Load<SpriteFont>("ManualFont"));

            BackGround backGround = new BackGround(Vector2.Zero, 1.0f, textureDict, true, fontDict, new Vector2(windoWidth, windowHeight), mainMenu);
            
            mainMenu.AddObject(backGround);

            textureDict = new Dictionary<string, Texture2D>();
            textureDict.Add("light", texturesDict["Start_Select"]);
            textureDict.Add("dark", texturesDict["StartButton"]);
            Vector2 place = InterfaceMenu.GetCoord(16, 17, 29, 50);
            Button btnStart = new Button(new Vector2(place.X - textureDict["light"].Width / 2, place.Y + textureDict["light"].Height / 2), 0.9f, textureDict, 0, mainMenu);
            btnStart.check = true;
            mainMenu.AddObject(btnStart);

            textureDict = new Dictionary<string, Texture2D>();
            textureDict.Add("light", texturesDict["Options_Select"]);
            textureDict.Add("dark", texturesDict["OptionsButton"]);
            place = InterfaceMenu.GetCoord(16, 21, 29, 50);
            Button btnOptions = new Button(new Vector2(place.X - textureDict["light"].Width / 2, place.Y + textureDict["light"].Height / 2), 0.9f, textureDict, 1, mainMenu);
            mainMenu.AddObject(btnOptions);


            textureDict = new Dictionary<string, Texture2D>();
            textureDict.Add("light", texturesDict["Exit_Select"]);
            textureDict.Add("dark", texturesDict["ExitButton"]);
            place = InterfaceMenu.GetCoord(16, 25, 29, 50);
            Button btnExit = new Button(new Vector2(place.X - textureDict["light"].Width / 2, place.Y + textureDict["light"].Height / 2), 0.9f, textureDict, 2, mainMenu);
            mainMenu.AddObject(btnExit);

            
            song = Content.Load<Song>("ME");
            return mainMenu;
        }

        private InterfaceMenu LoadOptions()
        {
            int[,] map = new int[1, 1];
            scenesDict.Remove("Options");

            List<Object> objList = new List<Object>();
            InterfaceMenu Options = new InterfaceMenu(map, 600, 150, 4, 6);

            Dictionary<string, Texture2D> textureDict = new Dictionary<string, Texture2D>();
            textureDict.Add("background", texturesDict["Garage_Options"]);

            SpriteFont gameName = Content.Load<SpriteFont>("Title");
            Dictionary<string, SpriteFont> fontDict = new Dictionary<string, SpriteFont>();
            fontDict.Add("Title", gameName);
            fontDict.Add("ManualFont", Content.Load<SpriteFont>("ManualFont"));

            BackGround backGround = new BackGround(Vector2.Zero, 1.0f, textureDict, true, fontDict, new Vector2(windoWidth, windowHeight),Options);
            Options.AddObject(backGround);

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
                screenArray, screenIndex, Options);
            swbScreen.check = true;
            Options.AddObject(swbScreen);

            textureDict = new Dictionary<string, Texture2D>();
            textureDict.Add("light", texturesDict["Table"]);
            place = InterfaceMenu.GetCoord(13, 2, 29, 51);
            Vector2 tableSize = new Vector2((float)(windoWidth * 0.25), (float)(swbScreen.currentImage.Height));
            Label lblScreen = new Label(place, 0.9f, textureDict, 6, Content.Load<SpriteFont>("ManualFont"),
                new string[] { "Screen Resolution" }, 0, tableSize, Options);
            Options.AddObject(lblScreen);

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
                Content.Load<SpriteFont>("ManualFont"), new string[] { "Yes", "No" }, fullScreenIndex, Options);
            Options.AddObject(swbScreen);


            textureDict = new Dictionary<string, Texture2D>();
            textureDict.Add("light", texturesDict["Table"]);
            place = InterfaceMenu.GetCoord(13, 6, 29, 51);
            Label lblScreenFull = new Label(place, 0.9f, textureDict, 7, Content.Load<SpriteFont>("ManualFont"), 
                new string[] { "Fullscreen" }, 0, tableSize, Options);
            Options.AddObject(lblScreenFull);

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
                volumeArray, volumeIndex, Options);
            Options.AddObject(swbMusic);

            textureDict = new Dictionary<string, Texture2D>();
            textureDict.Add("light", Content.Load<Texture2D>("Table"));
            place = InterfaceMenu.GetCoord(13, 10, 29, 51);
            Label lblMusic = new Label(place, 0.9f, textureDict, 8, Content.Load<SpriteFont>("ManualFont"), 
                new string[] { "Music Volume" }, 0, tableSize, Options);
            Options.AddObject(lblMusic);


            //Кнопка "Принять изменения"
            textureDict = new Dictionary<string, Texture2D>();
            textureDict.Add("light", Content.Load<Texture2D>("ApplyChanges_Select"));
            textureDict.Add("dark", Content.Load<Texture2D>("ApplyChanges_Button"));
            place = InterfaceMenu.GetCoord(21, 14, 29, 51);
            Button btnApplyChanges = new Button(place, 0.9f, textureDict, 3,Options);
            Options.AddObject(btnApplyChanges);

            //Кнопка "Вернуться"
            textureDict = new Dictionary<string, Texture2D>();
            textureDict.Add("light", Content.Load<Texture2D>("BackButton_Select"));
            textureDict.Add("dark", Content.Load<Texture2D>("BackButton"));
            place = InterfaceMenu.GetCoord(21, 18, 29, 51);
            Button btnBack = new Button(place, 0.9f, textureDict, 4, Options);
            Options.AddObject(btnBack);

            //Рисунок планшета и инструкции на нем
            textureDict = new Dictionary<string, Texture2D>();
            textureDict.Add("light", Content.Load<Texture2D>("Planhset2"));
            textureDict.Add("dark", Content.Load<Texture2D>("Planhset2"));
            Vector2 planshetActSize = new Vector2((float)(windoWidth * 0.5), (float)(windowHeight * 0.45));
            place = InterfaceMenu.GetCoord(14, 34, 29, 51);
            BackGround Planshet = new BackGround(place, 0.95f, textureDict, true, planshetActSize, Options);
            Options.AddObject(Planshet);

            textureDict = new Dictionary<string, Texture2D>();
            textureDict.Add("light", Content.Load<Texture2D>("hitBoxBlank"));
            textureDict.Add("dark", Content.Load<Texture2D>("hitBoxBlank"));
            place = InterfaceMenu.GetCoord(75, 44, 100, 51);
            Label lblInstructions = new Label(place, 0.9f, textureDict, 5, Content.Load<SpriteFont>("ManualFont"),
                new string[] { "Press Up and Down arrows to choose", "Right and Left arrows to change value" }, 0, Vector2.Zero, Options);
            Options.AddObject(lblInstructions);
            return Options;
        }
        private Level LoadLevel()
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
            textureDict.Add("Speedometer", texturesDict["Speedometer1"]);
            int shiftY = windowHeight - textureDict["Speedometer"].Height;

            Level testLevel = new Level(map, (int)(texturesDict["Road1"].Height * 0.9),true, textureDict, shiftX + 139, shiftX + 702);
            
            for (int i = 0; i < map.GetLength(1); i++)
            {
                if (map[0, i] != 0)
                {
                    testLevel.AddObject(new BackGround(new Vector2(shiftX, i * (int)(textureDict["Road"].Height * 0.9)), 0.9f, textureDict, false, new Vector2(windoWidth, windowHeight), testLevel));

                }
            }
            textureDict.Clear();
            textureDict.Add("Grass", texturesDict["Grass1"]);
            for (int i = 0; i < map.GetLength(1); i++)
            {
                if (map[0, i] != 0)
                {
                    testLevel.AddObject(new BackGround(new Vector2(0, i * (int)(windowHeight * 0.9)), 0.91f, textureDict, true, new Vector2(windoWidth, windowHeight), testLevel));

                }
            }

            textureDict.Clear();
            textureDict.Add("Speedometer", texturesDict["Speedometer1"]);
            textureDict.Add("Arrow", texturesDict["Speedometer1_Arrow"]);
            
            Speedometer playerSpeedometer = new Speedometer(new Vector2(0, shiftY), 0.89f, textureDict, testLevel);
            testLevel.AddObject(playerSpeedometer);

            Animation carExplosion = new Animation(texturesDict["explosion"], 128, 128, new Point(8, 8), Vector2.Zero, false);
            carExplosion.scale = 2.0f;
            SoundEffect explosionSound = Content.Load<SoundEffect>("Sound/DeathFlash");

            //Генерация игрока
            textureDict.Clear();
            textureDict.Add("MainModel", texturesDict["MainModel"]);
            textureDict.Add("CrushedModel", texturesDict["CrushedModel"]);
            Vector2 place = InterfaceMenu.GetCoord(0, 0, 100, 100);
            Car Player = new Car(place, 0.2f, textureDict, new Vector2(0, 0), 5000, "Player", testLevel);
            Player.player = true;
            Player.animationDict.Add("explosion", carExplosion);
            Player.soundEffectsDict.Add("explosion", explosionSound);
            testLevel.AddObject(Player);

            //Загрузка текстур моделей машин
            textureDict.Clear();
            textureDict.Add("MainModel2", texturesDict["MainModel2"]);
            textureDict.Add("CrushedModel2", texturesDict["CrushedModel2"]);
            textureDict.Add("MainModel3", texturesDict["MainModel3"]);
            textureDict.Add("CrushedModel3", texturesDict["CrushedModel3"]);
            textureDict.Add("MainModel4", texturesDict["MainModel4"]);
            textureDict.Add("CrushedModel4", texturesDict["CrushedModel4"]);
            textureDict.Add("MainModel5", texturesDict["MainModel5"]);
            textureDict.Add("CrushedModel5", texturesDict["CrushedModel5"]);
            textureDict.Add("MainModel6", texturesDict["MainModel6"]);
            textureDict.Add("CrushedModel6", texturesDict["CrushedModel6"]);
            textureDict.Add("MainModel7", texturesDict["MainModel7"]);
            textureDict.Add("CrushedModel7", texturesDict["CrushedModel7"]);
            textureDict.Add("MainModel8", texturesDict["MainModel8"]);
            textureDict.Add("CrushedModel8", texturesDict["CrushedModel8"]);

            textureDict.Add("explosion", texturesDict["explosion"]);         
           

            gameFont = Content.Load<SpriteFont>("ManualFont");
            return testLevel;
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
            currentTime += gameTime.ElapsedGameTime.Milliseconds;  

            switch (sceneController.GetCurrentSceneKey())
            {
                case "MainMenu":
                    {
                        InterfaceMenu currentForm = (InterfaceMenu)sceneController.GetCurrentScene();
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
                                            string currentSceneKey = "level0";
                                            sceneController.AddScene(currentSceneKey, LoadLevel());
                                            sceneController.SwitchScene(currentSceneKey);                                            
                                            break;
                                        }
                                    case 1:
                                        {
                                            //load = 0;
                                            string currentSceneKey = "Options";
                                            sceneController.SwitchScene(currentSceneKey);                                          
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
                        InterfaceMenu currentForm = (InterfaceMenu)sceneController.GetCurrentScene();
                        currentTimePushed += gameTime.ElapsedGameTime.Milliseconds;
                        if (currentTimePushed > periodPushed)
                        {
                            pushed = false;
                        }
                        if (!pushed)
                        {
                            if (Keyboard.GetState().IsKeyDown(Keys.Up))
                            {
                                currentForm.updateScene(gameTime.ElapsedGameTime.Milliseconds);
                                pushed = true;
                                currentTimePushed = 0;
                            }
                            else if (Keyboard.GetState().IsKeyDown(Keys.Down))
                            {
                                currentForm.updateScene(gameTime.ElapsedGameTime.Milliseconds);
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
                                            SwitchBox s = (SwitchBox)sceneController.GetCurrentScene().objectList[2];//TODO: Да-да, я знаю
                                            s.ChangeIndex(dirValue, gameTime.ElapsedGameTime.Milliseconds);
                                            break;
                                        }
                                    case 1:
                                        {
                                            SwitchBox s = (SwitchBox)sceneController.GetCurrentScene().objectList[4];//TODO: Да-да, я знаю
                                            s.ChangeIndex(dirValue, gameTime.ElapsedGameTime.Milliseconds);
                                            break;
                                        }
                                    case 2:
                                        {
                                            SwitchBox s = (SwitchBox)sceneController.GetCurrentScene().objectList[6];//TODO: Да-да, я знаю
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
                                            SwitchBox s = (SwitchBox)sceneController.GetCurrentScene().objectList[2];
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

                                            s = (SwitchBox)sceneController.GetCurrentScene().objectList[4];
                                            bool fullScreen;
                                            if (SwitchBox.ParseValue(s.GetValue(), out fullScreen))
                                            {
                                                if (graphics.IsFullScreen != fullScreen)
                                                {
                                                    graphics.IsFullScreen = fullScreen;
                                                    graphics.ApplyChanges();
                                                }

                                            }

                                            s = (SwitchBox)sceneController.GetCurrentScene().objectList[6];
                                            float musicVolume;
                                            if (SwitchBox.ParseValue(s.GetValue(), out musicVolume))
                                            {
                                                MediaPlayer.Volume = musicVolume;
                                            }

                                            break;
                                        }
                                    case 4:
                                        {                                            
                                            string currentSceneKey = "MainMenu";
                                            sceneController.SwitchScene(currentSceneKey);
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
                        Level currentLevel = (Level)sceneController.GetCurrentScene();
                        
                        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                        {                          
                            sceneController.SwitchScene("MainMenu");
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
                                sceneController.RemoveScene("level0");
                                sceneController.AddScene("level0", LoadLevel());
                                sceneController.SwitchScene("level0");
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
                act(t);
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
            switch (sceneController.GetCurrentSceneKey())
            {
                case "MainMenu":
                    {
                        int period = 50;
                        currentTime += gameTime.ElapsedGameTime.Milliseconds;

                        string[] gameName = new string[] { "       K.A.R.C.", "Adrenaline Racing" };

                        foreach (var obj in sceneController.GetCurrentScene().objectList)
                        {
                            obj.Value.colDraw = new Color(load, load, load);
                            obj.Value.drawObject(spriteBatch, gameTime.ElapsedGameTime.Milliseconds);
                        }

                        period = 300;
                        if (currentTime < period)
                        {

                        }
                        else if (currentTime > period && currentTime < 2 * period)
                        {
                            BackGround pressStart = (BackGround)sceneController.GetCurrentScene().objectList[1];
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
                        break;

                    }
                case "Options":
                    {
                        currentTime += gameTime.ElapsedGameTime.Milliseconds;

                        foreach (var obj in sceneController.GetCurrentScene().objectList)
                        {

                            obj.Value.colDraw = new Color(load, load, load);
                            obj.Value.drawObject(spriteBatch, gameTime.ElapsedGameTime.Milliseconds);
                        }
                        break;
                    }
                case "level0"://Можно обобщить
                    {
                        foreach (var obj in sceneController.GetCurrentScene().objectList)
                        {
                            if (obj.Value.pos.Y > -1500 && obj.Value.pos.Y < 1500)
                                obj.Value.drawObject(spriteBatch, gameTime.ElapsedGameTime.Milliseconds);
                            if (showhitBox && obj.Value.physical)
                            {
                                PhysicalObject hb = (PhysicalObject)obj.Value;
                                spriteBatch.Draw(Content.Load<Texture2D>("hitBoxBlank"), hb.hitBox, null, Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, 0.3f);
                            }
                        }

                        Car Player = (Car)sceneController.GetCurrentScene().objectList[playerId];
                        int speedKm = -(int)(Player.Speed.Y * 3.6);
                        spriteBatch.DrawString(gameFont, "Управление:\nСтрелки - движение \nLeftCtrl - Показать хитбоксы \nR - Перезагрузить\nEsc - Выход в меню", new Vector2(0, 400), Color.Red);
                        break;
                    }
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
