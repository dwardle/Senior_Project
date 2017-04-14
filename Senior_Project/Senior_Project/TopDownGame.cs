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
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class TopDownGame : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager m_Graphics;
        SpriteBatch m_SpriteBatch;
        public List<Rooms> m_RoomList = new List<Rooms>();
        public Rooms m_Room = new Rooms();
        public Rooms m_Room2 = new Rooms(0, -832);
        //RoomFloor m_Floor = new RoomFloor();
        public Door m_Door = new Door();
        Player m_MainPlayer;
        Camera m_Camera;
        Level m_Level_1 = new Level(1);
        const int m_roomWidth = 960;
        const int m_RoomHeight = 832;
        public int LevelCount = 1;


        public TopDownGame()
        {
            //960 x 832
            //room size
            m_Graphics = new GraphicsDeviceManager(this);
            m_Graphics.IsFullScreen = false;
            m_Graphics.PreferredBackBufferWidth = m_roomWidth;
            m_Graphics.PreferredBackBufferHeight = m_RoomHeight;
            m_MainPlayer = new Player(m_Level_1);
            this.Window.Title = "batdoug";
            Content.RootDirectory = "Content";
            this.IsMouseVisible = true;
            m_RoomList.Add(m_Room);
            m_RoomList.Add(m_Room2);
            m_Room2.GenerateDoors(0, 0);
            m_Room2.GenerateDoors(2, 2);
            m_Room2.GenerateDoors(2, 3);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            m_Camera = new Camera(GraphicsDevice.Viewport);
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            m_SpriteBatch = new SpriteBatch(GraphicsDevice);
            m_Level_1.LoadContent(Content);
            //m_Room.LoadContent(Content);
            //m_Room2.LoadContent(Content);
            //m_Floor.LoadContent(Content);
            //m_Door.LoadContent(Content);
            m_MainPlayer.LoadContent(Content);
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="a_GameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime a_GameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            m_MainPlayer.Update(a_GameTime, m_Level_1);
            List<Rooms> CurrenRoomList = new List<Rooms>();
            CurrenRoomList = m_Level_1.GetRoomList();
            //Go threw the top door
            if(CurrenRoomList[m_MainPlayer.RoomIndex].DoorExists((int)Level.m_DoorPlacement.Up))
            {
                int TopDoor = CurrenRoomList[m_MainPlayer.RoomIndex].FindDoor((int)Level.m_DoorPlacement.Up);
                if(CurrenRoomList[m_MainPlayer.RoomIndex].m_RoomDoors[TopDoor].m_BoundingBox.Intersects(m_MainPlayer.m_BoundingBox))
                {
                    m_MainPlayer.RoomIndex = CurrenRoomList[m_MainPlayer.RoomIndex].m_RoomDoors[TopDoor].m_nextRoom;
                    m_Camera.Update(a_GameTime, (int)CurrenRoomList[m_MainPlayer.RoomIndex].m_RoomPosition.X,
                        (int)CurrenRoomList[m_MainPlayer.RoomIndex].m_RoomPosition.Y);
                    m_MainPlayer.m_PlayerPosition.Y = (int)CurrenRoomList[m_MainPlayer.RoomIndex].m_RoomPosition.Y + 704;
                    m_MainPlayer.m_PlayerPosition.X = (int)CurrenRoomList[m_MainPlayer.RoomIndex].m_RoomPosition.X + 480;
                }
            }

            //go threw bottom door
            if (CurrenRoomList[m_MainPlayer.RoomIndex].DoorExists((int)Level.m_DoorPlacement.Down))
            {
                int DownDoor = CurrenRoomList[m_MainPlayer.RoomIndex].FindDoor((int)Level.m_DoorPlacement.Down);
                if (CurrenRoomList[m_MainPlayer.RoomIndex].m_RoomDoors[DownDoor].m_BoundingBox.Intersects(m_MainPlayer.m_BoundingBox))
                {
                    m_MainPlayer.RoomIndex = CurrenRoomList[m_MainPlayer.RoomIndex].m_RoomDoors[DownDoor].m_nextRoom;
                    m_Camera.Update(a_GameTime, (int)CurrenRoomList[m_MainPlayer.RoomIndex].m_RoomPosition.X,
                        (int)CurrenRoomList[m_MainPlayer.RoomIndex].m_RoomPosition.Y);
                    m_MainPlayer.m_PlayerPosition.Y = (int)CurrenRoomList[m_MainPlayer.RoomIndex].m_RoomPosition.Y + 128;
                    m_MainPlayer.m_PlayerPosition.X = (int)CurrenRoomList[m_MainPlayer.RoomIndex].m_RoomPosition.X + 480;
                }
            }

            //go threw left door
            if (CurrenRoomList[m_MainPlayer.RoomIndex].DoorExists((int)Level.m_DoorPlacement.Left))
            {
                int LeftDoor = CurrenRoomList[m_MainPlayer.RoomIndex].FindDoor((int)Level.m_DoorPlacement.Left);
                if (CurrenRoomList[m_MainPlayer.RoomIndex].m_RoomDoors[LeftDoor].m_BoundingBox.Intersects(m_MainPlayer.m_BoundingBox))
                {
                    m_MainPlayer.RoomIndex = CurrenRoomList[m_MainPlayer.RoomIndex].m_RoomDoors[LeftDoor].m_nextRoom;
                    m_Camera.Update(a_GameTime, (int)CurrenRoomList[m_MainPlayer.RoomIndex].m_RoomPosition.X,
                        (int)CurrenRoomList[m_MainPlayer.RoomIndex].m_RoomPosition.Y);
                    m_MainPlayer.m_PlayerPosition.Y = (int)CurrenRoomList[m_MainPlayer.RoomIndex].m_RoomPosition.Y + 416;
                    m_MainPlayer.m_PlayerPosition.X = (int)CurrenRoomList[m_MainPlayer.RoomIndex].m_RoomPosition.X + 832;
                }
            }

            //go threw right door
            if (CurrenRoomList[m_MainPlayer.RoomIndex].DoorExists((int)Level.m_DoorPlacement.Right))
            {
                int RightDoor = CurrenRoomList[m_MainPlayer.RoomIndex].FindDoor((int)Level.m_DoorPlacement.Right);
                if (CurrenRoomList[m_MainPlayer.RoomIndex].m_RoomDoors[RightDoor].m_BoundingBox.Intersects(m_MainPlayer.m_BoundingBox))
                {
                    m_MainPlayer.RoomIndex = CurrenRoomList[m_MainPlayer.RoomIndex].m_RoomDoors[RightDoor].m_nextRoom;
                    m_Camera.Update(a_GameTime, (int)CurrenRoomList[m_MainPlayer.RoomIndex].m_RoomPosition.X,
                        (int)CurrenRoomList[m_MainPlayer.RoomIndex].m_RoomPosition.Y);
                    m_MainPlayer.m_PlayerPosition.Y = (int)CurrenRoomList[m_MainPlayer.RoomIndex].m_RoomPosition.Y + 416;
                    m_MainPlayer.m_PlayerPosition.X = (int)CurrenRoomList[m_MainPlayer.RoomIndex].m_RoomPosition.X + 128;
                }
            }
            //Door Bounding
            //if (m_Door.m_BoundingBox.Intersects(m_MainPlayer.m_BoundingBox))
            //{
            //    m_MainPlayer.m_CurrentRoom++;
            //    m_Camera.Update(a_GameTime, (int)m_RoomList[m_MainPlayer.m_CurrentRoom].m_RoomPosition.X, 
            //        (int)m_RoomList[m_MainPlayer.m_CurrentRoom].m_RoomPosition.Y);
            //    //m_MainPlayer.m_PlayerPosition.X = 0;
            //    m_MainPlayer.m_PlayerPosition.Y = -300;
            //    //m_MainPlayer.m_CurrentRoom++;
            //}

            ////trying to get done what the above function does but with room and door lists instead of hard coded numbers
            //List<Rooms> currenLevel = m_Level_1.GetRoomList();
            //List<Door> currentDoors = currenLevel[m_MainPlayer.m_CurrentRoom].GetDoorList();
            //if(currentDoors[0].m_BoundingBox.Intersects(m_MainPlayer.m_BoundingBox))
            //{
            //    m_MainPlayer.m_CurrentRoom++;
            //    m_Camera.Update(a_GameTime, (int)currenLevel)
            //}

            //if (m_MainPlayer.m_BoundingBox.Intersects(m_Door.m_BoundingBox))
            //{
            //    m_Camera.Update(a_GameTime);
            //}
            //m_Camera.Update(a_GameTime);
            base.Update(a_GameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="a_GameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime a_GameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            if(m_Door.m_IsDoorOpen == true)
            {
                m_Door.LoadContent(Content);
            }
            m_SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, m_Camera.m_Transform);

            //m_Room.Draw(m_SpriteBatch);
            //m_Room2.Draw(m_SpriteBatch);
            //m_Floor.Draw(m_SpriteBatch);
            //m_Door.Draw(m_SpriteBatch);
            m_Level_1.Draw(m_SpriteBatch);
            m_MainPlayer.Draw(m_SpriteBatch);

            m_SpriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(a_GameTime);
        }
    }
}