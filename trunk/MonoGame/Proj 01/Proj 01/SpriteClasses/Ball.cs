using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Collections;
using MonoGame.Extended.Collisions;
using MonoGame.Extended;
using Proj_01.Interfaces;
using System;
using System.Reflection.Metadata;


namespace Proj_01.Sprites
{
    internal class Ball : IEntity 
    {
        private Texture2D Texture;

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

        public Ball(Game1 game, Vector2 position, float speed)
        {
            _game = game;
            Position = position;
            Speed = speed;
            Texture = game.Content.Load<Texture2D>("ball");
            Bounds = new RectangleF(position.X, position.Y, TextureWidth, TextureHeight);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            try
            {
                spriteBatch.Draw(
                    Texture,
                    Position,
                    null,
                    Color.White,
                    0f,
                    new Vector2(TextureWidth / 2, TextureHeight / 2),
                    Vector2.One,
                    SpriteEffects.None,
                    0f
                );
            }
            catch { }
        }

        public virtual void Update(GameTime gameTime)
        {
            Bounds.Position = new Point2(Position.X, Position.Y);
            //Bounds.Position += Velocity * gameTime.GetElapsedSeconds() * 50;
        }

        public void OnCollision(CollisionEventArgs collisionInfo)
        {
            _game.OnCollision(collisionInfo);

            //Velocity.X *= -1;
            //Velocity.Y *= -1;
            //Bounds.Position -= collisionInfo.PenetrationVector;
        }

    }
}
