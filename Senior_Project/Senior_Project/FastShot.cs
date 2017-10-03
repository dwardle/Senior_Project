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

        /// <name>FaseShot::FastShot()</name>
        /// <summary>
        /// Basic constructor for a FastShot Item
        /// </summary>
        /// <author>Douglas Wardle</author>
        /// <date></date>
        public FastShot()
        {
            m_Used = false;
            m_ShotMultiplyer = 0.5f;
            //m_HitBox = new Rectangle(0, 0, 0, 0);
        }

        /// <name>FaseShot::LoadContent()</name>
        /// <summary>
        /// Loads the texture for a FastShot object and sets the hitbox for the item
        /// </summary>
        /// <param name="a_Content">content manager for all game content</param>
        /// <author>Douglas Wardle</author>
        /// <date></date>
        public void LoadContent(ContentManager a_Content)
        {
            if(m_Used == false)
            {
                m_Texture = a_Content.Load<Texture2D>("Items/FastShot");
                m_HitBox = new Rectangle((int)m_Position.X, (int)m_Position.Y, m_Texture.Width, m_Texture.Height);
            }
            else
            {
                m_Texture = null;
                m_HitBox = new Rectangle(0, 0, 0, 0);
            }
            
        }

        /// <name>FaseShot::IncreaseShotSpeed()</name>
        /// <summary>
        /// should be called if a player collects a FastShot item. will increase the players shot speed multiplier
        /// </summary>
        /// <param name="a_MainPlayer">Player that collected the item</param>
        /// <author>Douglas Wardle</author>
        /// <date></date>
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
