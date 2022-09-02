using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Collisions;
using MonoGame.Extended;
using Proj_01.Sprites;
using Proj_01.Maps;
using static System.Net.Mime.MediaTypeNames;


namespace Proj_01
{
    public class Game1 : Game
    {
        //private Texture2D background;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Sprites.Ball spriteBall;
        private Tiles[,] map;
        private SpriteFont font;
        private float backgroundOffsetX = 0;
        private float backgroundOffsetY = 0;
        private int tileW = 64;
        private int tileH = 64;

        private float MapX = 0;
        private float MapY = 0;

        Texture2D wallTexture;
        Texture2D floorTexture;

        public Game1()
        {
            
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            MapLoader mapLoader = new MapLoader();
            mapLoader.LoadMap(out map);
            IsMouseVisible = true;

            MapX = tileW;
            MapY = tileH;
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


            base.Initialize();
        }


        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            wallTexture = Content.Load<Texture2D>("Floor01");
            floorTexture = Content.Load<Texture2D>("BlockSquare");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


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

            // work out where in the map array we are
            int arrayX = (int)(MapX / tileW);
            int arrayY = (int)(MapY / tileH);
            int blocksPerRow = (int)(_graphics.PreferredBackBufferWidth / tileW) + 2;
            int rows = (int)(_graphics.PreferredBackBufferHeight / tileH) +2;


            System.Diagnostics.Debug.WriteLine(string.Format("Blocks per row {0}, rows {1}", blocksPerRow, rows));


            int arrayXEnd = arrayX + blocksPerRow;
            int arrayYEnd = arrayY + rows;

            arrayXEnd = Math.Min(arrayXEnd, 499);
            arrayYEnd = Math.Min(arrayYEnd, 199); //TODO need a map class with these as properties
            int spriteCount = 0;

            int screenPixelY = 0;
            for(int row = arrayY; row <= arrayYEnd; row++)
            {
                float screenPixelX = 0;
                for(int col = arrayX; col <= arrayXEnd; col++)
                {
                    Vector2 screenpos = new Vector2(screenPixelX, screenPixelY);

                    Tiles tile = map[row, col];
                    Sprite sprite;
                    switch(tile)
                    {
                        case Tiles.Wall:
                            sprite = new Sprite(this, screenpos, 0, wallTexture);
                            break;

                        case Tiles.Floor:
                            sprite = new Sprite(this, screenpos, 0, floorTexture);
                            break;

                        default:
                            sprite = null;
                            break;

                    }

                    sprite.Draw(_spriteBatch);
                    screenPixelX += tileW; ;
                    spriteCount++;
                }
                screenPixelY+=tileH;
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

    }
}