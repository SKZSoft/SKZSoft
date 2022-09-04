using System;
using System.Collections.Generic;
using System.Text;
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
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Sprites.Ball spriteBall;
        private Map m_map;
        private SpriteFont font;
        private int tileW = 128;
        private int tileH = 128;

        private float MapX = 0;
        private float MapY = 0;

        public Game1()
        {
            
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            MapX = tileW;
            MapY = tileH;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
            _graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;
            _graphics.IsFullScreen = true;
            _graphics.ApplyChanges();


            //font = Content.Load<SpriteFont>("Fonts/Alef-Regular");
            font = Content.Load<SpriteFont>("File");

            spriteBall = new Sprites.Ball(this, new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2), 300f);


            base.Initialize();
        }


        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            m_map = new Map(64, 64, Content);
            m_map.Load();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


            KeyboardState kstate = Keyboard.GetState();

            
            // Handle keys for movement and work out new velocity
            float speedPerSecond = spriteBall.Speed;
            float delta = speedPerSecond * (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (kstate.IsKeyDown(Keys.W))
            {
                MapY -= delta;
            }

            if (kstate.IsKeyDown(Keys.S))
            {
                MapY += delta;
            }

            if (kstate.IsKeyDown(Keys.A))
            {
                MapX -= delta;
            }

            if (kstate.IsKeyDown(Keys.D))
            {
                MapX += delta;
            }


            // update the ball
            // TODO collision detection and other stuff which may affect the ball?
            spriteBall.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            try
            {
                GraphicsDevice.Clear(Color.CornflowerBlue);

                _spriteBatch.Begin();

                // work out where in the map array we are
                float arrayFloatX = MapX / tileW;
                float arrayFloatY = MapY / tileH;

                // array start positions for drawing the screen
                int arrayX = (int)arrayFloatX;
                int arrayY = (int)arrayFloatY;

                // the remainder is the offset of the sprite
                float backgroundOffsetX = MapX % tileW;
                float backgroundOffsetY = MapY % tileH;

                // Debug info
                StringBuilder sb = new StringBuilder(500);
                sb.Append("MapX=");
                sb.Append(MapX.ToString());
                sb.Append(" Map Y=");
                sb.AppendLine(MapY.ToString());
                sb.AppendLine(string.Format("Array X={0}, Array Y = {1}", arrayX, arrayY));
                sb.AppendLine(string.Format("TileW={0} TileH]{1}", tileW, tileH));


                int blocksPerRow = (int)(_graphics.PreferredBackBufferWidth / tileW) + 4; //TODO work out why this is needed. Maybe due to backgroundoffset?
                int rows = (int)(_graphics.PreferredBackBufferHeight / tileH) + 5;

//                arrayX = Math.Max(-10, arrayX);
//                arrayY = Math.Max(-10, arrayY);

                int arrayXEnd = arrayX + blocksPerRow;
                int arrayYEnd = arrayY + rows;

                arrayXEnd = Math.Min(arrayXEnd, m_map.Width - 1);
                arrayYEnd = Math.Min(arrayYEnd, m_map.Height - 1);
                int spriteCount = 0;

                MapTile[,] mapSection;

                m_map.GetMapSection(arrayX, arrayY, blocksPerRow, rows, out mapSection);

                int screenPixelY = -tileH;
                for (int row = 0; row < rows; row++)
                {
                    float screenPixelX = -tileW;
                    for (int col = 0; col < blocksPerRow; col++)
                    {
                        Vector2 screenpos = new Vector2(screenPixelX - backgroundOffsetX, screenPixelY - backgroundOffsetY);

                        MapTile mapTile = mapSection[col, row];
                        if (mapTile != null)
                        {
                            Sprite sprite = new Sprite(this, screenpos, 0, mapTile.Texture);
                            sprite.Draw(_spriteBatch);
                        }
                        screenPixelX += tileW; ;
                        spriteCount++;
                    }
                    screenPixelY += tileH;
                }

                spriteBall.Draw(_spriteBatch);

                double framerate = (1 / gameTime.ElapsedGameTime.TotalSeconds);

                sb.AppendLine(string.Format("Blocks per row {0}, rows {1}", blocksPerRow, rows));
                sb.AppendLine(string.Format("Framerate {0}", framerate));

                string debugText = sb.ToString();

                // Places text in center of the screen
                Vector2 position = new Vector2(0, 0);
                _spriteBatch.DrawString(font, string.Format(debugText), position, Color.White, 0, position, 1.0f, SpriteEffects.None, 0.5f);

                _spriteBatch.End();



                base.Draw(gameTime);
            }
            catch(Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
            }
        }

    }
}