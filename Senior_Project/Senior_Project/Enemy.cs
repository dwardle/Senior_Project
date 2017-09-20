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
    /*Texture2D pixel;

// Somewhere in your LoadContent() method:
pixel = new Texture2D(GameBase.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
pixel.SetData(new[] { Color.White }); // so that we can draw whatever color we want on top of it


in draw method
// Create any rectangle you want. Here we'll use the TitleSafeArea for fun.
Rectangle titleSafeRectangle = GraphicsDevice.Viewport.TitleSafeArea;

// Call our method (also defined in this blog-post)
DrawBorder(titleSafeRectangle, 5, Color.Red);

        actual method

private void DrawBorder(Rectangle rectangleToDraw, int thicknessOfBorder, Color borderColor)
{
    // Draw top line
    spriteBatch.Draw(pixel, new Rectangle(rectangleToDraw.X, rectangleToDraw.Y, rectangleToDraw.Width, thicknessOfBorder), borderColor);

    // Draw left line
    spriteBatch.Draw(pixel, new Rectangle(rectangleToDraw.X, rectangleToDraw.Y, thicknessOfBorder, rectangleToDraw.Height), borderColor);

    // Draw right line
    spriteBatch.Draw(pixel, new Rectangle((rectangleToDraw.X + rectangleToDraw.Width - thicknessOfBorder),
                                    rectangleToDraw.Y,
                                    thicknessOfBorder,
                                    rectangleToDraw.Height), borderColor);
    // Draw bottom line
    spriteBatch.Draw(pixel, new Rectangle(rectangleToDraw.X,
                                    rectangleToDraw.Y + rectangleToDraw.Height - thicknessOfBorder,
                                    rectangleToDraw.Width,
                                    thicknessOfBorder), borderColor);
}


 */
    public class Enemy
    {
        //enemy size 64 x 56
        public Texture2D m_Texture;
        public Vector2 m_Position;
        public bool m_IsAlive = true;
        public bool m_IsActive = false;
        public float m_Health;
        public float m_Speed;
        public float m_Damage = 0.5f;
        public float m_Rotation = 0;
        public Vector2 m_EnemyOrigin = new Vector2();
        public Rectangle m_BoundingBox = new Rectangle();
        public int m_MoveDelay = 0;
        public int m_MoveCount = 10;



        public Enemy()
        {
            m_Texture = null;
            m_Speed = 2;
            m_Health = 10;
            m_IsAlive = true;
        }

        /*public void LoadContent(ContentManager a_Content)
        {
            m_Texture = a_Content.Load<Texture2D>()
        }*/
        public void Draw(SpriteBatch a_SpriteBatch)
        {
            //GraphicsDevice x;
            //var rect = new Texture2D(x, 1, 1);
            
            m_EnemyOrigin.X = m_Texture.Width / 2;
            m_EnemyOrigin.Y = m_Texture.Height / 2;
            a_SpriteBatch.Draw(m_Texture, m_Position, null, Color.White, m_Rotation, m_EnemyOrigin, 1.0f, SpriteEffects.None, 0f);
        }

        public void SetTexture(Texture2D a_Texture)
        {
            m_Texture = a_Texture;
        }

        public void SetPosition(float a_Xpos, float a_Ypos)
        {
            m_Position.X = a_Xpos;
            m_Position.Y = a_Ypos;
        }

        public void SetRotation(char a_Direction)
        {
            if (a_Direction == 'W')
            {
                m_Rotation = ((float)Math.PI / 2.0f) * 4;
                //this.m_BoundingBox = new Rectangle((int)m_Position.X, (int)m_Position.Y, this.m_Texture.Width, this.m_Texture.Height);
            }
            if (a_Direction == 'A')
            {
                // need to swap width and height to compensate for the rotation of the texture
                m_Rotation = ((float)Math.PI / 2.0f) * 3;
                //this.m_BoundingBox = new Rectangle((int)m_Position.X, (int)m_Position.Y, this.m_Texture.Height, this.m_Texture.Width);
            }
            if (a_Direction == 'S')
            {
                m_Rotation = ((float)Math.PI / 2.0f) * 2;
                //this.m_BoundingBox = new Rectangle((int)m_Position.X, (int)m_Position.Y, this.m_Texture.Width, this.m_Texture.Height);
            }
            if (a_Direction == 'D')
            {
                // need to swap width and height to compensate for the rotation of the texture
                m_Rotation = ((float)Math.PI / 2.0f);
                //this.m_BoundingBox = new Rectangle((int)m_Position.X, (int)m_Position.Y, this.m_Texture.Height, this.m_Texture.Width);
            }
        }

        public void SetIsActive(bool a_IsActive)
        {
            m_IsActive = a_IsActive;
        }

        public void SetMoveDelay(int a_MoveDelay)
        {
            m_MoveDelay = a_MoveDelay;
        }

        public int GetMoveDelay()
        {
            return m_MoveDelay;
        }

        public void MoveToPlayer(Player a_MainPlayer, List<Enemy> a_RoomEnemies)
        {
            //move right, toward player

            //testing bounding box *
            //this.SetRotation('W');// * = line that will be deleted. # = line that will be put back
            if (a_MainPlayer.m_PlayerPosition.X > this.m_Position.X)
            {
                this.SetRotation('D'); // #

                //when facing left or right, the bounding box height and width need to be swapped to make it correct with the rotation
                this.m_BoundingBox = new Rectangle((int)this.m_Position.X - (this.m_Texture.Width/2), 
                    (int)this.m_Position.Y - (this.m_Texture.Height/2), this.m_Texture.Height, this.m_Texture.Width);
                m_Position.X = m_Position.X + m_Speed; // #
            }
            
            //move down, toward player
            if (a_MainPlayer.m_PlayerPosition.Y > this.m_Position.Y)
            {
                this.SetRotation('S'); // #
                //this.m_BoundingBox = new Rectangle((int)this.m_Position.X - 32, (int)this.m_Position.Y - 28, this.m_Texture.Width, this.m_Texture.Height);
                this.m_BoundingBox = new Rectangle((int)this.m_Position.X - (this.m_Texture.Width / 2), 
                    (int)this.m_Position.Y - (this.m_Texture.Height / 2), this.m_Texture.Width, this.m_Texture.Height);
                m_Position.Y = m_Position.Y + m_Speed; // #
            }

            //move left toward player,
            if (a_MainPlayer.m_PlayerPosition.X < this.m_Position.X)
            {
                this.SetRotation('A');// #
                this.m_BoundingBox = new Rectangle((int)this.m_Position.X - (this.m_Texture.Width / 2), 
                    (int)this.m_Position.Y - (this.m_Texture.Height / 2), this.m_Texture.Height, this.m_Texture.Width);
                m_Position.X = m_Position.X - m_Speed;// #
            }

            //move up, toward player
            if (a_MainPlayer.m_PlayerPosition.Y < this.m_Position.Y)
            {
                this.SetRotation('W');// #
                this.m_BoundingBox = new Rectangle((int)this.m_Position.X - (this.m_Texture.Width / 2), 
                    (int)this.m_Position.Y - (this.m_Texture.Height / 2), this.m_Texture.Width, this.m_Texture.Height);
                m_Position.Y = m_Position.Y - m_Speed;// #
            }
        }

        public void TakeDamage(float a_Damage)
        {
            m_Health -= a_Damage;
            if(m_Health <= 0)
            {
                this.m_IsAlive = false;
            }
        }
    }
}
