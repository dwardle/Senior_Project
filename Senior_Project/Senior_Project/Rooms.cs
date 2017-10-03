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
    //Have done commenting


    public class Rooms
    {
        //Work on 9/27 start to add boss to game

        enum m_DoorPlacement { Up, Down, Left, Right };

        public Texture2D m_Texture;
        public Vector2 m_RoomPosition;
        public List<Door> m_RoomDoors = new List<Door>();
        public RoomFloor m_Floor = new RoomFloor();
        public bool m_IsDeadEnd = false;
        Random doorGen = new Random();
        DoorPlacement doorComp = new DoorPlacement();
        public bool m_InThisRoom = false;
        public bool m_RoomEmpty = false;
        public Vector2 m_RoomIndex;
        public Boss m_RoomBoss;

        public List<Enemy> m_RoomEnemies = new List<Enemy>();
        public Vector2[] m_SpawnPoints = new Vector2[4];
        int enemyType = 1;

        //boolean values to tell if this is a boss room or an item room. if both are false it is a regular room
        public bool m_IsItemRoom;
        public bool m_IsBossRoom;

        //new random number generator for 

        //public Rectangle m_HitBox;


        //public Rooms()
        //{
        //    m_Texture = null;
        //    m_IsItemRoom = false;
        //    m_IsBossRoom = false;
        //    m_RoomPosition = new Vector2(0, 0);
        //    m_Floor.m_FloorPosition = new Vector2(64, 64);
        //    m_SpawnPoints = new Vector2[4]
        //    {
        //        new Vector2(m_RoomPosition.X + 128, m_RoomPosition.Y + 128),
        //        new Vector2(m_RoomPosition.X + 128, m_RoomPosition.Y + 640),
        //        new Vector2(m_RoomPosition.X + 768, m_RoomPosition.Y + 128),
        //        new Vector2(m_RoomPosition.X + 768, m_RoomPosition.Y + 640)
        //    };
        //    //m_HitBox = new Rectangle((int)m_RoomPosition.X, (int)m_RoomPosition.Y, 64, 64);
        //}

        //new constructor for first room. can replace regular constructor after all calls to original have been removed

        /// <name>Rooms::Rooms()</name>
        /// <summary>
        /// Constructor for Rooms object, Sets the room index so that the room will be placed in the middle of the levelRooms array.
        /// </summary>
        /// <param name="a_NumRooms">Total number of rooms for the game level</param>
        /// <author>Douglas Wardle</author>
        /// <date></date>
        public Rooms(int a_NumRooms)
        {
            m_Texture = null;
            m_IsItemRoom = false;
            m_IsBossRoom = false;
            m_RoomPosition = new Vector2(0, 0);
            m_Floor.m_FloorPosition = new Vector2(64, 64);
            m_RoomIndex.X = a_NumRooms / 2;
            m_RoomIndex.Y = a_NumRooms / 2;
            m_SpawnPoints = new Vector2[4]
            {
                new Vector2(m_RoomPosition.X + 128, m_RoomPosition.Y + 128),
                new Vector2(m_RoomPosition.X + 128, m_RoomPosition.Y + 640),
                new Vector2(m_RoomPosition.X + 768, m_RoomPosition.Y + 128),
                new Vector2(m_RoomPosition.X + 768, m_RoomPosition.Y + 640)
            };
            //m_HitBox = new Rectangle((int)m_RoomPosition.X, (int)m_RoomPosition.Y, 64, 64);
        }

        /// <name>Rooms::Rooms()</name>
        /// <summary>
        /// Constructor for Room object, set room position to a Vector2 with the first number passed as the X coordinate and the 
        /// second number passed as the Y coordinate
        /// </summary>
        /// <param name="a_RoomX">Room position X coordinate</param>
        /// <param name="a_RoomY">Room position Y coordinate</param>
        /// <author>Douglas Wardle</author>
        /// <date></date>
        public Rooms(int a_RoomX, int a_RoomY)
        {
            m_RoomPosition = new Vector2(a_RoomX, a_RoomY);
            m_IsItemRoom = false;
            m_IsBossRoom = false;
            m_Floor.m_FloorPosition = new Vector2(a_RoomX + 64, a_RoomY + 64);
            m_SpawnPoints = new Vector2[4]
            {
                new Vector2(m_RoomPosition.X + 128, m_RoomPosition.Y + 128),
                new Vector2(m_RoomPosition.X + 128, m_RoomPosition.Y + 640),
                new Vector2(m_RoomPosition.X + 768, m_RoomPosition.Y + 128),
                new Vector2(m_RoomPosition.X + 768, m_RoomPosition.Y + 640)
            };
        }

        //new room constructor to create a room with its index in the array stored
        /// <name>Rooms::Rooms()</name>
        /// <summary>
        /// constructor for a new room with the rooms position specified by first and second parameter and stores the index
        /// at which the room will be store in the levelArray
        /// </summary>
        /// <param name="a_RoomX">room position X coordinate</param>
        /// <param name="a_RoomY">room position Y coordinate</param>
        /// <param name="index_x">room Row index</param>
        /// <param name="index_y">room column index</param>
        /// <author>Douglas Wardle</author>
        /// <date></date>
        public Rooms(int a_RoomX, int a_RoomY, int index_x, int index_y)
        {
            m_RoomIndex.X = index_x;
            m_RoomIndex.Y = index_y;
            m_IsItemRoom = false;
            m_IsBossRoom = false;
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

        //new function to get room index
        /// <name>Rooms::GetRoomIndex()</name>
        /// <summary>
        /// Accesses the index at which the room is in an array. The m_RoomIndex is a Vector2
        /// </summary>
        /// <returns>Vector2 with the rooms row as the Vector2 X and the column as Y</returns>
        /// <author>Douglas Wardle</author>
        /// <date></date>
        public Vector2 GetRoomIndex()
        {
            return m_RoomIndex;
        }

        // these loadcontent and draw functions should still work regardless of room array///////////////////////////////////////////////////////
        /// <name>Rooms::LoadContent()</name>
        /// <summary>
        /// Called whenever a room needs to load its textures. This function will load the textures for the Room Walls, room floor, room doors, and Room enemies
        /// </summary>
        /// <param name="a_Content">content manager containing all content for the game</param>
        /// <author>Douglas Wardle</author>
        /// <date></date>
        public void LoadContent(ContentManager a_Content)
        {
            m_Texture = a_Content.Load<Texture2D>("Rooms/RoomWall");
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
            else if(enemyType == 2)
            {
                if(m_RoomBoss != null)
                {
                    m_RoomBoss.LoadContent(a_Content);
                }
                
            }
            
        }

        /// <name>Rooms::Draw()</name>
        /// <summary>
        /// Fuction should be called whenever the room needs to be drawn. it will also call draw functions for anything contained in the room
        /// </summary>
        /// <param name="a_SpriteBatch">SpriteBatch object to allow sprites to be drawn</param>
        /// <author>Douglas Wardle</author>
        /// <date></date>
        public void Draw(SpriteBatch a_SpriteBatch)
        {
            a_SpriteBatch.Draw(m_Texture, m_RoomPosition, Color.White);
            m_Floor.Draw(a_SpriteBatch);
            foreach (Door d in m_RoomDoors)
            {
                d.Draw(a_SpriteBatch);
            }

            if (m_IsBossRoom)
            {
                if(m_RoomBoss.m_IsAlive)
                {
                    m_RoomBoss.Draw(a_SpriteBatch);
                }
                
            }
            else
            {
                foreach (EnemyNoGun en in m_RoomEnemies)
                {
                    en.Draw(a_SpriteBatch);
                }
            }
            

        }
        /////////////////////////////////////////////////////////////////////////////////////
        //Am not using this right now. possibly will be deleted
        /// <name>Rooms::GenerateDoors()</name>
        /// <summary>
        /// Function will randomly generate doors and place them in the room. a room may contain up to 4 doors but no less then 1
        /// </summary>
        /// <author>Douglas Wardle</author>
        /// <date></date>
        //public void GenerateDoors()
        //{
        //    int numDoors = doorGen.Next(1, 5);
        //    int Placement;
        //    while(numDoors > 0)
        //    {
        //        Placement = doorGen.Next(0, 4);
        //        if (DoorExists(Placement) ==  false)
        //        {
        //            CreateDoor(Placement);
        //            numDoors--;
        //        }
        //    }
        //    this.m_RoomDoors.Sort(doorComp);
        //}


        //a_LastDoorLocation may not be used 
        /// <name>Rooms::GenerateDoors()</name>
        /// <summary>
        /// Function will randomly generate doors in the room. it will first get a random number between 0-3 to
        /// decide how many doors to add. if the number generated is larger then the amount of rooms remaining to be created
        /// it will decrement the number of doors until it is less then the remaining number of rooms. then it will create a door
        /// for the number of doors and use a random number between 0-3 to place the door in the room
        /// </summary>
        /// <param name="a_RoomsRemaining">The amount of rooms that still need to be created</param>
        /// <param name="a_LastDoorLocation"></param>
        /// <author>Douglas Wardle</author>
        /// <date></date>
        public void GenerateDoors(int a_RoomsRemaining, int a_LastDoorLocation)
        {
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

        /// <name>Rooms::DoorExists()</name>
        /// <summary>
        /// Checks the door list of the room to see if a door already exists at the placement that is passed to the function
        /// </summary>
        /// <param name="a_Placement">placement that is being checked for an existing door</param>
        /// <returns>Returns true if a door already exists and false if it does not</returns>
        /// <author>Douglas Wardle</author>
        /// <date></date>
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

        /// <name>Rooms::CreateDoor()</name>
        /// <summary>
        /// Creates a door in the current room at the at placement passed to the function
        /// </summary>
        /// <param name="a_Placement">integer representing the placement of the door within the room</param>
        /// <author>Douglas Wardle</author>
        /// <date></date>
        public void CreateDoor(int a_Placement)
        {
            Door nDoor = new Door();
            nDoor.SetDoorPosition((int)m_RoomPosition.X, (int)m_RoomPosition.Y, a_Placement);
            this.m_RoomDoors.Add(nDoor);
        }

        //not currently using this version of CreateDoor possibly remove
        ///// <name>Rooms::</name>
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="a_Placement"></param>
        ///// <param name="a_NextRoomIndex"></param>
        ///// <author>Douglas Wardle</author>
        ///// <date></date>
        //public void CreateDoor(int a_Placement, int a_NextRoomIndex)
        //{
        //    Door nDoor = new Door();
        //    nDoor.SetDoorPosition((int)m_RoomPosition.X, (int)m_RoomPosition.Y, a_Placement);
        //    nDoor.m_nextRoom = a_NextRoomIndex;
        //    this.m_RoomDoors.Add(nDoor);
        //}

        /// <name>Rooms::GetDoorList()</name>
        /// <summary>
        /// Access the rooms door list
        /// </summary>
        /// <returns>returns a list containing all the doors for the room</returns>
        /// <author>Douglas Wardle</author>
        /// <date></date>
        public List<Door> GetDoorList()
        {
            return this.m_RoomDoors;
        }

        /// <name>Rooms::FindDoor()</name>
        /// <summary>
        /// Looks through a door list for a door at a specific placement. if a door is found at this placement function will return
        /// the index in the door list at which the door is located
        /// </summary>
        /// <param name="a_Placement">placement of door that is being searched for</param>
        /// <returns>Returns index of door within the door list if a door is found at the placement specified. if not return -1</returns>
        /// <author>Douglas Wardle</author>
        /// <date></date>
        public int FindDoor(int a_Placement)
        {
            for (int i = 0; i < m_RoomDoors.Count; i++)
            {
                if (m_RoomDoors[i].m_Placement == a_Placement)
                {
                    return i;
                }
            }
            return -1;
        }

        /// <name>Rooms::MoveRoom()</name>
        /// <summary>
        /// Function accepts an X and Y value for the position the room is to be moved, takes those positions and changes the room position and 
        /// floor position to the new positions. then sets the spawn points for the enemies in that room
        /// </summary>
        /// <param name="a_RoomX">X position coordinate</param>
        /// <param name="a_RoomY">Y position coordinate</param>
        /// <author>Douglas Wardle</author>
        /// <date></date>
        public void MoveRoom(int a_RoomX, int a_RoomY)
        {
            this.m_RoomPosition.X = a_RoomX;
            this.m_RoomPosition.Y = a_RoomY;
            this.m_Floor.m_FloorPosition.X += a_RoomX;
            this.m_Floor.m_FloorPosition.Y += a_RoomY;
            this.SetSpawnPoints();
        }

        //new move room
        //m_roomWidth = 960;
        //m_RoomHeight = 832;
        /*public void MoveRoom()
        {
            this.m_RoomPosition.X = a_RoomX;
            this.m_RoomPosition.Y = a_RoomY;
            this.m_Floor.m_FloorPosition.X += a_RoomX;
            this.m_Floor.m_FloorPosition.Y += a_RoomY;
            this.SetSpawnPoints();
        }*/

        /// <name>Rooms::IsDeadEnd()</name>
        /// <summary>
        /// Accesses the rooms m_IsDeadEnd value. this value is true if the room is a dead end and false if it is not
        /// </summary>
        /// <returns>true if the room is a dead end, false if it is not</returns>
        /// <author>Douglas Wardle</author>
        /// <date></date>
        public bool IsDeadEnd()
        {
            return m_IsDeadEnd;
        }


        /// <name>Rooms::SetDeadEnd()</name>
        /// <summary>
        /// Accepts boolean value and assigns it to m_IsDeadEnd
        /// </summary>
        /// <param name="a_IsDeadEnd">boolean value, true to represent the room is a dead end and false if it is not</param>
        /// <author>Douglas Wardle</author>
        /// <date></date>
        public void SetDeadEnd(bool a_IsDeadEnd)
        {
            this.m_IsDeadEnd = a_IsDeadEnd;
        }

        /// <name>Rooms::GenerateEnemies()</name>
        /// <summary>
        /// Generates enemies for the room and places them at 4 different spawn points. it then sets the enemies rotation value
        /// so all enemies are either facing down or up
        /// </summary>
        /// <author>Douglas Wardle</author>
        /// <date></date>
        public void GenerateEnemies()
        {
            //enemy type 1 = enemy no gun
            //enemy type 2 = Boss
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

        /// <name>Rooms::ActivateEnemies()</name>
        /// <summary>
        /// Sets all enemies to active. enemies will not move or do anything unless first activated
        /// </summary>
        /// <author>Douglas Wardle</author>
        /// <date></date>
        public void ActivateEnemies()
        {
            foreach(Enemy en in m_RoomEnemies)
            {
                en.SetIsActive(true);
            }
        }


        // may not need this because player wont be able to travel back to a rooom until he first clears the room
        /// <name>Rooms::DeactivateEnemies()</name>
        /// <summary>
        /// Function is used to set all enemies to not active
        /// </summary>
        /// <author>Douglas Wardle</author>
        /// <date></date>
        public void DeactivateEnemies()
        {
            foreach(Enemy en in m_RoomEnemies)
            {
                en.SetIsActive(false);
            }
            ResetEnemies();
        }


        /// <name>Rooms::ResetEnemies()</name>
        /// <summary>
        /// Function will reset the room enemies to their original positions before the player entered the room
        /// </summary>
        /// <author>Douglas Wardle</author>
        /// <date></date>
        public void ResetEnemies()
        {
            for (int i = 0; i < m_RoomEnemies.Count; i++)
            {
                m_RoomEnemies[i].SetPosition(m_SpawnPoints[i].X, m_SpawnPoints[i].Y);
                //added to reset hitboxes if player leaves the room and comes back in
                if(i % 2 == 0)
                {
                    m_RoomEnemies[i].SetHitbox('S');
                }
                else
                {
                    m_RoomEnemies[i].SetHitbox('W');
                }
                
            }
        }

        /// <name>Rooms::GetEnemyList()</name>
        /// <summary>
        /// Accesses the enemy list for the room
        /// </summary>
        /// <returns>List containing all the enemies in the room</returns>
        /// <author>Douglas Wardle</author>
        /// <date></date>
        public List<Enemy> GetEnemyList()
        {
            return m_RoomEnemies;
        }

        /// <name>Rooms::GetEnemyType()</name>
        /// <summary>
        /// Accesses the type of enemy that is in the room
        /// </summary>
        /// <returns>returns an integer value that represents the enemy type for the room</returns>
        /// <author>Douglas Wardle</author>
        /// <date></date>
        public int GetEnemyType()
        {
            return enemyType;
        }


        /// <name>Rooms::SetSpawnPoints()</name>
        /// <summary>
        /// Sets the Spawn points to the correct positions in the room
        /// </summary>
        /// <author>Douglas Wardle</author>
        /// <date></date>
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


        /// <name>Rooms::RoomClear()</name>
        /// <summary>
        /// Checks to see if the player has cleared all enemies in the room
        /// </summary>
        /// <returns>If room enemy list is empty then return true, if not return false</returns>
        /// <author>Douglas Wardle</author>
        /// <date></date>
        public bool RoomClear()
        {
            if(m_RoomEnemies.Any() == false)
            {
                m_RoomEmpty = true;
                return m_RoomEmpty;
            }
            else
            {
                return m_RoomEmpty;
            }
        }



        //new functions to get the room x and y coordinates seperatly
        /// <name>Rooms::GetRoomCoord_X</name>
        /// <summary>
        /// Accesses just the X coordinate of the room position
        /// </summary>
        /// <returns>integer containing the X coordinate of the room position</returns>
        /// <author>Douglas Wardle</author>
        /// <date></date>
        public int GetRoomCoord_X()
        {
            return (int)m_RoomPosition.X;
        }

        /// <name>Rooms::GetRoomCoord_Y</name>
        /// <summary>
        /// Accesses just the Y coordinate of the room position
        /// </summary>
        /// <returns>integer containing the Y coordinate of the room position</returns>
        /// <author>Douglas Wardle</author>
        /// <date></date>
        public int GetRoomCoord_Y()
        {
            return (int)m_RoomPosition.Y;
        }

        //new functions to get the row and column seperately
        /// <name>Rooms::GetRoomRow()</name>
        /// <summary>
        /// Accesses the row index of the current room in the games array of rooms
        /// </summary>
        /// <returns>returns an integer for the row index of the room in the games room array</returns>
        /// <author>Douglas Wardle</author>
        /// <date></date>
        public int GetRoomRow()
        {
            return (int)m_RoomIndex.X;
        }

        /// <name>Rooms::GetRoomCol()</name>
        /// <summary>
        /// Accesses the column index of the current room in the games array of rooms
        /// </summary>
        /// <returns>returns an integer for the column index of the room in the games room array</returns>
        /// <author>Douglas Wardle</author>
        /// <date></date>
        public int GetRoomCol()
        {
            return (int)m_RoomIndex.Y;
        }

        /// <name>Rooms::GetRoomPosition()</name>
        /// <summary>
        /// Accesses the Vector2 containing the rooms position
        /// </summary>
        /// <returns>a Vector2 containing the room position</returns>
        /// <author>Douglas Wardle</author>
        /// <date></date>
        public Vector2 GetRoomPosition()
        {
            return m_RoomPosition;
        }

        /// <name>Rooms::SetAsItemRoom</name>
        /// <summary>
        /// Sets the room as an item room by changing its m_IsItemRoom value to true
        /// </summary>
        /// <author>Douglas Wardle</author>
        /// <date></date>
        public void SetAsItemRoom()
        {
            m_IsItemRoom = true;
        }

        /// <name>Rooms::SetAsBossRoom()</name>
        /// <summary>
        /// Sets the room as a boss room by changing its m_IsBossRoom value to true
        /// </summary>
        /// <author>Douglas Wardle</author>
        /// <date></date>
        public void SetAsBossRoom()
        {
            m_IsBossRoom = true;
        }

        /// <name>Rooms::IsBossRoom()</name>
        /// <summary>
        /// Accesses the rooms m_IsBossRoom value. this value is true if the room is a boss room and false if it is not
        /// </summary>
        /// <returns>true if the room is a boss room, false if it is not</returns>
        /// <author>Douglas Wardle</author>
        /// <date></date>
        public bool IsBossRoom()
        {
            return m_IsBossRoom;
        }

        /// <name>Rooms::IsItemRoom()</name>
        /// <summary>
        /// Accesses the rooms m_IsItemRoom value. this value is true if the room is a boss room and false if it is not
        /// </summary>
        /// <returns>true if the room is an item room and false if it is not</returns>
        /// <author>Douglas Wardle</author>
        /// <date></date>
        public bool IsItemRoom()
        {
            return m_IsItemRoom;
        }

        /// <name>Rooms::CreateBoss()</name>
        /// <summary>
        /// Function first checks that the room is a boss room. then it creates a boss and places it in the room
        /// </summary>
        /// <author>Douglas Wardle</author>
        /// <date></date>
        public void CreateBoss()
        {
            if(m_IsBossRoom != true)
            {
                return;
            }
            enemyType = 2;
            Boss levelBoss = new Boss();
            levelBoss.SetMoveDelay(500); 
            levelBoss.SetPosition(m_SpawnPoints[0].X, m_SpawnPoints[0].Y);
            m_RoomEnemies.Add(levelBoss);
        }

        /// <name>Rooms::CreateBoss1()</name>
        /// <summary>
        /// checks if the room is a boss room. then if it is a boss room it will create a boss and initialize the m_RoomBoss to the newly created boss
        /// and sets its position to the center of the room
        /// </summary>
        /// <author>Douglas Wardle</author>
        /// <date></date>
        public void CreateBoss1()
        {
            if (m_IsBossRoom != true)
            {
                return;
            }
            enemyType = 2;
            m_RoomBoss = new Boss();
            m_RoomBoss.SetMoveDelay(50);
            //set the boss location to middle of the room so that no mater what door they come in the player is not getting hit when they enter
            m_RoomBoss.SetPosition(m_RoomPosition.X + (480 - (m_RoomBoss.GetTextureOriginX() / 32)), m_RoomPosition.Y + (416 - (m_RoomBoss.GetTextureOriginY()/32))); //- m_RoomBoss.GetTextureOriginX())// //

            //m_RoomEnemies.Add(levelBoss);
        }

        /// <name>Rooms::CreateBoss1()</name>
        /// <summary>
        /// Checks if the room is a boss room, if not return and do not create a boss. if it is a boss room it creates a new boss
        /// then multiplies the bosses health, speed, and damage based on the level number. then sets the boss position to the center
        /// of the current room
        /// </summary>
        /// <param name="a_LevelNumber"></param>
        /// <author>Douglas Wardle</author>
        /// <date></date>
        public void CreateBoss1(int a_LevelNumber)
        {
            if (m_IsBossRoom != true)
            {
                return;
            }
            enemyType = 2;
            
            m_RoomBoss = new Boss();
            if (a_LevelNumber == 1)
            {
                
            }
            else if (a_LevelNumber == 2)
            {
                m_RoomBoss.MultiplyDamage(1f);
                m_RoomBoss.MultiplyHealth(.5f);
                m_RoomBoss.MultiplySpeed(.25f);
            }
            else if (a_LevelNumber == 3)
            {
                m_RoomBoss.MultiplyDamage(2f);
                m_RoomBoss.MultiplyHealth(.5f);
                m_RoomBoss.MultiplySpeed(.25f);
            }
            m_RoomBoss.SetMoveDelay(50);
            //set the boss location to middle of the room so that no mater what door they come in the player is not getting hit when they enter
            m_RoomBoss.SetPosition(m_RoomPosition.X + (480 - (m_RoomBoss.GetTextureOriginX() / 32)), m_RoomPosition.Y + (416 - (m_RoomBoss.GetTextureOriginY() / 32))); //- m_RoomBoss.GetTextureOriginX())// //

            //m_RoomEnemies.Add(levelBoss);
        }



        /// <name>Rooms::GetBoss()</name>
        /// <summary>
        /// Accesses the rooms m_RoomBoss object. this object will not be initialized unless CreateBoss() has been called
        /// </summary>
        /// <returns></returns>
        /// <author>Douglas Wardle</author>
        /// <date></date>
        public Boss GetBoss()
        {
            return m_RoomBoss;
        }

    }
}
