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
    public class Rooms
    {
        public Texture2D m_Texture;
        public Vector2 m_RoomPosition;
        //public Rectangle m_BoundingBox;

        public Rooms()
        {
            m_Texture = null;
            m_RoomPosition = new Vector2(0, 0);
            //m_BoundingBox = new Rectangle((int)m_RoomPosition.X, (int)m_RoomPosition.Y, 64, 64);
        }

        public Rooms(int a_RoomX, int a_RoomY)
        {
            m_RoomPosition = new Vector2(a_RoomX, a_RoomY);
        }

        public void LoadContent(ContentManager a_Content)
        {
            m_Texture = a_Content.Load<Texture2D>("test4");
        }

        public void Draw(SpriteBatch a_SpriteBatch)
        {
            a_SpriteBatch.Draw(m_Texture, m_RoomPosition, Color.White);
        }
    }
}
