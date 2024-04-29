using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace monogame_4___time_and_sound
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        MouseState mouseState;

        Texture2D bombTexture, explosionTexture;
        Rectangle bombRect, kaboomRect, window;

        SpriteFont timeFont;
        SoundEffect kaBlooey;

        Color bgColor, kaboomMask;

        float bombSeconds;
        bool esploded;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            window = new Rectangle(0, 0, 800, 500);

            _graphics.PreferredBackBufferWidth = window.Width;
            _graphics.PreferredBackBufferHeight = window.Height;
            _graphics.ApplyChanges();

            bombRect = new Rectangle(50, 50, 700, 400);
            kaboomRect = new Rectangle(0, 0, 800, 500);

            bgColor = Color.Bisque;
            kaboomMask = Color.Transparent;

            bombSeconds = 0;
            esploded = false;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            bombTexture = Content.Load<Texture2D>("bomb");
            explosionTexture = Content.Load<Texture2D>("deltaruneExplosion");
            timeFont = Content.Load<SpriteFont>("timeFont");
            kaBlooey = Content.Load<SoundEffect>("explosion");

        }

        protected override void Update(GameTime gameTime)
        {
            mouseState = Mouse.GetState();
            
            bombSeconds += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                bombSeconds = 0;
            }

            if (bombSeconds >= 10 && !esploded)
            {
                kaBlooey.Play();
                esploded = true;
                kaboomMask = Color.White;
            }
            
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(bgColor);

            // TODO: Add your drawing code here

            _spriteBatch.Begin();

            _spriteBatch.Draw(bombTexture, bombRect, Color.White);
            _spriteBatch.DrawString(timeFont, (10 -bombSeconds).ToString("0:00"), new Vector2(270, 200), Color.Black);
            _spriteBatch.Draw(explosionTexture, kaboomRect, kaboomMask);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}