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
            Logic.Object backGround = new Logic.Object(Vector2.Zero, 1.0f, textureDict, 1);
            objList.Add(backGround);

            Logic.Scene mainMenu = new Logic.Scene(mainMenuMap, 600, objList);
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
                        break;
                    }
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
