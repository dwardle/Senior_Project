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
        /// <name>HealthUp::HealthUp()</name>
        /// <summary>
        /// Basic constructor for a HealthUp Object
        /// </summary>
        /// <author>Douglas Wardle</author>
        /// <date>10/4/2017</date>
        public HealthUp()
        {
            m_Used = false;
        }

        /// <name>HealthUp::LoadContent()</name>
        /// <summary>
        /// Loads the tecxture for a HealthUp object and sets its hitbox
        /// </summary>
        /// <param name="a_Content">content manager for all game content</param>
        /// <author>Douglas Wardle</author>
        /// <date>10/4/2017</date>
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

        /// <name>HealthUp::IncreasePlayerHealth()</name>
        /// <summary>
        /// Function gets called when a player collects the item. it will increase the players health by 1 and
        /// set the m_Used value to true
        /// </summary>
        /// <param name="a_MainPlayer">The player that has collected the HealthUp item</param>
        /// <author>Douglas Wardle</author>
        /// <date>10/4/2017</date>
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
