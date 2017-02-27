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
    public class Door
    {
        public Texture2D m_Texture;
        public Vector2 m_DoorPosition;
        public Rectangle m_BoundingBox;
        public bool m_IsDoorOpen = true;

        public Door()
        {
            m_Texture = null;
            m_DoorPosition = new Vector2(448, 0);
            m_BoundingBox = new Rectangle((int)m_DoorPosition.X, (int)m_DoorPosition.Y, 64, 64);
        }

        public void LoadContent(ContentManager a_Content)
        {
            m_Texture = a_Content.Load<Texture2D>("door2");
        }

        public void Draw(SpriteBatch a_SpriteBatch)
        {
            a_SpriteBatch.Draw(m_Texture, m_DoorPosition, Color.White);
        }
    }
}