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
    public class HealthUp : Item
    {
        public HealthUp()
        {
            m_Used = false;
        }

        public void LoadContent(ContentManager a_Content)
        {
            if(m_Used == false)
            {
                m_Texture = a_Content.Load<Texture2D>("Items/HeartsUp");
                m_HitBox = new Rectangle((int)m_Position.X, (int)m_Position.Y, m_Texture.Width, m_Texture.Height);
            }
            else
            {
                m_Texture = null;
            }
            
        }

        public void IncreasePlayerHealth(Player a_MainPlayer)
        {
            if(m_Used == false)
            {
                a_MainPlayer.IncreaseHealth(1);
                SetUsed(true);
            }
        }

    }
}
