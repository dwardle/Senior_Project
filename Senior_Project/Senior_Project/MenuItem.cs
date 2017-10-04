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

        /// <name>MenuItem::MenuItem()</name>
        /// <summary>
        /// Basic contructor for a MenuItem Object
        /// </summary>
        /// <author>Douglas Wardle</author>
        /// <date>10/4/2017</date>
        public MenuItem()
        {
            m_Position.X = 350;
            m_Position.Y = 300;
            m_Option = 0;
            
        }

        /// <name>MenuItem::MenuItem()</name>
        /// <summary>
        /// Constructor that will set the type of menu item that the object is
        /// </summary>
        /// <param name="a_Option">type of menu item</param>
        /// <author>Douglas Wardle</author>
        /// <date>10/4/2017</date>
        public MenuItem(int a_Option)
        {
            m_Position.X = 350;
            m_Position.Y = 300;
            m_Option = a_Option;

        }

        /// <name>MenuItem::LoadContent()</name>
        /// <summary>
        /// Function is called to set the texture of the menu item and its hitbox. function will call set texture to set the
        /// correct texture to the menu item
        /// </summary>
        /// <param name="a_Content">Content manager with all the content for the game</param>
        /// <author>Douglas Wardle</author>
        /// <date>10/4/2017</date>
        public void LoadContent(ContentManager a_Content)
        {
            SetTexture(a_Content);
            m_Hitbox = new Rectangle((int)m_Position.X, (int)m_Position.Y, m_Texture.Width, m_Texture.Height);
        }

        /// <name>MenuItem::Draw()</name>
        /// <summary>
        /// Function draws the object to the screen
        /// </summary>
        /// <param name="a_SpriteBatch">SpriteBatch object to allow drawing of sprites</param>
        /// <author>Douglas Wardle</author>
        /// <date>10/4/2017</date>
        public void Draw(SpriteBatch a_SpriteBatch)
        {

            a_SpriteBatch.Draw(m_Texture, m_Position, Color.White);

        }

        /// <name>MenuItem::SetTexture()</name>
        /// <summary>
        /// Function sets the menu item texture based off m_Option
        /// </summary>
        /// <param name="a_Content">content manager that contains all content for the game</param>
        /// <author>Douglas Wardle</author>
        /// <date>10/4/2017</date>
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
            else if(m_Option == 2)
            {
                m_Texture = a_Content.Load<Texture2D>("Menus/size5");
            }
            else if (m_Option == 3)
            {
                m_Texture = a_Content.Load<Texture2D>("Menus/size7");
            }
            else if (m_Option == 4)
            {
                m_Texture = a_Content.Load<Texture2D>("Menus/size9");
            }
            else if(m_Option == 5)
            {
                m_Texture = a_Content.Load<Texture2D>("Menus/NewGame");
            }
        }

        /// <name>MenuItem::SetPosition()</name>
        /// <summary>
        /// Function accepts a Vector2 and sets the position of the menu item to that Vector2
        /// </summary>
        /// <param name="a_Position">The new position of the item</param>
        /// <author>Douglas Wardle</author>
        /// <date>10/4/2017</date>
        public void SetPosition(Vector2 a_Position)
        {
            m_Position = a_Position;
        }

        /// <name>MenuItem::SetPosition()</name>
        /// <summary>
        /// Function accepts 2 integers, 1 for the new position X value and one for the new position Y value. sets the item position
        /// to the X and Y passed to it
        /// </summary>
        /// <param name="a_Position_X">The new X position of the item</param>
        /// <param name="a_Position_Y">The new Y position of the item</param>
        /// <author>Douglas Wardle</author>
        /// <date>10/4/2017</date>
        public void SetPosition(int a_Position_X, int a_Position_Y)
        {
            m_Position.X = a_Position_X;
            m_Position.Y = a_Position_Y;
        }
    }


}
