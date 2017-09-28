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
        //public Rectangle boundingBox;
        //public Texture2D texture;
        //public Vector2 origin;
        //public Vector2 position;
        //public bool isVisible;
        //public float speed;


        //bullet size 6 x 8
        public Texture2D m_Texture;
        public Vector2 m_Position;
        public bool m_IsVisible;
        public float m_Speed;
        public float m_BulletRotaion = 0;
        public Vector2 m_BulletOrigin;
        public Keys m_BulletDirection;
        public Rectangle m_HitBox;

        public Bullet(Texture2D a_Texture)
        {
            //temporary bullet speed for testing
            //m_Speed = 2;
            m_Speed = 10;
            m_Texture = a_Texture;
            m_IsVisible = false;
        }

        public void Draw(SpriteBatch a_SpriteBatch)
        {
            m_BulletOrigin = new Vector2(m_Texture.Width / 2, m_Texture.Height / 2);
            a_SpriteBatch.Draw(m_Texture, m_Position, null, Color.White, m_BulletRotaion, m_BulletOrigin, 1.0f, SpriteEffects.None, 0f);
        }

        public float GetSpeed()
        {
            return m_Speed;
        }
    }
}
