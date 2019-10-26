using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace VorpalWorlds
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Ninja ninja;

        List<Texture2D> runningImages;
        List<Texture2D> idle;

        Texture2D crosshair;

        Vector2 crosshairPos;

        int staminaAmountG = 7;

        Stamina stamina;

        Texture2D backLobby;
        KeyboardState ks;
        KeyboardState lks;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = 600;
            graphics.PreferredBackBufferWidth = 800;
            graphics.ApplyChanges();
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            stamina = new Stamina(Content.Load<Texture2D>("IndividualStamina23"), new Vector2(20, 20), Content.Load<Texture2D>("StaminaBar"));

            crosshair = Content.Load<Texture2D>("cur5");

            runningImages = new List<Texture2D>();

            idle = new List<Texture2D>();

            for (int i = 0; i < 6; i++)
            {
                runningImages.Add(Content.Load<Texture2D>($"Run__00{i}"));
            }

            for (int i = 0; i < 10; i++)
            {
                idle.Add(Content.Load<Texture2D>($"Idle__00{i}"));
            }

            ninja = new Ninja(Content.Load<Texture2D>("Idle__001"), new Vector2(50, 380), Content.Load<Texture2D>("Slide__001"), Content.Load<Texture2D>("Jump__006"), runningImages, idle, Content.Load<Texture2D>("IndividualStamina23"));
            backLobby = Content.Load<Texture2D>("scienceLab");
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            MouseState ms = Mouse.GetState();
            crosshairPos = new Vector2(ms.X, ms.Y);

            ks = Keyboard.GetState();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            ninja.Update(ks, lks, GraphicsDevice.Viewport.Bounds, gameTime);

            staminaAmountG = stamina.checkStamina();

            lks = ks;
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();



            // TODO: Add your drawing code here

            spriteBatch.Draw(backLobby, new Vector2(0, 0), Color.White);

            ninja.Draw(spriteBatch);

            //stamina.Draw(spriteBatch);

            spriteBatch.Draw(crosshair, crosshairPos, null, Color.White, 0f, new Vector2(4, 4), new Vector2(1.2f, 1.2f), SpriteEffects.None, 0f);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}