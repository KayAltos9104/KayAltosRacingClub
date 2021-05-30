using Microsoft.Xna.Framework;
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



    public enum objType: byte
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
         
        Dictionary<string, Logic.Scene> scenesDict = new Dictionary<string, Logic.Scene>();

        public static int windoWidth;
        public static int windowHeight;

        int currentTime = 0;

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
            graphics.PreferredBackBufferHeight =768;
            graphics.ApplyChanges();

            windoWidth = Window.ClientBounds.Width;
            windowHeight = Window.ClientBounds.Height;


            int[,] map = new int[1, 1];

            List<Logic.Object> objList = new List<Logic.Object>();
            Dictionary<string, Texture2D> textureDict = new Dictionary<string, Texture2D>();


            //===================Загрузка начального экрана

            textureDict.Add("background", Content.Load<Texture2D>("MenuBackGround"));

            SpriteFont gameName = Content.Load<SpriteFont>("Title");
            Dictionary<string, SpriteFont> fontDict = new Dictionary<string, SpriteFont>();
            fontDict.Add("Title",gameName);
            Logic.BackGround backGround = new Logic.BackGround(Vector2.Zero, 1.0f, textureDict, 1,fontDict,50);
            objList.Add(backGround);

            //TODO: период лучше в сцену вставлять. Или туда и туда
            textureDict = new Dictionary<string, Texture2D>();
            textureDict.Add("light", Content.Load<Texture2D>("Start_Select"));
            textureDict.Add("dark", Content.Load<Texture2D>("StartButton"));
            Logic.Button btnStart = new Logic.Button(new Vector2(windoWidth/2-30, windowHeight/2-100), 0.9f, textureDict, 2, 0, 50);
            //Logic.Button btnStart = new Logic.Button(new Vector2(100, 100), 0.9f, textureDict, 2, 0, 50);
            btnStart.check = true;
            objList.Add(btnStart);


            textureDict = new Dictionary<string, Texture2D>();
            textureDict.Add("light", Content.Load<Texture2D>("Exit_Select"));
            textureDict.Add("dark", Content.Load<Texture2D>("Exit"));
            Logic.Button btnExit = new Logic.Button(new Vector2(windoWidth / 2 - 30, windowHeight / 2 - 40), 0.9f, textureDict, 3, 1, 50);
            objList.Add(btnExit);

            Logic.InterfaceMenu mainMenu = new Logic.InterfaceMenu(map, 600, objList,100);
            //mainMenu.song = Content.Load<Song>("ME");
            song = Content.Load<Song>("ME");
            scenesDict.Add("MainMenu", mainMenu);
            //==============================Конец


            //===================Загрузка игры


            //==============================Конец
        }




        protected override void LoadContent()
        {
            
            spriteBatch = new SpriteBatch(GraphicsDevice);


            //if (mode == GameMode.mainMenu)
            //{

            //}
            //if (scenesDict["MainMenu"].song != null)
            //{
            //    MediaPlayer.Play(scenesDict["MainMenu"].song);
            //    // повторять после завершения
            //    MediaPlayer.IsRepeating = true;
            //}
            //back = new Logic.Object(Vector2.Zero,1.0f);
            //texturesDict.Add("InitialBackGround", Content.Load<Texture2D>("MenuBackGround"));
            //back.loadImages("Back", texturesDict["InitialBackGround"]);

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
                        //if (scenesDict["MainMenu"].song != null)
                       // {
                            //MediaPlayer.Play(song);
                            // повторять после завершения
                            //MediaPlayer.IsRepeating = true;
                       // }

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

                        if (Keyboard.GetState().IsKeyDown(Keys.Space)|| Keyboard.GetState().IsKeyDown(Keys.Enter))
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
                        
                            foreach (var obj in scenesDict["MainMenu"].objectList)
                            {
                                obj.colDraw = new Color(load, load, load);
                                obj.drawObject(spriteBatch);
                            //if (load >= 255)
                           // {
                                Logic.BackGround title = (Logic.BackGround)scenesDict["MainMenu"].objectList[0];
                                title.drawString("Title", "K.A.R.C.", new Vector2(windoWidth / 2 - 50, windowHeight / 2 - 150), new Color(load, 0, 0), spriteBatch);
                           // }
                            
                        }
                           
                        if (currentTime > period)
                        {
                            currentTime = 0;
                            if (load < 255)
                            {
                                load+=3;
                            }                           
                                }
                        

                        break;
                    }
                case GameMode.game:
                    {
                        spriteBatch.DrawString(Content.Load<SpriteFont>("Title"), "Игра запущена", Vector2.Zero, Color.Black);
                       
                        break;
                    }
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
