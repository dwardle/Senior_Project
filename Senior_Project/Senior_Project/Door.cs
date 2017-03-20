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
        enum m_DoorPlacement {Up, Down, Left, Right };

        public Texture2D m_Texture;
        public Vector2 m_DoorPosition;
        public Rectangle m_BoundingBox;
        public bool m_IsDoorOpen = true;
        public int m_Placement;
        public int m_nextRoom = 0;

        public Door()
        {
            m_Texture = null;
            m_DoorPosition = new Vector2(448, 0);
            m_BoundingBox = new Rectangle((int)m_DoorPosition.X, (int)m_DoorPosition.Y, 64, 64);
            m_Placement = (int)m_DoorPlacement.Up;
        }

        public Door(int a_DoorX, int a_DoorY)
        {
            m_DoorPosition = new Vector2(a_DoorX, a_DoorY);
        }

        public void SetDoorPlacement(int a_Placement)
        {
            if(a_Placement <= 0 || a_Placement >= 4)
            {
                return;
            }
            m_Placement = a_Placement;
        }

        public void SetDoorPosition(int a_DoorX, int a_DoorY, int a_Placement)
        {
            if(a_Placement == (int)m_DoorPlacement.Up)
            {
                m_Placement = a_Placement;
                m_DoorPosition = new Vector2(a_DoorX + 448, a_DoorY);
            }
            else if (a_Placement == (int)m_DoorPlacement.Down)
            {
                m_Placement = a_Placement;
                m_DoorPosition = new Vector2(a_DoorX + 448, a_DoorY + 768);
            }
            else if (a_Placement == (int)m_DoorPlacement.Left)
            {
                m_Placement = a_Placement;
                m_DoorPosition = new Vector2(a_DoorX, a_DoorY + 384);
            }
            else if (a_Placement == (int)m_DoorPlacement.Right)
            {
                m_Placement = a_Placement;
                m_DoorPosition = new Vector2(a_DoorX + 896, a_DoorY + 384);
            }
            m_BoundingBox = new Rectangle((int)m_DoorPosition.X, (int)m_DoorPosition.Y, 64, 64);
        }

        public void LoadContent(ContentManager a_Content)
        {
            if(this.m_Placement == (int)m_DoorPlacement.Up)
            {
                if (this.m_IsDoorOpen == true)
                {
                    m_Texture = a_Content.Load<Texture2D>("DoorUp");
                }
                else
                {
                    m_Texture = a_Content.Load<Texture2D>("DoorUpClosed");
                }
            }
            if (this.m_Placement == (int)m_DoorPlacement.Down)
            {
                if (this.m_IsDoorOpen == true)
                {
                    m_Texture = a_Content.Load<Texture2D>("DoorDown");
                }
                else
                {
                    m_Texture = a_Content.Load<Texture2D>("DoorDownClosed");
                }
            }
            if (this.m_Placement == (int)m_DoorPlacement.Left)
            {
                if (this.m_IsDoorOpen == true)
                {
                    m_Texture = a_Content.Load<Texture2D>("DoorLeft");
                }
                else
                {
                    m_Texture = a_Content.Load<Texture2D>("DoorLeftClosed");
                }
            }
            if (this.m_Placement == (int)m_DoorPlacement.Right)
            {
                if (this.m_IsDoorOpen == true)
                {
                    m_Texture = a_Content.Load<Texture2D>("DoorRight");
                }
                else
                {
                    m_Texture = a_Content.Load<Texture2D>("DoorRightClosed");
                }
            }
        }

        public void Draw(SpriteBatch a_SpriteBatch)
        {
            a_SpriteBatch.Draw(m_Texture, m_DoorPosition, Color.White);
        }


    }

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