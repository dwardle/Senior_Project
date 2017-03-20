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
        enum m_DoorPlacement { Up, Down, Left, Right };

        public Texture2D m_Texture;
        public Vector2 m_RoomPosition;
        public List<Door> m_RoomDoors = new List<Door>();
        public RoomFloor m_Floor = new RoomFloor();
        public bool m_IsDeadEnd = false;
        Random doorGen = new Random();
        DoorPlacement doorComp = new DoorPlacement();

        //public Rectangle m_BoundingBox;

        public Rooms()
        {
            m_Texture = null;
            m_RoomPosition = new Vector2(0, 0);
            m_Floor.m_FloorPosition = new Vector2(64, 64);
            //m_BoundingBox = new Rectangle((int)m_RoomPosition.X, (int)m_RoomPosition.Y, 64, 64);
        }

        public Rooms(int a_RoomX, int a_RoomY)
        {
            m_RoomPosition = new Vector2(a_RoomX, a_RoomY);
            m_Floor.m_FloorPosition = new Vector2(a_RoomX + 64, a_RoomY + 64);
        }

        public void LoadContent(ContentManager a_Content)
        {
            m_Texture = a_Content.Load<Texture2D>("RoomWall");
            m_Floor.LoadContent(a_Content);
            foreach(Door d in m_RoomDoors)
            {
                d.LoadContent(a_Content);
            }
        }

        public void Draw(SpriteBatch a_SpriteBatch)
        {
            a_SpriteBatch.Draw(m_Texture, m_RoomPosition, Color.White);
            m_Floor.Draw(a_SpriteBatch);
            foreach (Door d in m_RoomDoors)
            {
                d.Draw(a_SpriteBatch);
            }
        }
        public void GenerateDoors()
        {
            int numDoors = doorGen.Next(1, 5);
            int Placement;
            while(numDoors > 0)
            {
                Placement = doorGen.Next(0, 4);
                if (DoorExists(Placement) ==  false)
                {
                    CreateDoor(Placement);
                    numDoors--;
                }
            }
            this.m_RoomDoors.Sort(doorComp);
        }

        public void GenerateDoors(int a_RoomsRemaining, int a_LastDoorLocation)
        {
            //448 is (the width of room wall - 64) / 2 or (960 - 64)/2
            //768 is (the length of room wall - 64) or (832 - 64)
            //896 is (the width of room wall - 64) or (960 - 64)
            //384 is (the length of room wall - 64) / 2 or (832 - 64) / 2
            //int numDoors = doorGen.Next(1, 4);

            //if (a_LastDoorLocation == (int)m_DoorPlacement.Up)
            //{
            //    //place first door
            //    CreateDoor((int)m_DoorPlacement.Down);
            //}
            //else if(a_LastDoorLocation == (int)m_DoorPlacement.Down)
            //{
            //    CreateDoor((int)m_DoorPlacement.Up);
            //    //Door nDoor = new Door((int)this.m_RoomPosition.X + 448, (int)this.m_RoomPosition.Y);
            //    //nDoor.SetDoorPlacement((int)m_DoorPlacement.Up);
            //    //this.m_RoomDoors.Add(nDoor);
            //}
            //else if (a_LastDoorLocation == (int)m_DoorPlacement.Left)
            //{
            //    CreateDoor((int)m_DoorPlacement.Right);
            //}
            //else if (a_LastDoorLocation == (int)m_DoorPlacement.Right)
            //{
            //    CreateDoor((int)m_DoorPlacement.Left);
            //}

            int numDoors = doorGen.Next(0, 4);
            while (numDoors >= a_RoomsRemaining)
            {
                numDoors--;
            }
            int Placement;
            while (numDoors > 0)
            {
                Placement = doorGen.Next(0, 4);
                if (DoorExists(Placement) == false)
                {
                    CreateDoor(Placement);
                    numDoors--;
                }
            }
            //sort doors by door placement in accending order
            
            this.m_RoomDoors.Sort(doorComp);

        }

        public bool DoorExists(int a_Placement)
        {
            bool exists = false;
            foreach (Door d in this.m_RoomDoors)
            {
                if (a_Placement == d.m_Placement)
                {
                    exists = true;
                    break;
                }
            }
            return exists;
        }

        public void CreateDoor(int a_Placement)
        {
            Door nDoor = new Door();
            nDoor.SetDoorPosition((int)m_RoomPosition.X, (int)m_RoomPosition.Y, a_Placement);
            this.m_RoomDoors.Add(nDoor);
        }

        public void CreateDoor(int a_Placement, int a_NextRoomIndex)
        {
            Door nDoor = new Door();
            nDoor.SetDoorPosition((int)m_RoomPosition.X, (int)m_RoomPosition.Y, a_Placement);
            nDoor.m_nextRoom = a_NextRoomIndex;
            this.m_RoomDoors.Add(nDoor);
        }

        public List<Door> GetDoorList()
        {
            return this.m_RoomDoors;
        }

        public int FindDoor(int a_Placement)
        {
            for(int i = 0; i < m_RoomDoors.Count; i++)
            {
                if(m_RoomDoors[i].m_Placement == a_Placement)
                {
                    return i;
                }
            }
            return -1;
        }

        public void MoveRoom(int a_RoomX, int a_RoomY)
        {
            this.m_RoomPosition.X = a_RoomX;
            this.m_RoomPosition.Y = a_RoomY;
            this.m_Floor.m_FloorPosition.X += a_RoomX;
            this.m_Floor.m_FloorPosition.Y += a_RoomY;
        }

        public bool IsDeadEnd()
        {
            return m_IsDeadEnd;
        }

        public void SetDeadEnd(bool a_IsDeadEnd)
        {
            this.m_IsDeadEnd = a_IsDeadEnd;
        }
    } 
}
