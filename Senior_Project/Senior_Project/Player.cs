using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Senior_Project
{
    public class Player
    {
        public Texture2D texture;
        public Vector2 position;
        public int speed;
        public float rotation;
        public Vector2 origin;
        //public float bulletDelay;
        public Rectangle boundingBox;
        const int innerWallBound = 125;

        public Player()
        {
            texture = null;
            speed = 5;
            position = new Vector2(300, 300);
        }

        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("batDoug5");
        }

        public void Draw(SpriteBatch sprtBatch)
        {
            origin.X = texture.Width / 2;
            origin.Y = texture.Height / 2;
            sprtBatch.Draw(texture, position, null, Color.White, rotation, origin, 1.0f, SpriteEffects.None, 0f);
            //sprtBatch.Draw(texture, position, Color.White);
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState keyState = Keyboard.GetState();
            if (keyState.IsKeyDown(Keys.W))
            {
                rotation = ((float)Math.PI / 2.0f) * 4;
                //angle = (float)Math.PI / 2.0f;  // 90 degrees
                //scale = 1.0f;
                //if (keyState.IsKeyDown(Keys.Up))
                //{
                //    rotation = ((float)Math.PI / 2.0f) * 3;
                //}
                //else if (keyState.IsKeyDown(Keys.Right))
                //{
                //    rotation = ((float)Math.PI / 2.0f) * 4;
                //}
                //else if (keyState.IsKeyDown(Keys.Left))
                //{
                //    rotation = ((float)Math.PI / 2.0f) * 2;
                //}
                //else if (keyState.IsKeyDown(Keys.Down))
                //{
                //    rotation = ((float)Math.PI / 2.0f);
                //}
                //else
                //{
                //    rotation = ((float)Math.PI / 2.0f) * 3;
                //}


                position.Y = position.Y - speed;
            }
            if (keyState.IsKeyDown(Keys.A))
            {

                rotation = ((float)Math.PI / 2.0f) * 3;

                //rotation = ((float)Math.PI / 2.0f) * 2;
                position.X = position.X - speed;
            }
            if (keyState.IsKeyDown(Keys.S))
            {

                rotation = ((float)Math.PI / 2.0f) * 2;


                position.Y = position.Y + speed;
            }
            if (keyState.IsKeyDown(Keys.D))
            {
                rotation = ((float)Math.PI / 2.0f);
                //rotation = ((float)Math.PI / 2.0f) * 4;
                position.X = position.X + speed;
            }

            if (keyState.IsKeyDown(Keys.Up))
            {
                rotation = ((float)Math.PI / 2.0f) * 4;
            }
            else if (keyState.IsKeyDown(Keys.Right))
            {
                rotation = ((float)Math.PI / 2.0f);
            }
            else if (keyState.IsKeyDown(Keys.Left))
            {
                rotation = ((float)Math.PI / 2.0f) * 3;
            }
            else if (keyState.IsKeyDown(Keys.Down))
            {
                rotation = ((float)Math.PI / 2.0f) * 2;
            }


            /*
            if(position.X <= 0)
            {
                position.X = 0;
            }
            if(position.X >= 800 - texture.Width)
            {
                position.X = 800 - texture.Width;
            }
            if(position.Y <= 0)
            {
                position.Y = 0;
            }
            if(position.Y >= 650 - texture.Height)
            {
                position.Y = 650 - texture.Height;
            }
            */
            //960 x 832
            //room size

            if (position.X <= (64) + texture.Width / 2)
            {
                position.X = (64) + texture.Width / 2;
            }
            if (position.X >= (960 - (64)) - texture.Width / 2)
            {
                position.X = (960 - (64)) - texture.Width / 2;
            }
            if (position.Y <= (64) + texture.Height / 2)
            {
                position.Y = (64) + texture.Height / 2;
            }
            if (position.Y >= (832 - (64)) - texture.Height / 2)
            {
                position.Y = (832 - (64)) - texture.Height / 2;
            }

        }
    }
}