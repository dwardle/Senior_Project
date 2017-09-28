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

        public Boss()
        {

        }

        public void LoadContent(ContentManager a_Content)
        {
            m_Texture = a_Content.Load<Texture2D>("Enemies/Boss1-2");
        }


        public void Draw2(SpriteBatch a_SpriteBatch)
        {
            a_SpriteBatch.Draw(m_Texture, m_Position, Color.White);
        }

        public void MoveToPlayer(Player a_MainPlayer)
        {
            //attempting to move the boss toward the player until it hits the players last location
            float moveToX = a_MainPlayer.m_PlayerPosition.X;
            float moveToY = a_MainPlayer.m_PlayerPosition.Y;

            if ((m_Position.X + (m_Texture.Width / 2)) <= (moveToX + (a_MainPlayer.m_PlayerOrigin.X)))
            {
                m_Position.X = m_Position.X + m_Speed;

            }
            if(m_Position.Y + (m_Texture.Height / 2) <= moveToY + (a_MainPlayer.m_PlayerOrigin.Y))
            {
                m_Position.Y = m_Position.Y + m_Speed;
            }

        }

        public Boss GetBoss()
        {
            return this;
        }
    }
}
