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
    public class Bullet
    {

        //bullet size 6 x 8
        public Texture2D m_Texture;
        public Vector2 m_Position;
        public bool m_IsVisible;
        public float m_Speed;
        public float m_BulletRotaion = 0;
        public Vector2 m_BulletOrigin;
        public Keys m_BulletDirection;
        public Rectangle m_HitBox;

        /// <name>Bullet::Bullet()</name>
        /// <summary>
        /// Basic constructor for a Bullet object
        /// </summary>
        /// <param name="a_Texture">Texture for the bullet object</param>
        /// <author>Douglas Wardle</author>
        /// <date>10/4/2017</date>
        public Bullet(Texture2D a_Texture)
        {
            m_Speed = 10;
            m_Texture = a_Texture;
            m_IsVisible = false;
        }

        /// <name>Bullet::Draw()</name>
        /// <summary>
        /// Function for drawing bullets. is called whenever bullets need to be drawn on screen
        /// </summary>
        /// <param name="a_SpriteBatch">SpriteBatch object to allow drawing of sprites</param>
        /// <author>Douglas Wardle</author>
        /// <date>10/4/2017</date>
        public void Draw(SpriteBatch a_SpriteBatch)
        {
            m_BulletOrigin = new Vector2(m_Texture.Width / 2, m_Texture.Height / 2);
            a_SpriteBatch.Draw(m_Texture, m_Position, null, Color.White, m_BulletRotaion, m_BulletOrigin, 1.0f, SpriteEffects.None, 0f);
        }

        /// <name>Bullet::GetSpeed()</name>
        /// <summary>
        /// Accesses the speed value for the bullet object. this value is how fast a bullet travels normally
        /// </summary>
        /// <returns>floating point value for the bullets speed</returns>
        /// <author>Douglas Wardle</author>
        /// <date>10/4/2017</date>
        public float GetSpeed()
        {
            return m_Speed;
        }
    }
}
