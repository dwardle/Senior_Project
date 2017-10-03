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
    //have done commenting
    public class Door
    {
        enum m_DoorPlacement {Up, Down, Left, Right };

        public Texture2D m_Texture;
        public Vector2 m_DoorPosition;
        public Rectangle m_HitBox;
        public bool m_IsDoorOpen = false;
        public int m_Placement;
        public int m_nextRoom = 0;

        /// <name>Door::Door()</name>
        /// <summary>
        /// Basic contructor for Door object
        /// </summary>
        /// <author>Douglas Wardle</author>
        /// <date></date>
        public Door()
        {
            m_Texture = null;
            m_DoorPosition = new Vector2(448, 0);
            m_HitBox = new Rectangle((int)m_DoorPosition.X, (int)m_DoorPosition.Y, 32, 32);
            m_Placement = (int)m_DoorPlacement.Up;
        }

        /// <name>Door::Door()</name>
        /// <summary>
        /// Constuctor that accepts an X and Y for the door position
        /// </summary>
        /// <param name="a_DoorX">new position X</param>
        /// <param name="a_DoorY">new position Y</param>
        /// <author>Douglas Wardle</author>
        /// <date></date>
        public Door(int a_DoorX, int a_DoorY)
        {
            m_DoorPosition = new Vector2(a_DoorX, a_DoorY);
        }

        /// <name>Door::SetDoorPlacement()</name>
        /// <summary>
        /// Sets the m_Placement value. m_Placement is the location the door is going to be placed in the room
        /// </summary>
        /// <param name="a_Placement">new door placement value</param>
        /// <author>Douglas Wardle</author>
        /// <date></date>
        public void SetDoorPlacement(int a_Placement)
        {
            if(a_Placement <= 0 || a_Placement >= 4)
            {
                return;
            }
            m_Placement = a_Placement;
        }

        /// <name>Door::SetDoorPosition()</name>
        /// <summary>
        /// Sets the door position in relation to its placement in the room.
        /// </summary>
        /// <param name="a_DoorX">Doors X position</param>
        /// <param name="a_DoorY">Doors Y position</param>
        /// <param name="a_Placement">Location in the room that the door will be placed</param>
        /// <author>Douglas Wardle</author>
        /// <date></date>
        public void SetDoorPosition(int a_DoorX, int a_DoorY, int a_Placement)
        {
            if(a_Placement == (int)m_DoorPlacement.Up)
            {
                m_Placement = a_Placement;
                m_DoorPosition = new Vector2(a_DoorX + 448, a_DoorY);
                //m_HitBox = new Rectangle((int)m_DoorPosition.X, (int)m_DoorPosition.Y, 64, 64);
            }
            else if (a_Placement == (int)m_DoorPlacement.Down)
            {
                m_Placement = a_Placement;
                m_DoorPosition = new Vector2(a_DoorX + 448, a_DoorY + 768);
                //m_HitBox = new Rectangle((int)m_DoorPosition.X, (int)m_DoorPosition.Y + (m_Texture.Width/2), 64, 32);
            }
            else if (a_Placement == (int)m_DoorPlacement.Left)
            {
                m_Placement = a_Placement;
                m_DoorPosition = new Vector2(a_DoorX, a_DoorY + 384);
                //m_HitBox = new Rectangle((int)m_DoorPosition.X, (int)m_DoorPosition.Y, 64, 64);
            }
            else if (a_Placement == (int)m_DoorPlacement.Right)
            {
                m_Placement = a_Placement;
                m_DoorPosition = new Vector2(a_DoorX + 896, a_DoorY + 384);
                //m_HitBox = new Rectangle((int)m_DoorPosition.X, (int)m_DoorPosition.Y, 32, 64);
            }
            //m_HitBox = new Rectangle((int)m_DoorPosition.X, (int)m_DoorPosition.Y, 32, 32);
        }

        /// <name>Door::LoadContent()</name>
        /// <summary>
        /// Function calls SetTexture to set and load the content for the door
        /// </summary>
        /// <param name="a_Content">Content manager contianing all the conetent for the game</param>
        /// <author>Douglas Wardle</author>
        /// <date></date>
        public void LoadContent(ContentManager a_Content)
        {
            SetTexture(a_Content);
        }

        /// <name>Door::Draw()</name>
        /// <summary>
        /// Function called every time object should be drawn
        /// </summary>
        /// <param name="a_SpriteBatch">allows drawing of sprites to the screen</param>
        /// <author>Douglas Wardle</author>
        /// <date></date>
        public void Draw(SpriteBatch a_SpriteBatch)
        {
            a_SpriteBatch.Draw(m_Texture, m_DoorPosition, Color.White);
        }

        //sets the door textures and hitboxes
        //hitboxes are set to be smaller then the actual door texture to make it look more like the player actually walked through the door

        /// <name>Door::SetTexture()</name>
        /// <summary>
        /// Fuction sets the texture of the door based on its placement and if it is open or not
        /// </summary>
        /// <param name="a_Content">content manager containing all content for the game</param>
        /// <author>Douglas Wardle</author>
        /// <date></date>
        public void SetTexture(ContentManager a_Content)
        {
            if (this.m_Placement == (int)m_DoorPlacement.Up)
            {
                if (this.m_IsDoorOpen == true)
                {
                    m_Texture = a_Content.Load<Texture2D>("Doors/DoorUp");
                }
                else
                {
                    m_Texture = a_Content.Load<Texture2D>("Doors/DoorUpClosed");
                }
                m_HitBox = new Rectangle((int)m_DoorPosition.X, (int)m_DoorPosition.Y, m_Texture.Width, m_Texture.Height / 2);
            }
            if (this.m_Placement == (int)m_DoorPlacement.Down)
            {
                if (this.m_IsDoorOpen == true)
                {
                    m_Texture = a_Content.Load<Texture2D>("Doors/DoorDown");
                }
                else
                {
                    m_Texture = a_Content.Load<Texture2D>("Doors/DoorDownClosed");
                }
                m_HitBox = new Rectangle((int)m_DoorPosition.X, (int)m_DoorPosition.Y + (m_Texture.Width / 2), m_Texture.Width, m_Texture.Height/2);
            }
            if (this.m_Placement == (int)m_DoorPlacement.Left)
            {
                if (this.m_IsDoorOpen == true)
                {
                    m_Texture = a_Content.Load<Texture2D>("Doors/DoorLeft");
                }
                else
                {
                    m_Texture = a_Content.Load<Texture2D>("Doors/DoorLeftClosed");
                }
                m_HitBox = new Rectangle((int)m_DoorPosition.X, (int)m_DoorPosition.Y, m_Texture.Width/2, m_Texture.Height);
            }
            if (this.m_Placement == (int)m_DoorPlacement.Right)
            {
                if (this.m_IsDoorOpen == true)
                {
                    m_Texture = a_Content.Load<Texture2D>("Doors/DoorRight");
                }
                else
                {
                    m_Texture = a_Content.Load<Texture2D>("Doors/DoorRightClosed");
                }
                m_HitBox = new Rectangle((int)m_DoorPosition.X + (m_Texture.Width / 2), (int)m_DoorPosition.Y, m_Texture.Width / 2, m_Texture.Height);
            }

        }

        /// <name>Door::SetIsOpen()</name>
        /// <summary>
        /// Function sets the m_IsDoorOpen value
        /// </summary>
        /// <param name="a_IsOpen">bool value, Should be true to set the door to open and false to set to closed</param>
        /// <author>Douglas Wardle</author>
        /// <date></date>
        public void SetIsOpen(bool a_IsOpen)
        {
            m_IsDoorOpen = a_IsOpen;
        }


    }


    /// <name>Door::DoorPlacement : Compare</name>
    /// <summary>
    /// Comparison class that will compare 2 doors and sort then in a door list by their placement value.
    /// it will be sorted from lowest placement value to highest;
    /// </summary>
    /// <author>Douglas Wardle</author>
    /// <date></date>
    public class DoorPlacement : Comparer<Door>
    {
        //sort doors by placement
        public override int Compare(Door x, Door y)
        {
            if(x.m_Placement.CompareTo(y.m_Placement ) != 0)
            {
                return x.m_Placement.CompareTo(y.m_Placement);
            }
            else
            {
                return 0;
            }
        }
    }
}