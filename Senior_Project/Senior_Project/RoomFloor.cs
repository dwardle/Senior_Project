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
    public class RoomFloor
    {
        public Texture2D m_Texture;

        public Vector2 m_FloorPosition;

        public RoomFloor()
        {
            m_Texture = null;
            m_FloorPosition = new Vector2(64, 64);
        }

        public void LoadContent(ContentManager a_Content)
        {
            m_Texture = a_Content.Load<Texture2D>("Rooms/roomFloor");
        }

        public void Draw(SpriteBatch a_SpriteBatch)
        {
            a_SpriteBatch.Draw(m_Texture, m_FloorPosition, Color.White);
        }
    }
}