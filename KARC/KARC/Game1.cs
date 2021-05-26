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
        car = 1
    }


    public class Game1 : Game
    {
        Dictionary<string, Texture2D> texturesDict;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        GameMode mode;



        Logic.Object back;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            texturesDict = new Dictionary<string, Texture2D>();
            mode = GameMode.mainMenu;
        }

      


        protected override void Initialize()
        {         
            base.Initialize();
            
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
            spriteBatch.Begin();

            //back.Update(spriteBatch);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
