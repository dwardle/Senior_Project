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

        /// <name>Heart::Heart()</name>
        /// <summary>
        /// Basic Constructor for Heart object
        /// </summary>
        /// <author>Douglas Wardle</author>
        /// <date>10/4/2017</date>
        public Heart()
        {
            m_Texture_Type = 0;
            m_Position = new Vector2(40, 16);
        }

        /// <name>Heart::Heart()</name>
        /// <summary>
        /// Constuctor to create Heart object with a specified position as a Vector2
        /// </summary>
        /// <param name="a_Position">Position of the object</param>
        /// <author>Douglas Wardle</author>
        /// <date>10/4/2017</date>
        public Heart(Vector2 a_Position)
        {
            m_Position = a_Position;
        }

        /// <name>Heart::Heart()</name>
        /// <summary>
        /// Constructor to create Heart object with specified position as 2 floating point values
        /// </summary>
        /// <param name="a_Position_X">X position of the object</param>
        /// <param name="a_Position_Y">Y position of the object</param>
        /// <author>Douglas Wardle</author>
        /// <date>10/4/2017</date>
        public Heart(float a_Position_X, float a_Position_Y)
        {
            m_Position.X = a_Position_X;
            m_Position.Y = a_Position_Y;
        }

        /// <name>Heart::LoadContent()</name>
        /// <summary>
        /// Function should be called whenever a Heart object needs to load its texture
        /// </summary>
        /// <param name="a_Content">content manager containing all conetent for the game</param>
        /// <author>Douglas Wardle</author>
        /// <date>10/4/2017</date>
        public void LoadContent(ContentManager a_Content)
        {
            SetTexture(a_Content);
        }

        /// <name>Heart::Draw()</name>
        /// <summary>
        /// Function should be called whenever a Heart object needs to be drawn to the screen
        /// </summary>
        /// <param name="a_SpriteBatch">SpriteBatch object to allow sprites to be drawn</param>
        /// <author>Douglas Wardle</author>
        /// <date>10/4/2017</date>
        public void Draw(SpriteBatch a_SpriteBatch)
        {
            a_SpriteBatch.Draw(m_Texture, m_Position, Color.White);
        }

        /// <name>Heart::SetPosition()</name>
        /// <summary>
        /// Accepts a Vector2 and sets the position of the object to the Vector2 passed to the function
        /// </summary>
        /// <param name="a_Position">Vector2 containing the new position for the object</param>
        /// <author>Douglas Wardle</author>
        /// <date>10/4/2017</date>
        public void SetPosition(Vector2 a_Position)
        {
            m_Position = a_Position;
        }

        /// <name>Heart::SetPosition()</name>
        /// <summary>
        /// Accepts 2 floating point values which represent the X and Y coordinates the objects position will be set to
        /// </summary>
        /// <param name="a_Position_X">X position coordinates</param>
        /// <param name="a_Position_Y">Y position coordinates</param>
        /// <author>Douglas Wardle</author>
        /// <date>10/4/2017</date>
        public void SetPosition(float a_Position_X, float a_Position_Y)
        {
            m_Position.X = a_Position_X;
            m_Position.Y = a_Position_Y;
        }

        /// <name>Heart::SetTexture()</name>
        /// <summary>
        /// Sets the texture of the Heart object based off of its m_Texture_Type variable
        /// </summary>
        /// <param name="a_Content">content manager that contains all content for the game</param>
        /// <author>Douglas Wardle</author>
        /// <date>10/4/2017</date>
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
        /// <name>Heart::SetTextureType()</name>
        /// <summary>
        /// Accepts an integer value that represents the texture type and sets the objects texture type to it
        /// </summary>
        /// <param name="a_Texture_Type">integer represting the desired texture type</param>
        /// <author>Douglas Wardle</author>
        /// <date>10/4/2017</date>
        public void SetTextureType(int a_Texture_Type)
        {
            m_Texture_Type = a_Texture_Type;
        }
    }
}
