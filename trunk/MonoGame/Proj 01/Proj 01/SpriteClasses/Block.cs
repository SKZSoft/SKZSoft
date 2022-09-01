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
    internal class Block : IEntity
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

        public Block(Game1 game, Vector2 position, float speed)
        {
            _game = game;
            Position = position;
            Speed = speed;
            Texture = game.Content.Load<Texture2D>("BlockSquareSmall");
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



        public virtual void Update(GameTime gameTime)
        {
            //Bounds.Position += Velocity * gameTime.GetElapsedSeconds() * 50;
        }

        public void OnCollision(CollisionEventArgs collisionInfo)
        {
            //Velocity.X *= -1;
            //Velocity.Y *= -1;
            //Bounds.Position -= collisionInfo.PenetrationVector;
        }


    }
}
