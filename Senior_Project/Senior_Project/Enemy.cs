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
    
    public class Enemy
    {
        public Texture2D m_Texture;
        public Vector2 m_Position;
        public bool m_IsAlive = true;
        public bool m_IsActive = false;
        public float m_Health;
        public float m_Speed;
        public float m_Rotation = 0;
        public Vector2 m_EnemyOrigin = new Vector2();
        public Rectangle m_BoundingBox = new Rectangle();
        public int m_MoveDelay = 0;
        public int m_MoveCount = 10;
        //public int m_TimeTillDelay = 10;
        //public Random m_MoveRand = new Random();

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
            }
            if (a_Direction == 'A')
            {
                m_Rotation = ((float)Math.PI / 2.0f) * 3;
            }
            if (a_Direction == 'S')
            {
                m_Rotation = ((float)Math.PI / 2.0f) * 2;
            }
            if (a_Direction == 'D')
            {
                m_Rotation = ((float)Math.PI / 2.0f);
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

        public void MoveToPlayer(Player a_MainPlayer, List<Enemy> a_RoomEnemies)//, Rooms a_CurrentRoom)
        {
            /*for(int i = 0; i < a_RoomEnemies.Capacity; i++)
            {
                if(a_RoomEnemies[i] == this)
                {
                    a_RoomEnemies.Remove(a_RoomEnemies[i]);
                    //a_RoomEnemies.TrimExcess();
                    break;
                }
            }*/
            //move right, toward player

            //bool touching = false;
            
            
            if (a_MainPlayer.m_PlayerPosition.X > this.m_Position.X)
            {
                
                //for(int i = 0; i < a_RoomEnemies.Capacity; i++)
                //{
                //    if(this.m_BoundingBox.Intersects(a_RoomEnemies[i].m_BoundingBox) && a_RoomEnemies[i] != this)
                //    {
                //        touching = true;
                //    }
                //    else if (a_RoomEnemies[i] != this)
                //    {
                //        touching = false;
                //    }
                //}
                //if(!touching)
                //{
                //    this.SetRotation('D');
                //    m_Position.X = m_Position.X + m_Speed;
                //}
                this.SetRotation('D');
                m_Position.X = m_Position.X + m_Speed;
            }
            
            //move down, toward player
            if (a_MainPlayer.m_PlayerPosition.Y > this.m_Position.Y)
            {
                //for (int i = 0; i < a_RoomEnemies.Capacity; i++)
                //{
                //    if (this.m_BoundingBox.Intersects(a_RoomEnemies[i].m_BoundingBox) && a_RoomEnemies[i] != this)
                //    {
                //        touching = true;
                //    }
                //    else if(a_RoomEnemies[i] != this)
                //    {
                //        touching = false;
                //    }
                //}
                //if(!touching)
                //{
                //    this.SetRotation('S');
                //    m_Position.Y = m_Position.Y + m_Speed;
                //}
                this.SetRotation('S');
                m_Position.Y = m_Position.Y + m_Speed;
            }

            //move left toward player,
            if (a_MainPlayer.m_PlayerPosition.X < this.m_Position.X)
            {
                //for (int i = 0; i < a_RoomEnemies.Capacity; i++)
                //{
                //    if (this.m_BoundingBox.Intersects(a_RoomEnemies[i].m_BoundingBox) && a_RoomEnemies[i] != this)
                //    {
                //        touching = true;
                //    }
                //    else if (a_RoomEnemies[i] != this)
                //    {
                //        touching = false;
                //    }

                //}
                //if (!touching)
                //{
                //    this.SetRotation('A');
                //    m_Position.X = m_Position.X - m_Speed;
                //}
                this.SetRotation('A');
                m_Position.X = m_Position.X - m_Speed;
            }

            //move up, toward player
            if (a_MainPlayer.m_PlayerPosition.Y < this.m_Position.Y)
            {
                //for (int i = 0; i < a_RoomEnemies.Capacity; i++)
                //{
                //    if (this.m_BoundingBox.Intersects(a_RoomEnemies[i].m_BoundingBox) && a_RoomEnemies[i] != this)
                //    {
                //        touching = true;
                //    }
                //    else if (a_RoomEnemies[i] != this)
                //    {
                //        touching = false;
                //    }
                //}
                //if (!touching)
                //{
                //    this.SetRotation('W');
                //    m_Position.Y = m_Position.Y - m_Speed;
                //}
                this.SetRotation('W');
                m_Position.Y = m_Position.Y - m_Speed;
            }
        }
    }
}
