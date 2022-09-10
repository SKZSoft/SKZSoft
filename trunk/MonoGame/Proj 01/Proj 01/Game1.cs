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
        private Sprites.Ball _spriteBall;
        private Map _map;
        private SpriteFont _font;
        private int _tileW = 128;
        private int _tileH = 128;
        private Vector2 _ballCentre;
        private Song _fxBuzz;
        private MapTile[,] _lastMapSection;


        private StringBuilder _sbDebug;
        private StringBuilder _sbExceptions;

        private float _mapX = 0;
        private float _mapY = 0;

        public Game1()
        {
            
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            _mapX = _tileW;
            _mapY = _tileH;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
            _graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;
            _graphics.IsFullScreen = true;
            _graphics.ApplyChanges();


            //font = Content.Load<SpriteFont>("Fonts/Alef-Regular");
            _font = Content.Load<SpriteFont>("File");

            _ballCentre = new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2);
            _spriteBall = new Sprites.Ball(this, _ballCentre, 300f);

            _sbExceptions = new StringBuilder(1000);
            base.Initialize();
        }


        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _map = new Map(64, 64, Content);
            _map.Load();
            _fxBuzz = Content.Load<Song>("buzz");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _sbDebug = new StringBuilder(500);

            bool wasMoving = _spriteBall.Moving;

            KeyboardState kstate = Keyboard.GetState();


            // Handle keys for movement and work out new velocity
            float gametimeSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;
            float speedPerSecond = _spriteBall.Speed;
            float delta = speedPerSecond * gametimeSeconds;
            bool movingKeypressedV = false;
            bool movingKeypressedH = false;
            float deltaX = 0;
            float deltaY = 0;

            _sbDebug.AppendFormat("Speed: {0} actual delta: {1}", _spriteBall.Speed, delta);
            _sbDebug.AppendLine();

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
                _spriteBall.CurrentDeltaX = deltaX;
                _mapX += deltaX;
            }

            if(movingKeypressedV)
            {
                _spriteBall.CurrentDeltaY = deltaY;
                _mapY += deltaY;
            }

            // TODO work out which map tiles are newly touched - and so they block?

            // Now deal with keys which are NO LONGER pressed but might had residual movement
            if (!movingKeypressedH)
            {
                if (_spriteBall.CurrentDeltaX != 0)
                {
                    // no key pressed but movement exists in this direction.
                    // Reduce it.
                    _spriteBall.CurrentDeltaX = _spriteBall.CurrentDeltaX * 0.95f;
                    _mapX += _spriteBall.CurrentDeltaX;
                    if (Math.Abs(_spriteBall.CurrentDeltaX) < 0.5)
                    {
                        _spriteBall.CurrentDeltaX = 0;
                    }
                }
            }

            if (!movingKeypressedV)
            {

                if (_spriteBall.CurrentDeltaY != 0)
                {
                    // no key pressed but movement exists in this direction.
                    // Reduce it.
                    _spriteBall.CurrentDeltaY = _spriteBall.CurrentDeltaY * 0.95f;
                    _mapY += _spriteBall.CurrentDeltaY;
                    if (Math.Abs(_spriteBall.CurrentDeltaY) < 0.5)
                    {
                        _spriteBall.CurrentDeltaY = 0;
                    }
                }
            }

            _sbDebug.AppendFormat("delta X {0}", _spriteBall.CurrentDeltaX);
            _sbDebug.AppendLine();
            _sbDebug.AppendFormat("delta Y {0}", _spriteBall.CurrentDeltaY);
            _sbDebug.AppendLine();


            float volume = 1;
            _sbExceptions = new StringBuilder(500);
            try
            {
                // sound fx depends on movement.
                if (!wasMoving && _spriteBall.Moving)
                {
                    // movement started - start hummmm
                    MediaPlayer.Play(_fxBuzz);
                    MediaPlayer.IsRepeating = true;
                    MediaPlayer.Volume = volume;
                }

                float percentDeltaX = 0;
                float percentDeltaY = 0;
                if (wasMoving && _spriteBall.Moving)
                {
                    percentDeltaX = _spriteBall.DeltaPercentOfSpeedX(delta);
                    percentDeltaY = _spriteBall.DeltaPercentOfSpeedY(delta);

                    volume = Math.Max(percentDeltaX,percentDeltaY);
                    volume = volume / 100;
                    MediaPlayer.Volume = volume;
                }
                _sbDebug.AppendFormat("% delta X {0}", percentDeltaX);
                _sbDebug.AppendLine();
                _sbDebug.AppendFormat("% delta Y {0}", percentDeltaY);
                _sbDebug.AppendLine();

                if (!_spriteBall.Moving)
                {
                    MediaPlayer.Stop();
                }
                _sbDebug.AppendFormat("Was Moving: {0}", wasMoving);
                _sbDebug.AppendLine();
                _sbDebug.AppendFormat("Is Moving:  {0}", _spriteBall.Moving);
                _sbDebug.AppendLine();
                _sbDebug.AppendFormat("Volume:     {0}", volume);
                _sbDebug.AppendLine();
            }
            catch (Exception e)
            {
                _sbExceptions.AppendLine(e.Message);
            }

            // update the ball
            // TODO collision detection and other stuff which may affect the ball?
            _spriteBall.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            try
            {
                GraphicsDevice.Clear(Color.CornflowerBlue);

                _spriteBatch.Begin();

                // work out where in the map array we are
                float arrayFloatX = _mapX / _tileW;
                float arrayFloatY = _mapY / _tileH;

                // array start positions for drawing the screen
                int arrayX = (int)arrayFloatX;
                int arrayY = (int)arrayFloatY;

                // the remainder is the offset of the sprite the map must be scrolled by
                float backgroundOffsetX = _mapX % _tileW;
                float backgroundOffsetY = _mapY % _tileH;

                // Debug info
                _sbDebug.Append("MapX=");
                _sbDebug.Append(_mapX.ToString());
                _sbDebug.Append(" Map Y=");
                _sbDebug.AppendLine(_mapY.ToString());
                _sbDebug.AppendLine(string.Format("Array X={0}, Array Y = {1}", arrayX, arrayY));
                _sbDebug.AppendLine(string.Format("TileW={0} TileH]{1}", _tileW, _tileH));


                int blocksPerRow = (int)(_graphics.PreferredBackBufferWidth / _tileW) + 4; //TODO work out why this is needed. Maybe due to backgroundoffset?
                int rows = (int)(_graphics.PreferredBackBufferHeight / _tileH) + 5;

                int arrayXEnd = arrayX + blocksPerRow;
                int arrayYEnd = arrayY + rows;

                arrayXEnd = Math.Min(arrayXEnd, _map.Width - 1);
                arrayYEnd = Math.Min(arrayYEnd, _map.Height - 1);
                int spriteCount = 0;


                MapTile[,] mapSection;
                _map.GetMapSection(arrayX, arrayY, blocksPerRow, rows, out mapSection);

                // store this section for the next update call
                _lastMapSection = mapSection;

                int screenPixelY = -_tileH;
                for (int row = 0; row < rows; row++)
                {
                    float screenPixelX = -_tileW;
                    for (int col = 0; col < blocksPerRow; col++)
                    {
                        Vector2 screenpos = new Vector2(screenPixelX - backgroundOffsetX, screenPixelY - backgroundOffsetY);

                        MapTile mapTile = mapSection[col, row];
                        if (mapTile != null)
                        {
                            Sprite sprite = new Sprite(this, screenpos, 0, mapTile.Texture);
                            sprite.Draw(_spriteBatch);
                        }
                        screenPixelX += _tileW; 
                        spriteCount++;
                    }
                    screenPixelY += _tileH;
                }

                _spriteBall.Draw(_spriteBatch);

                double framerate = (1 / gameTime.ElapsedGameTime.TotalSeconds);

                _sbDebug.AppendLine(string.Format("Blocks per row {0}, rows {1}", blocksPerRow, rows));
                _sbDebug.AppendLine(string.Format("Framerate {0}", framerate));

                _sbDebug.AppendLine(_sbExceptions.ToString());
                string debugText = _sbDebug.ToString();

                // Places text in center of the screen
                Vector2 position = new Vector2(0, 0);

                // draw debug text
                _spriteBatch.DrawString(_font, string.Format(debugText), position, Color.White);
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