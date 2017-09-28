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
    public class FastShot : Item
    {
        public float m_ShotMultiplyer;
        
        public FastShot()
        {
            m_Used = false;
            m_ShotMultiplyer = 0.5f;
            //m_HitBox = new Rectangle(0, 0, 0, 0);
        }

        public void LoadContent(ContentManager a_Content)
        {
            m_Texture = a_Content.Load<Texture2D>("Items/FastShot");
            m_HitBox = new Rectangle((int)m_Position.X, (int)m_Position.Y, m_Texture.Width, m_Texture.Height);
        }

        public void IncreaseShotSpeed(Player a_MainPlayer)
        {
            if(m_Used == false)
            {
                a_MainPlayer.IncreaseShotSpeed(m_ShotMultiplyer);
                SetUsed(true);
            }
        }

    }
}
