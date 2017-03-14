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
        public Texture2D m_Texture;
        public Vector2 m_PlayerPosition;
        public int m_PlayerSpeed;
        public float m_PlayerRotation;
        public Vector2 m_PlayerOrigin;
        //public float bulletDelay;
        public Rectangle m_BoundingBox;
        TopDownGame m_CurrentGame;
        public int m_CurrentRoom = 0;
        const int m_RoomWidth = 960;
        const int m_RoomHeight = 832;

        public Player(TopDownGame a_CurrentGame)
        {
            m_CurrentGame = a_CurrentGame;
            m_Texture = null;
            m_PlayerSpeed = 5;
            m_PlayerPosition = new Vector2(300, 300);
        }

        public void LoadContent(ContentManager a_Content)
        {
            m_Texture = a_Content.Load<Texture2D>("batDoug5");
        }

        public void Draw(SpriteBatch a_SpriteBatch)
        {
            m_PlayerOrigin.X = m_Texture.Width / 2;
            m_PlayerOrigin.Y = m_Texture.Height / 2;
            a_SpriteBatch.Draw(m_Texture, m_PlayerPosition, null, Color.White, m_PlayerRotation, m_PlayerOrigin, 1.0f, SpriteEffects.None, 0f);
            //a_SpriteBatch.Draw(m_Texture, m_PlayerPosition, Color.White);
        }

        public void Update(GameTime a_GameTime, TopDownGame a_Game)
        {
            KeyboardState keyState = Keyboard.GetState();
            if (keyState.IsKeyDown(Keys.W))
            {
                m_PlayerRotation = ((float)Math.PI / 2.0f) * 4;
                //angle = (float)Math.PI / 2.0f;  // 90 degrees
                //scale = 1.0f;
                //if (keyState.IsKeyDown(Keys.Up))
                //{
                //    m_PlayerRotation = ((float)Math.PI / 2.0f) * 3;
                //}
                //else if (keyState.IsKeyDown(Keys.Right))
                //{
                //    m_PlayerRotation = ((float)Math.PI / 2.0f) * 4;
                //}
                //else if (keyState.IsKeyDown(Keys.Left))
                //{
                //    m_PlayerRotation = ((float)Math.PI / 2.0f) * 2;
                //}
                //else if (keyState.IsKeyDown(Keys.Down))
                //{
                //    m_PlayerRotation = ((float)Math.PI / 2.0f);
                //}
                //else
                //{
                //    m_PlayerRotation = ((float)Math.PI / 2.0f) * 3;
                //}


                m_PlayerPosition.Y = m_PlayerPosition.Y - m_PlayerSpeed;
            }
            if (keyState.IsKeyDown(Keys.A))
            {

                m_PlayerRotation = ((float)Math.PI / 2.0f) * 3;

                //m_PlayerRotation = ((float)Math.PI / 2.0f) * 2;
                m_PlayerPosition.X = m_PlayerPosition.X - m_PlayerSpeed;
            }
            if (keyState.IsKeyDown(Keys.S))
            {

                m_PlayerRotation = ((float)Math.PI / 2.0f) * 2;


                m_PlayerPosition.Y = m_PlayerPosition.Y + m_PlayerSpeed;
            }
            if (keyState.IsKeyDown(Keys.D))
            {
                m_PlayerRotation = ((float)Math.PI / 2.0f);
                //m_PlayerRotation = ((float)Math.PI / 2.0f) * 4;
                m_PlayerPosition.X = m_PlayerPosition.X + m_PlayerSpeed;
            }

            if (keyState.IsKeyDown(Keys.Up))
            {
                m_PlayerRotation = ((float)Math.PI / 2.0f) * 4;
                //line below is for testing the switch from closed door to open door
                m_CurrentGame.m_Door.m_IsDoorOpen = true;
            }
            else if (keyState.IsKeyDown(Keys.Right))
            {
                m_PlayerRotation = ((float)Math.PI / 2.0f);
            }
            else if (keyState.IsKeyDown(Keys.Left))
            {
                m_PlayerRotation = ((float)Math.PI / 2.0f) * 3;
            }
            else if (keyState.IsKeyDown(Keys.Down))
            {
                m_PlayerRotation = ((float)Math.PI / 2.0f) * 2;
            }


            /*
            if(m_PlayerPosition.X <= 0)
            {
                m_PlayerPosition.X = 0;
            }
            if(m_PlayerPosition.X >= 800 - m_Texture.Width)
            {
                m_PlayerPosition.X = 800 - m_Texture.Width;
            }
            if(m_PlayerPosition.Y <= 0)
            {
                m_PlayerPosition.Y = 0;
            }
            if(m_PlayerPosition.Y >= 650 - m_Texture.Height)
            {
                m_PlayerPosition.Y = 650 - m_Texture.Height;
            }
            */
            //960 x 832
            //room size

            if (m_PlayerPosition.X <= (64 + m_CurrentGame.m_RoomList[m_CurrentRoom].m_RoomPosition.X) + m_Texture.Width / 2)
            {
                m_PlayerPosition.X = (64 + m_CurrentGame.m_RoomList[m_CurrentRoom].m_RoomPosition.X) + m_Texture.Width / 2;
            }
            if (m_PlayerPosition.X >= (960 - (64) + m_CurrentGame.m_RoomList[m_CurrentRoom].m_RoomPosition.X) - m_Texture.Width / 2)
            {
                m_PlayerPosition.X = (960 - (64) + m_CurrentGame.m_RoomList[m_CurrentRoom].m_RoomPosition.X) - m_Texture.Width / 2;
            }
            if (m_PlayerPosition.Y <= (64 + m_CurrentGame.m_RoomList[m_CurrentRoom].m_RoomPosition.Y) + m_Texture.Height / 2)
            {
                //boundaries for top door if its open
                if (a_Game.m_Door.m_IsDoorOpen == true && m_PlayerPosition.X >= 448 && m_PlayerPosition.X <= 512)
                {
                    //m_CurrentRoom++;
                    if (m_PlayerPosition.X >= 448 && m_PlayerPosition.Y < 64)
                    {
                        m_PlayerPosition.X = 448 + m_Texture.Width / 2;
                    }
                }
                else
                {
                    m_PlayerPosition.Y = (64 + m_CurrentGame.m_RoomList[m_CurrentRoom].m_RoomPosition.Y) + m_Texture.Height / 2;
                }
                //m_PlayerPosition.Y = (64) + m_Texture.Height / 2;
            }
            if (m_PlayerPosition.Y >= (832 - (64) + m_CurrentGame.m_RoomList[m_CurrentRoom].m_RoomPosition.Y) - m_Texture.Height / 2)
            {
                m_PlayerPosition.Y = (832 - (64) + m_CurrentGame.m_RoomList[m_CurrentRoom].m_RoomPosition.Y) - m_Texture.Height / 2;
            }

            m_BoundingBox = new Rectangle((int)m_PlayerPosition.X, (int)m_PlayerPosition.Y, m_Texture.Width, m_Texture.Height);
        }

        //new update for when using a Level object. not implimented yet
        public void Update(GameTime a_GameTime, Level a_CurrentLevel)
        {
            KeyboardState keyState = Keyboard.GetState();
            if (keyState.IsKeyDown(Keys.W))
            {
                m_PlayerRotation = ((float)Math.PI / 2.0f) * 4;
                m_PlayerPosition.Y = m_PlayerPosition.Y - m_PlayerSpeed;
            }
            if (keyState.IsKeyDown(Keys.A))
            {
                m_PlayerRotation = ((float)Math.PI / 2.0f) * 3;
                m_PlayerPosition.X = m_PlayerPosition.X - m_PlayerSpeed;
            }
            if (keyState.IsKeyDown(Keys.S))
            {
                m_PlayerRotation = ((float)Math.PI / 2.0f) * 2;
                m_PlayerPosition.Y = m_PlayerPosition.Y + m_PlayerSpeed;
            }
            if (keyState.IsKeyDown(Keys.D))
            {
                m_PlayerRotation = ((float)Math.PI / 2.0f);
                m_PlayerPosition.X = m_PlayerPosition.X + m_PlayerSpeed;
            }

            if (keyState.IsKeyDown(Keys.Up))
            {
                m_PlayerRotation = ((float)Math.PI / 2.0f) * 4;
                //line below is for testing the switch from closed door to open door
                m_CurrentGame.m_Door.m_IsDoorOpen = true;
            }
            else if (keyState.IsKeyDown(Keys.Right))
            {
                m_PlayerRotation = ((float)Math.PI / 2.0f);
            }
            else if (keyState.IsKeyDown(Keys.Left))
            {
                m_PlayerRotation = ((float)Math.PI / 2.0f) * 3;
            }
            else if (keyState.IsKeyDown(Keys.Down))
            {
                m_PlayerRotation = ((float)Math.PI / 2.0f) * 2;
            }

            List<Rooms> CurrentRoomList = new List<Rooms>();
            CurrentRoomList = a_CurrentLevel.GetRoomList();
            Rooms CurrentRoom = new Rooms();
            CurrentRoom = CurrentRoomList[m_CurrentRoom];
            int cRoom_X = (int)CurrentRoom.m_RoomPosition.X;
            int cRoom_Y = (int)CurrentRoom.m_RoomPosition.Y;

            if (m_PlayerPosition.X <= (64 + cRoom_X) + m_Texture.Width / 2)
            {
                m_PlayerPosition.X = (64 + cRoom_X) + m_Texture.Width / 2;
            }
            if (m_PlayerPosition.X >= (896 + cRoom_X) + m_Texture.Width / 2)
            {
                m_PlayerPosition.X = (896 + cRoom_X) + m_Texture.Width / 2;
            }
            if (m_PlayerPosition.Y <= (64 + cRoom_Y) + m_Texture.Height / 2)
            {
                if (CurrentRoom.m_RoomDoors[0].m_IsDoorOpen && m_PlayerPosition.X >= cRoom_X + 448 && m_PlayerPosition.X <= cRoom_X + 512)
                {
                    if (m_PlayerPosition.Y >= 64)
                    {
                        m_PlayerPosition.X = (cRoom_X + 448) + (m_Texture.Width / 2);
                    }
                }
                else
                {
                    m_PlayerPosition.X = (64 + cRoom_Y) + m_Texture.Height / 2;
                }
            }
            if (m_PlayerPosition.Y >= (768 + cRoom_Y) + m_Texture.Height / 2)
            {
                m_PlayerPosition.X = (768 + cRoom_Y) + m_Texture.Height / 2;
            }

            //Boundaries 
            //if (m_PlayerPosition.X <= (64 + m_CurrentGame.m_RoomList[m_CurrentRoom].m_RoomPosition.X) + m_Texture.Width / 2)
            //{
            //    m_PlayerPosition.X = (64 + m_CurrentGame.m_RoomList[m_CurrentRoom].m_RoomPosition.X) + m_Texture.Width / 2;
            //}
            //if (m_PlayerPosition.X >= (960 - (64) + m_CurrentGame.m_RoomList[m_CurrentRoom].m_RoomPosition.X) - m_Texture.Width / 2)
            //{
            //    m_PlayerPosition.X = (960 - (64) + m_CurrentGame.m_RoomList[m_CurrentRoom].m_RoomPosition.X) - m_Texture.Width / 2;
            //}

            //if (m_PlayerPosition.Y <= (64 + m_CurrentGame.m_RoomList[m_CurrentRoom].m_RoomPosition.Y) + m_Texture.Height / 2)
            //{
            //    //boundaries for top door if its open
            //    if (a_Game.m_Door.m_IsDoorOpen == true && m_PlayerPosition.X >= 448 && m_PlayerPosition.X <= 512)
            //    {
            //        //m_CurrentRoom++;
            //        if (m_PlayerPosition.X >= 448 && m_PlayerPosition.Y < 64)
            //        {
            //            m_PlayerPosition.X = 448 + m_Texture.Width / 2;
            //        }
            //    }
            //    else
            //    {
            //        m_PlayerPosition.Y = (64 + m_CurrentGame.m_RoomList[m_CurrentRoom].m_RoomPosition.Y) + m_Texture.Height / 2;
            //    }
            //    //m_PlayerPosition.Y = (64) + m_Texture.Height / 2;
            //}
            //if (m_PlayerPosition.Y >= (832 - (64) + m_CurrentGame.m_RoomList[m_CurrentRoom].m_RoomPosition.Y) - m_Texture.Height / 2)
            //{
            //    m_PlayerPosition.Y = (832 - (64) + m_CurrentGame.m_RoomList[m_CurrentRoom].m_RoomPosition.Y) - m_Texture.Height / 2;
            //}

            //m_BoundingBox = new Rectangle((int)m_PlayerPosition.X, (int)m_PlayerPosition.Y, m_Texture.Width, m_Texture.Height);
        }
    }
}