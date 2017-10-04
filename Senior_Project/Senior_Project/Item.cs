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

        /// <name>Item::Item()</name>
        /// <summary>
        /// Basic contructor for an Item object
        /// </summary>
        /// <author>Douglas Wardle</author>
        /// <date>10/4/2017</date>
        public Item()
        {
            m_Texture = null;
            m_Position = new Vector2(0, 0);
            m_IsVisible = false;
            m_HitBox = new Rectangle(0, 0, 0, 0);
        }

        /// <name>Item::Draw()</name>
        /// <summary>
        /// Draws the item to the screen as long as its texture is not null
        /// </summary>
        /// <param name="a_SpriteBatch">SpriteBatch object to allow for drawing to the screen</param>
        /// <author>Douglas Wardle</author>
        /// <date>10/4/2017</date>
        public void Draw(SpriteBatch a_SpriteBatch)
        {
            if(m_Texture != null)
            {
                a_SpriteBatch.Draw(m_Texture, m_Position, Color.White);
            }       
        }

        /// <name>Item::SetUsed()</name>
        /// <summary>
        /// Sets the m_Used value to the bool value a_Used. 
        /// </summary>
        /// <param name="a_Used">bool value, Should be true if the item has been used and false otherwise</param>
        /// <author>Douglas Wardle</author>
        /// <date>10/4/2017</date>
        public void SetUsed(bool a_Used)
        {
            m_Used = a_Used;
        }

        /// <name>Item::SetPosition()</name>
        /// <summary>
        /// Accepts a Vector2 and sets the items position to that Vector2
        /// </summary>
        /// <param name="a_Position">Vector2 containing the new position for the item</param>
        /// <author>Douglas Wardle</author>
        /// <date>10/4/2017</date>
        public void SetPosition(Vector2 a_Position)
        {
            m_Position = a_Position;
        }

        /// <name>Item::SetPosition()</name>
        /// <summary>
        /// Accepts two floating point values to set as the items new position
        /// </summary>
        /// <param name="a_Position_X">new position X</param>
        /// <param name="a_Position_Y">new position Y</param>
        /// <author>Douglas Wardle</author>
        /// <date>10/4/2017</date>
        public void SetPosition(float a_Position_X, float a_Position_Y)
        {
            m_Position = new Vector2(a_Position_X, a_Position_Y);
        }

        /// <name>Item::GetUsed()</name>
        /// <summary>
        /// Accesses the items is m_Used value
        /// </summary>
        /// <returns>True if the item has been used, false otherwise</returns>
        /// <author>Douglas Wardle</author>
        /// <date>10/4/2017</date>
        public bool GetUsed()
        {
            return m_Used;
        }

        /// <name>Item::GetPosition()</name>
        /// <summary>
        /// Accesses the items m_Position
        /// </summary>
        /// <returns>Vector2 containing the items position</returns>
        /// <author>Douglas Wardle</author>
        /// <date>10/4/2017</date>
        public Vector2 GetPosition()
        {
            return m_Position;
        }
    }
}
