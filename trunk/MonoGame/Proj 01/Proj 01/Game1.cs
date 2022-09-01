using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Collisions;
using MonoGame.Extended;
using Proj_01.Sprites;
using static System.Net.Mime.MediaTypeNames;


namespace Proj_01
{
    public class Game1 : Game
    {
        //private Texture2D background;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Sprites.Ball spriteBall;
        private List<Sprites.Block> blocks;
        private SpriteFont font;
        private int spriteCount = 0;
        private float backgroundOffsetX = 0;
        private float backgroundOffsetY = 0;
        private int tileW = 32;
        private int tileH = 32;


        public Game1()
        {
            
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
            _graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;
            _graphics.IsFullScreen = true;
            _graphics.ApplyChanges();


            //font = Content.Load<SpriteFont>("Fonts/Alef-Regular");
            font = Content.Load<SpriteFont>("File");


            spriteBall = new Sprites.Ball(this, new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2), 200f);


            RefreshBlocks();
            base.Initialize();
        }


        private void RefreshBlocks()
        {

            int width = GraphicsDevice.DisplayMode.Width;
            int height = GraphicsDevice.DisplayMode.Height;
            spriteCount = 0;

            blocks = new List<Block>();
            int X = -tileW;
            int Y = -tileH;
            width += (tileW * 2);
            height += (tileH * 2);

            while (Y <= height)
            {
                while(X <= width)
                {
                    Sprites.Block block = new Block(this, new Vector2(X, Y), 0);
                    blocks.Add(block);
                    X += tileW;
                    spriteCount++;
                }
                X = -tileW;
                Y += tileH;
            }
        }


    protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


            RefreshBlocks();

            KeyboardState kstate = Keyboard.GetState();

            Vector2 ballPos = spriteBall.Position;
            float speedPerSecond = spriteBall.Speed;
            float delta = speedPerSecond * (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (kstate.IsKeyDown(Keys.Up))
            {
                ballPos.Y += -delta;
                backgroundOffsetY += delta;
            }

            if (kstate.IsKeyDown(Keys.Down))
            {
                ballPos.Y -= delta;
                backgroundOffsetY -= delta;
            }

            if (kstate.IsKeyDown(Keys.Left))
            {
                ballPos.X += delta;
                backgroundOffsetX += delta;
            }

            if (kstate.IsKeyDown(Keys.Right))
            {
                ballPos.X -= delta;
                backgroundOffsetX -= delta;
            }


            if (backgroundOffsetX > tileW)
                backgroundOffsetX -= tileW;

            if (backgroundOffsetX < -tileW)
                backgroundOffsetX += tileW;

            if (backgroundOffsetY > tileH)
                backgroundOffsetY -= tileH;

            if (backgroundOffsetY < -tileH)
                backgroundOffsetY += tileH;




            ballPos.X = Math.Max(ballPos.X, spriteBall.TextureWidth / 2);
            ballPos.X = Math.Min(ballPos.X, _graphics.PreferredBackBufferWidth - spriteBall.TextureWidth / 2);

            ballPos.Y = Math.Max(ballPos.Y, spriteBall.TextureHeight / 2);
            ballPos.Y = Math.Min(ballPos.Y, _graphics.PreferredBackBufferHeight - spriteBall.TextureHeight / 2);

            //spriteBall.Position = ballPos;
            spriteBall.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            //_spriteBatch.Draw(background, new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight), Color.White);


            foreach(Block block in blocks)
            {
                block.OffsetX = backgroundOffsetX;
                block.OffsetY = backgroundOffsetY;
                block.Draw(_spriteBatch);
            }
            spriteBall.Draw(_spriteBatch);


            string text = gameTime.TotalGameTime.ToString();
            double framerate = (1 / gameTime.ElapsedGameTime.TotalSeconds);
            text = framerate.ToString();
            // Finds the center of the string in coordinates inside the text rectangle
            Vector2 textMiddlePoint = font.MeasureString(text) / 2;
            // Places text in center of the screen
            Vector2 position = new Vector2(Window.ClientBounds.Width / 2, Window.ClientBounds.Height / 2);
            _spriteBatch.DrawString(font, string.Format("{0} {1}", spriteCount, text), position, Color.White, 0, textMiddlePoint, 1.0f, SpriteEffects.None, 0.5f);

            _spriteBatch.End();



            base.Draw(gameTime);
        }



        public void OnCollision(CollisionEventArgs collisionInfo)
        {
            //return;
            Block block = collisionInfo.Other as Block;
            if (block != null)
            {
                blocks.Remove(block);
            }
            //Velocity.X *= -1;
            //Velocity.Y *= -1;
            //Bounds.Position -= collisionInfo.PenetrationVector;
        }

    }
}