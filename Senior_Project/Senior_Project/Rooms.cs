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
        public bool m_InThisRoom = false;

        public List<Enemy> m_RoomEnemies = new List<Enemy>();
        public Vector2[] m_SpawnPoints = new Vector2[4];
        int enemyType = 1;


        //public Rectangle m_BoundingBox;

        public Rooms()
        {
            m_Texture = null;
            m_RoomPosition = new Vector2(0, 0);
            m_Floor.m_FloorPosition = new Vector2(64, 64);
            m_SpawnPoints = new Vector2[4]
            {
                new Vector2(m_RoomPosition.X + 128, m_RoomPosition.Y + 128),
                new Vector2(m_RoomPosition.X + 128, m_RoomPosition.Y + 640),
                new Vector2(m_RoomPosition.X + 768, m_RoomPosition.Y + 128),
                new Vector2(m_RoomPosition.X + 768, m_RoomPosition.Y + 640)
            };
            //m_BoundingBox = new Rectangle((int)m_RoomPosition.X, (int)m_RoomPosition.Y, 64, 64);
        }

        public Rooms(int a_RoomX, int a_RoomY)
        {
            m_RoomPosition = new Vector2(a_RoomX, a_RoomY);
            m_Floor.m_FloorPosition = new Vector2(a_RoomX + 64, a_RoomY + 64);
            m_SpawnPoints = new Vector2[4]
            {
                new Vector2(m_RoomPosition.X + 128, m_RoomPosition.Y + 128),
                new Vector2(m_RoomPosition.X + 128, m_RoomPosition.Y + 640),
                new Vector2(m_RoomPosition.X + 768, m_RoomPosition.Y + 128),
                new Vector2(m_RoomPosition.X + 768, m_RoomPosition.Y + 640)
            };
        }

        public void LoadContent(ContentManager a_Content)
        {
            m_Texture = a_Content.Load<Texture2D>("RoomWall");
            m_Floor.LoadContent(a_Content);

            foreach(Door d in m_RoomDoors)
            {
                d.LoadContent(a_Content);
            }
            
            if(enemyType == 1)
            {
                foreach (EnemyNoGun en in m_RoomEnemies)
                {
                    en.LoadContent(a_Content);
                }
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

            foreach (EnemyNoGun en in m_RoomEnemies)
            {
                en.Draw(a_SpriteBatch);
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
            this.SetSpawnPoints();
        }

        public bool IsDeadEnd()
        {
            return m_IsDeadEnd;
        }

        public void SetDeadEnd(bool a_IsDeadEnd)
        {
            this.m_IsDeadEnd = a_IsDeadEnd;
        }

        public void GenerateEnemies()
        {
            //Vector2[] m_SpawnPoints = new Vector2[4]
            //{
            //    new Vector2(m_RoomPosition.X + 128, m_RoomPosition.Y + 128),
            //    new Vector2(m_RoomPosition.X + 128, m_RoomPosition.Y + 640),
            //    new Vector2(m_RoomPosition.X + 768, m_RoomPosition.Y + 128),
            //    new Vector2(m_RoomPosition.X + 768, m_RoomPosition.Y + 640)
            //};
            Random enemyRand = new Random();
            int enemyType = 1;//enemyRand.Next(0, 4);
            int numEnemies = enemyRand.Next(0, 5);
            if (enemyType == 1)
            {
                for (int i = 0; i < 4; i++)
                {
                    EnemyNoGun newEnemy = new EnemyNoGun();
                    newEnemy.SetPosition(m_SpawnPoints[i].X, m_SpawnPoints[i].Y);
                    if (i % 2 == 0)
                    {
                        newEnemy.SetRotation('S');
                    }
                    else
                    {
                        newEnemy.SetRotation('W');
                    }
                    
                    m_RoomEnemies.Add(newEnemy);
                }
            }
        }

        public void ActivateEnemies()
        {
            for(int i = 0; i < m_RoomEnemies.Capacity; i++)
            {
                m_RoomEnemies[i].SetIsActive(true);
            }
        }

        public void DeactivateEnemies()
        {
            for (int i = 0; i < m_RoomEnemies.Capacity; i++)
            {
                m_RoomEnemies[i].SetIsActive(false);
            }
            ResetEnemies();
        }

        public void ResetEnemies()
        {
            for (int i = 0; i < m_RoomEnemies.Count; i++)
            {
                m_RoomEnemies[i].SetPosition(m_SpawnPoints[i].X, m_SpawnPoints[i].Y);
            }
        }

        public List<Enemy> GetEnemyList()
        {
            return m_RoomEnemies;
        }

        public int GetEnemyType()
        {
            return enemyType;
        }

        public void SetSpawnPoints()
        {
            m_SpawnPoints = new Vector2[4]
            {
                new Vector2(m_RoomPosition.X + 128, m_RoomPosition.Y + 128),
                new Vector2(m_RoomPosition.X + 128, m_RoomPosition.Y + 640),
                new Vector2(m_RoomPosition.X + 768, m_RoomPosition.Y + 128),
                new Vector2(m_RoomPosition.X + 768, m_RoomPosition.Y + 640)
            };
        }
    } 
}
