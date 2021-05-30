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
            graphics.PreferredBackBufferWidth = 1024;
            graphics.PreferredBackBufferHeight = 768;
            graphics.ApplyChanges();

            windoWidth = Window.ClientBounds.Width;
            windowHeight = Window.ClientBounds.Height;

            musicList = new Song[2];
            musicList[0] = Content.Load<Song>("ME");
            musicList[1] = Content.Load<Song>("DemonSpeeding");

            //===================Загрузка начального экрана
            int[,] map = new int[1, 1];

            List<Logic.Object> objList = new List<Logic.Object>();
            Dictionary<string, Texture2D> textureDict = new Dictionary<string, Texture2D>();
            textureDict.Add("background", Content.Load<Texture2D>("MenuBackGround"));

            SpriteFont gameName = Content.Load<SpriteFont>("Title");
            Dictionary<string, SpriteFont> fontDict = new Dictionary<string, SpriteFont>();
            fontDict.Add("Title", gameName);
            fontDict.Add("ManualFont", Content.Load<SpriteFont>("ManualFont"));

            Logic.BackGround backGround = new Logic.BackGround(Vector2.Zero, 1.0f, textureDict, 1, fontDict, 50);
            objList.Add(backGround);

            //TODO: период лучше в сцену вставлять. Или туда и туда
            textureDict = new Dictionary<string, Texture2D>();
            textureDict.Add("light", Content.Load<Texture2D>("Start_Select"));
            textureDict.Add("dark", Content.Load<Texture2D>("StartButton"));
            Logic.Button btnStart = new Logic.Button(new Vector2(windoWidth / 2 - 30, windowHeight / 2 - 100), 0.9f, textureDict, 2, 0, 50);
            //Logic.Button btnStart = new Logic.Button(new Vector2(100, 100), 0.9f, textureDict, 2, 0, 50);
            btnStart.check = true;
            objList.Add(btnStart);


            textureDict = new Dictionary<string, Texture2D>();
            textureDict.Add("light", Content.Load<Texture2D>("Exit_Select"));
            textureDict.Add("dark", Content.Load<Texture2D>("Exit"));
            Logic.Button btnExit = new Logic.Button(new Vector2(windoWidth / 2 - 30, windowHeight / 2 - 40), 0.9f, textureDict, 3, 1, 50);
            objList.Add(btnExit);

            Logic.InterfaceMenu mainMenu = new Logic.InterfaceMenu(map, 600, objList, 100);
            //mainMenu.song = Content.Load<Song>("ME");
            //song = Content.Load<Song>("ME");
            scenesDict.Add("MainMenu", mainMenu);
            //==============================Конец





            //===================Загрузка игры


            //==============================Конец
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
                            obj.colDraw = new Color(load, load, load);
                            obj.drawObject(spriteBatch);
                            if (load >= 255)
                            {
                                Logic.BackGround title = (Logic.BackGround)scenesDict["MainMenu"].objectList[0];
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
                                Logic.BackGround pressStart = (Logic.BackGround)scenesDict["MainMenu"].objectList[0];
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
                        spriteBatch.DrawString(Content.Load<SpriteFont>("Title"), "Игра запущена", Vector2.Zero, Color.AliceBlue);
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




                        break;
                    }
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
