using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace Senior_Project
{
    public class Boss : Enemy
    {
        public Vector2 m_MoveTo;
        public bool m_CanMove;
        public bool m_IsMoving;
        

        public Boss()
        {
            m_CanMove = false;
            m_IsMoving = false;
            m_MoveDelay = 250;
            m_MoveTo = new Vector2(0, 0);
            m_EnemyOrigin.X = 96;
            m_EnemyOrigin.Y = 96;
            m_Speed = 5;
        }

        public void LoadContent(ContentManager a_Content)
        {
            m_Texture = a_Content.Load<Texture2D>("Enemies/Boss1-2");
            m_EnemyOrigin.X = (m_Texture.Width / 2);
            m_EnemyOrigin.Y = (m_Texture.Height / 2);
        }


        public void Draw2(SpriteBatch a_SpriteBatch)
        {
            a_SpriteBatch.Draw(m_Texture, m_Position, Color.White);
        }

        public void MoveToPlayer(Player a_MainPlayer)
        {
            //follow the player
            float moveToX = a_MainPlayer.m_PlayerPosition.X; //+ (a_MainPlayer.m_Texture.Width/2);  //(a_MainPlayer.m_Texture.Width);
            float moveToY = a_MainPlayer.m_PlayerPosition.Y; //+ (a_MainPlayer.m_Texture.Height / 2);  //(a_MainPlayer.m_Texture.Height);

            if (m_Position.X < moveToX)
            {
                m_Position.X = m_Position.X + m_Speed;

            }
            if(m_Position.Y < moveToY)
            {
                m_Position.Y = m_Position.Y + m_Speed;
            }
            if (m_Position.X > moveToX)
            {
                m_Position.X = m_Position.X - m_Speed;

            }
            if (m_Position.Y > moveToY)
            {
                m_Position.Y = m_Position.Y - m_Speed;
            }

        }

        public void MoveToPlayer()
        {
            //boss movement if i want the boss to move toward the players last position then stop and wait
            if (m_Position.X < m_MoveTo.X)
            {
                m_Position.X = m_Position.X + m_Speed;
                if (m_Position.Y < m_MoveTo.Y)
                {
                    //m_Position.X = m_Position.X + m_Speed;
                    m_Position.Y = m_Position.Y + m_Speed;
                }
                if (m_Position.Y > m_MoveTo.Y)
                {
                    //m_Position.X = m_Position.X + m_Speed;
                    m_Position.Y = m_Position.Y - m_Speed;
                }
                if (m_Position.X >= m_MoveTo.X)
                {
                    SetIsMoving(false);
                    SetCanMove(false);
                }
            }
            if (m_Position.X > m_MoveTo.X)
            {
                m_Position.X = m_Position.X - m_Speed;
                if (m_Position.Y < m_MoveTo.Y)
                {
                    //m_Position.X = m_Position.X - m_Speed;
                    m_Position.Y = m_Position.Y + m_Speed;
                }
                if (m_Position.Y > m_MoveTo.Y)
                {
                   // m_Position.X = m_Position.X - m_Speed;
                    m_Position.Y = m_Position.Y - m_Speed;
                }
                

                if (m_Position.X <= m_MoveTo.X)
                {
                    SetIsMoving(false);
                    SetCanMove(false);
                }
                //m_Position.X = m_Position.X - m_Speed;
                //if (m_Position.X < m_MoveTo.X)
                //{
                //    SetIsMoving(false);
                //}
                //else
                //{
                //    SetIsMoving(true);
                //}
            }
            //if (m_Position.Y <= m_MoveTo.Y)
            //{
            //    m_Position.Y = m_Position.Y + m_Speed;
            //    //if (m_Position.Y > m_MoveTo.Y)
            //    //{
            //    //    SetIsMoving(false);
            //    //}
            //    //else
            //    //{
            //    //    SetIsMoving(true);
            //    //}
            //}
            
            //if (m_Position.Y >= m_MoveTo.Y)
            //{
            //    m_Position.Y = m_Position.Y - m_Speed;
            //    //if (m_Position.Y < m_MoveTo.Y)
            //    //{
            //    //    SetIsMoving(false);
            //    //}
            //    //else
            //    //{
            //    //    SetIsMoving(true);
            //    //}
            //}
        }

        public Boss GetBoss()
        {
            return this;
        }

        public void SetCanMove()
        {

            m_CanMove = !m_CanMove;
        }
        public void SetCanMove(bool a_CanMove)
        {
            m_CanMove = a_CanMove;
        }

        //need to fix the movement on this function so when player enters the room there is a delay before the boss moves
        public bool CanMove()
        {
            //if(m_CanMove && m_MoveDelay <= 0)
            //{
            //    return m_CanMove;
            //}
            if(m_CanMove == false && m_IsMoving == false && m_MoveDelay <= 0)
            {
                m_MoveDelay = 100;
                SetCanMove();
                //SetIsMoving(true);
            }
            else if(m_CanMove == false && m_MoveDelay >= 0)
            {
                m_MoveDelay--;
            }
            //else if(m_IsMoving == false && m_CanMove == true && m_MoveDelay >= 0)
            //{
            //    SetCanMove(false);
            //}
            //else if(m_IsMoving == true && m_Position.X <= m_MoveTo.X + 1 && m_Position.Y >= m_MoveTo.Y + 1)
            //{
            //    SetIsMoving(false);
            //    SetCanMove();
            //}
            //else if (m_IsMoving == true && m_Position.X <= m_MoveTo.X - 1 && m_Position.Y >= m_MoveTo.Y - 1)
            //{
            //    SetIsMoving(false);
            //    SetCanMove();
            //}
            //else if (m_CanMove == true && m_MoveDelay >= 0)
            //{

            //}
            return m_CanMove;
        }

        public void SetMoveLocation(Vector2 a_MoveTo)
        {
            m_MoveTo = a_MoveTo;
        }

        public float GetTextureOriginX()
        {
            return m_EnemyOrigin.X;
        }

        public float GetTextureOriginY()
        {
            return m_EnemyOrigin.Y;
        }

        public void SetIsMoving(bool a_IsMoving)
        {
            m_IsMoving = a_IsMoving;
        }

        public bool IsMoving()
        {
            return m_IsMoving;
        }
    }
}
