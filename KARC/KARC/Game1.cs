using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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
        Dictionary<string, Texture2D> texturesDict;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        GameMode mode;

        Dictionary<string, Logic.Scene> scenesDict = new Dictionary<string, Logic.Scene>();

        
        

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

            int[,] mainMenuMap = new int[1, 1];

            List<Logic.Object> objList = new List<Logic.Object>();
            Dictionary<string, Texture2D> textureDict = new Dictionary<string, Texture2D>();


            textureDict.Add("background", Content.Load<Texture2D>("MenuBackGround"));
            SpriteFont gameName = Content.Load<SpriteFont>("Title");
            Dictionary<string, SpriteFont> fontDict = new Dictionary<string, SpriteFont>();
            fontDict.Add("Title",gameName);
            Logic.BackGround backGround = new Logic.BackGround(Vector2.Zero, 1.0f, textureDict, 1,fontDict,50);
            objList.Add(backGround);
            //TODO: период лучше в сцену вставлять. Или туда и туда
            textureDict = new Dictionary<string, Texture2D>();
            textureDict.Add("light", Content.Load<Texture2D>("Start_Select"));
            textureDict.Add("dark", Content.Load<Texture2D>("flatLight41"));
            Logic.Button btnStart = new Logic.Button(new Vector2(370, 200), 0.9f, textureDict, 2, 0, 50);
            btnStart.check = true;
            objList.Add(btnStart);


            textureDict = new Dictionary<string, Texture2D>();
            textureDict.Add("light", Content.Load<Texture2D>("Exit_Select"));
            textureDict.Add("dark", Content.Load<Texture2D>("Exit"));
            Logic.Button btnExit = new Logic.Button(new Vector2(370, 260), 0.9f, textureDict, 3, 1, 50);
            objList.Add(btnExit);

            Logic.InterfaceMenu mainMenu = new Logic.InterfaceMenu(mainMenuMap, 600, objList,100);
            scenesDict.Add("MainMenu", mainMenu);
        }



        
        protected override void LoadContent()
        {
            
            spriteBatch = new SpriteBatch(GraphicsDevice);


            

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


                        break;
                    }
            }

            base.Update(gameTime);
        }



      
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin(SpriteSortMode.BackToFront);

            //spriteBatch.Begin();

            switch (mode)
            {
                case GameMode.mainMenu:
                    {
                        foreach (var obj in scenesDict["MainMenu"].objectList)
                        {
                            obj.drawObject(spriteBatch);

                        }
                        Logic.BackGround title = (Logic.BackGround)scenesDict["MainMenu"].objectList[0];
                        title.drawString("Title", "K.A.R.C.", new Vector2(350, 150), Color.Red, spriteBatch);
                        break;
                    }
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
