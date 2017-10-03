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
    public class Item
    {
        public Texture2D m_Texture;
        public Vector2 m_Position;
        public bool m_IsVisible;
        public Rectangle m_HitBox;
        public bool m_Used;

        public Item()
        {
            m_Texture = null;
            m_Position = new Vector2(0, 0);
            m_IsVisible = false;
            m_HitBox = new Rectangle(0, 0, 0, 0);
        }

        public void Draw(SpriteBatch a_SpriteBatch)
        {
            if(m_Texture != null)
            {
                a_SpriteBatch.Draw(m_Texture, m_Position, Color.White);
            }       
        }


        public void SetUsed(bool a_Used)
        {
            m_Used = a_Used;
        }

        public void SetPosition(Vector2 a_Position)
        {
            m_Position = a_Position;
        }

        public void SetPosition(float a_Position_X, float a_Position_Y)
        {
            m_Position = new Vector2(a_Position_X, a_Position_Y);
        }


        public bool GetUsed()
        {
            return m_Used;
        }

        public Vector2 GetPosition()
        {
            return m_Position;
        }
    }
}
