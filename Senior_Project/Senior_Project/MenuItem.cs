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
    public class MenuItem
    {
        public Texture2D m_Texture;
        public Vector2 m_Position;
        public Rectangle m_Hitbox = new Rectangle();
        public int m_Option;

        public MenuItem()
        {
            m_Position.X = 350;
            m_Position.Y = 300;
            m_Option = 0;
            
        }

        public MenuItem(int a_Option)
        {
            m_Position.X = 350;
            m_Position.Y = 300;
            m_Option = a_Option;

        }

        public void LoadContent(ContentManager a_Content)
        {
            SetTexture(a_Content);
            //m_Texture = a_Content.Load<Texture2D>("Start");
            m_Hitbox = new Rectangle((int)m_Position.X, (int)m_Position.Y, m_Texture.Width, m_Texture.Height);
        }

        public void Draw(SpriteBatch a_SpriteBatch)
        {
            //a_SpriteBatch.Begin();
            a_SpriteBatch.Draw(m_Texture, m_Position, Color.White);
            //a_SpriteBatch.End();
        }

        public void SetTexture(ContentManager a_Content)
        {
            if(m_Option == 0)
            {
                m_Texture = a_Content.Load<Texture2D>("Menus/Start");
            }
            else if(m_Option == 1)
            {
                m_Texture = a_Content.Load<Texture2D>("Menus/Exit");
            }
        }

        public void SetPosition(Vector2 a_Position)
        {
            m_Position = a_Position;
            //m_Hitbox = new Rectangle((int)m_Position.X, (int)m_Position.Y, m_Texture.Width, m_Texture.Height);
        }

        public void SetPosition(int a_Position_X, int a_Position_Y)
        {
            m_Position.X = a_Position_X;
            m_Position.Y = a_Position_Y;
            //m_Hitbox = new Rectangle((int)m_Position.X, (int)m_Position.Y, m_Texture.Width, m_Texture.Height);
        }
    }


}
