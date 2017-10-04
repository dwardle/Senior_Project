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

        /// <name>RoomFloor::RoomFloor()</name>
        /// <summary>
        /// basic constructor for a room floor object
        /// </summary>
        /// <author>Douglas Wardle</author>
        /// <date>10/4/2017</date>
        public RoomFloor()
        {
            m_Texture = null;
            m_FloorPosition = new Vector2(64, 64);
        }

        /// <name>RoomFloor::LoadContent</name>
        /// <summary>
        /// Loads the texture for a room floor
        /// </summary>
        /// <param name="a_Content">content manager that contains the textures for the game</param>
        /// <author>Douglas Wardle</author>
        /// <date>10/4/2017</date>
        public void LoadContent(ContentManager a_Content)
        {
            m_Texture = a_Content.Load<Texture2D>("Rooms/roomFloor");
        }

        /// <name>RoomFloor::Draw()</name>
        /// <summary>
        /// called whenever a RoomFloor needs to be drawn
        /// </summary>
        /// <param name="a_SpriteBatch">SpriteBatch object to allow for drawing of sprites</param>
        /// <author>Douglas Wardle</author>
        /// <date>10/4/2017</date>
        public void Draw(SpriteBatch a_SpriteBatch)
        {
            a_SpriteBatch.Draw(m_Texture, m_FloorPosition, Color.White);
        }
    }
}