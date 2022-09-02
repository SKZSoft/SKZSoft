using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proj_01.SpriteClasses
{
    internal class Wall01
    {

        private Texture2D Texture;

        public float OffsetX { get; set; }
        public float OffsetY { get; set; }

        public int TextureHeight
        {
            get { return Texture.Height; }
        }
        public int TextureWidth
        {
            get { return Texture.Width; }
        }

        public Vector2 Position { get; set; }
        public float Speed { get; set; }

        private readonly Game1 _game;
        public IShapeF Bounds { get; }

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public Wall01(Game1 game, Vector2 position, float speed)
        {
            _game = game;
            Position = position;
            Speed = speed;
            Texture = game.Content.Load<Texture2D>("Floor01");
            Bounds = new RectangleF(position.X, position.Y, position.X + TextureWidth, position.Y + TextureHeight);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            try
            {
                Vector2 offsetPos = new Vector2(Position.X + OffsetX, Position.Y + OffsetY);
                spriteBatch.Draw(
                    Texture,
                    offsetPos,
                    null,
                    Color.White,
                    0f,
                    new Vector2(TextureWidth / 2, TextureHeight / 2),
                    Vector2.One,
                    SpriteEffects.None,
                    0f
                );
            }
            catch { System.Diagnostics.Debug.WriteLine("XXX"); }
        }


    }
}
