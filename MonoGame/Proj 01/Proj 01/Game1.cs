using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
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
        
        private Song m_fxBuzz;

        private StringBuilder m_sbDebug;
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
            m_fxBuzz = Content.Load<Song>("buzz");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            m_sbDebug = new StringBuilder(500);

            bool wasMoving = spriteBall.Moving;

            KeyboardState kstate = Keyboard.GetState();


            // Handle keys for movement and work out new velocity
            float gametimeSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;
            float speedPerSecond = spriteBall.Speed;
            float delta = speedPerSecond * gametimeSeconds;
            bool movingKeypressedV = false;
            bool movingKeypressedH = false;
            float deltaX = 0;
            float deltaY = 0;

            m_sbDebug.AppendFormat("Speed: {0} actual delta: {1}", spriteBall.Speed, delta);
            m_sbDebug.AppendLine();

            // Work out wanted deltas if keys are pressed
            // And WHICH keys have been pressed
            if (kstate.IsKeyDown(Keys.W))
            {
                deltaY = -delta;
                movingKeypressedV = true;
            }

            if (kstate.IsKeyDown(Keys.S))
            {
                deltaY += delta;
                movingKeypressedV = true;
            }

            if (kstate.IsKeyDown(Keys.A))
            {
                deltaX = -delta;
                movingKeypressedH = true;
            }

            if (kstate.IsKeyDown(Keys.D))
            {
                deltaX = delta;
                movingKeypressedH = true;
            }

            if(movingKeypressedH)
            {
                // set the current deltas
                spriteBall.CurrentDeltaX = deltaX;
                MapX += deltaX;
            }

            if(movingKeypressedV)
            {
                spriteBall.CurrentDeltaY = deltaY;
                MapY += deltaY;
            }


            // Now deal with keys which are NO LONGER pressed but might had residual movement
            if (!movingKeypressedH)
            {
                if (spriteBall.CurrentDeltaX != 0)
                {
                    // no key pressed but movement exists in this direction.
                    // Reduce it.
                    spriteBall.CurrentDeltaX = spriteBall.CurrentDeltaX * 0.95f;
                    MapX += spriteBall.CurrentDeltaX;
                    if (Math.Abs(spriteBall.CurrentDeltaX) < 0.5)
                    {
                        spriteBall.CurrentDeltaX = 0;
                    }
                }
            }

            if (!movingKeypressedV)
            {

                if (spriteBall.CurrentDeltaY != 0)
                {
                    // no key pressed but movement exists in this direction.
                    // Reduce it.
                    spriteBall.CurrentDeltaY = spriteBall.CurrentDeltaY * 0.95f;
                    MapY += spriteBall.CurrentDeltaY;
                    if (Math.Abs(spriteBall.CurrentDeltaY) < 0.5)
                    {
                        spriteBall.CurrentDeltaY = 0;
                    }
                }
            }

            m_sbDebug.AppendFormat("delta X {0}", spriteBall.CurrentDeltaX);
            m_sbDebug.AppendLine();
            m_sbDebug.AppendFormat("delta Y {0}", spriteBall.CurrentDeltaY);
            m_sbDebug.AppendLine();


            float volume = 1;

            try
            {
                // sound fx depends on movement.
                if (!wasMoving && spriteBall.Moving)
                {
                    // movement started - start hummmm
                    MediaPlayer.Play(m_fxBuzz);
                    MediaPlayer.IsRepeating = true;
                    MediaPlayer.Volume = volume;
                    m_sbDebug.AppendLine("Starting buzz");
                }

                if (wasMoving && spriteBall.Moving)
                {
                    float percentDeltaX = spriteBall.DeltaPercentOfSpeedX(delta);
                    float percentDeltaY = spriteBall.DeltaPercentOfSpeedY(delta);

                    m_sbDebug.AppendFormat("% delta X {0}", percentDeltaX);
                    m_sbDebug.AppendLine();
                    m_sbDebug.AppendFormat("% delta Y {0}", percentDeltaY);
                    m_sbDebug.AppendLine();

                    volume = Math.Max(percentDeltaX,percentDeltaY);
                    volume = volume / 100;
                    //volume = Math.Max(100, volume);
                    MediaPlayer.Volume = volume;
                }

                if (!spriteBall.Moving)
                {
                    m_sbDebug.AppendLine("Not moving: Buzz off");
                    MediaPlayer.Stop();
                }
                else
                {
                    m_sbDebug.AppendFormat("Was Moving: {0}", wasMoving);
                    m_sbDebug.AppendLine();
                    m_sbDebug.AppendFormat("Is Moving:  {0}", spriteBall.Moving);
                    m_sbDebug.AppendLine();
                    m_sbDebug.AppendFormat("Volume: {0}", volume);
                    m_sbDebug.AppendLine();
                    m_sbDebug.AppendFormat("Is Moving:  {1}", spriteBall.Moving);
                    m_sbDebug.AppendLine();
                    m_sbDebug.AppendFormat("Is Moving:  {1}", spriteBall.Moving);
                    m_sbDebug.AppendLine();
                }
            }
            catch { }

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
                m_sbDebug.Append("MapX=");
                m_sbDebug.Append(MapX.ToString());
                m_sbDebug.Append(" Map Y=");
                m_sbDebug.AppendLine(MapY.ToString());
                m_sbDebug.AppendLine(string.Format("Array X={0}, Array Y = {1}", arrayX, arrayY));
                m_sbDebug.AppendLine(string.Format("TileW={0} TileH]{1}", tileW, tileH));


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

                m_sbDebug.AppendLine(string.Format("Blocks per row {0}, rows {1}", blocksPerRow, rows));
                m_sbDebug.AppendLine(string.Format("Framerate {0}", framerate));

                string debugText = m_sbDebug.ToString();

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