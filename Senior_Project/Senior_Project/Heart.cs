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
    public class Heart
    {
        public Texture2D m_Texture;
        public Vector2 m_Position;
        public int m_Texture_Type;

        public Heart()
        {
            m_Texture_Type = 0;
            m_Position = new Vector2(40, 16);
        }

        public Heart(Vector2 a_Position)
        {
            m_Position = a_Position;
        }

        public Heart(float a_Position_X, float a_Position_Y)
        {
            m_Position.X = a_Position_X;
            m_Position.Y = a_Position_Y;
        }

        public void LoadContent(ContentManager a_Content)
        {
            SetTexture(a_Content);
        }

        public void Draw(SpriteBatch a_SpriteBatch)
        {
            a_SpriteBatch.Draw(m_Texture, m_Position, Color.White);
        }

        public void SetPosition(Vector2 a_Position)
        {
            m_Position = a_Position;
        }

        public void SetPosition(float a_Position_X, float a_Position_Y)
        {
            m_Position.X = a_Position_X;
            m_Position.Y = a_Position_Y;
        }

        public void SetTexture(ContentManager a_Content)
        {
            switch(m_Texture_Type)
            {
                case 0:
                    m_Texture = a_Content.Load<Texture2D>("Hearts/FullHeart");
                    break;
                case 1:
                    m_Texture = a_Content.Load<Texture2D>("Hearts/HalfHeart");
                    break;
                case 2:
                    m_Texture = a_Content.Load<Texture2D>("Hearts/EmptyHeart");
                    break;
                default:
                    break;
            }
        }

       /* public void ChangeTexture(float a_MaxHealth, float a_PlayerHealth)
        {
            for(int i = 0; i < a_MaxHealth; i++)
            {
                if(a_MaxHealth - a_PlayerHealth == 0)
                {
                    SetTextureType(0);
                }
            }

        }*/

        public void SetTextureType(int a_Texture_Type)
        {
            m_Texture_Type = a_Texture_Type;
        }
    }
}
