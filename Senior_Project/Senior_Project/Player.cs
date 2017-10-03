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

//have done commenting

namespace Senior_Project
{
    
    public class Player
    {
        //Player Stats
        public float m_PlayerHealth = 3f;
        public float m_MaxHealth = 3f;
        public float m_ShotDelay = 20f;
        public float m_PlayerSpeed;
        public float m_ShotRange = 300f;
        public bool m_CantTakeDamage = false;
        public int m_DamageDelay = 30;
        public float m_Damage = 2.5f;

        //multipliers
        public float m_ShotSpeedMultiplyer = 1;
        public float m_ShotDelayMultiplyer = 1;
        public float m_ShotRangeMultiplyer = 1;
        public float m_DamageMultiplier = 1;
        public float m_PlayerSpeedMultiplyer = 1;


        //testing heart case image
        public Heart m_PlayerHeart = new Heart();
        public List<Heart> m_PlayerHearts;

        public Texture2D m_Texture;
        public Texture2D m_BulletTexture;
        public Vector2 m_PlayerPosition;
        
        public float m_PlayerRotation;
        public Vector2 m_PlayerOrigin;
        //public float bulletDelay;
        public Rectangle m_HitBox;
        //TopDownGame m_CurrentGame;
        Level m_CurrentLevel;
        public int m_CurrentRoom = 0;
        //const int m_RoomWidth = 960;
        //const int m_RoomHeight = 832;
        public int RoomIndex = 0;
        
        public List<Bullet> m_BulletList = new List<Bullet>();

        /// <name>Player::Player()</name>
        /// <summary>
        /// Constructor for a Player object. to keep track of which level the player is on the current level is passed to it and stored
        /// </summary>
        /// <param name="a_CurrentLevel"></param>
        /// <author>Douglas Wardle</author>
        /// <date></date>
        public Player(Level a_CurrentLevel)
        {
            m_CurrentLevel = a_CurrentLevel;
            m_Texture = null;
            m_PlayerSpeed = 5;
            m_PlayerPosition = new Vector2(480, 462);
            m_PlayerHearts = new List<Heart>();
            for(int i = 0; i < m_MaxHealth; i++)
            {
                m_PlayerHearts.Add(new Heart(new Vector2(40 + (i * 40), 16)));
            }
            //ChangeHeartTexture();
            Console.Write("Done");
        }

        /// <name>Player::LoadContent()</name>
        /// <summary>
        /// Loads all textures that the player needs. this includes the player texture, bullet texture, and Heart texture
        /// </summary>
        /// <param name="a_Content">content manager that contains all content for the game</param>
        /// <author>Douglas Wardle</author>
        /// <date></date>
        public void LoadContent(ContentManager a_Content)
        {
            m_Texture = a_Content.Load<Texture2D>("Player/batDoug5");
            m_BulletTexture = a_Content.Load<Texture2D>("Player/Bullet1");
            //m_PlayerHeart.LoadContent(a_Content);
            foreach(Heart h in m_PlayerHearts)
            {
                h.LoadContent(a_Content);
            }
            Console.Write("Done");
        }

        /// <name>Player::Draw()</name>
        /// <summary>
        /// this function draws all textures for the player character. this includes the player texture, bullet texture, and heart texture
        /// </summary>
        /// <param name="a_SpriteBatch">SpriteBatch object to allow for drawing of sprites</param>
        /// <author>Douglas Wardle</author>
        /// <date></date>
        public void Draw(SpriteBatch a_SpriteBatch)
        {
            m_PlayerOrigin.X = m_Texture.Width / 2;
            m_PlayerOrigin.Y = m_Texture.Height / 2;
            a_SpriteBatch.Draw(m_Texture, m_PlayerPosition, null, Color.White, m_PlayerRotation, m_PlayerOrigin, 1.0f, SpriteEffects.None, 0f);
            foreach(Bullet b in m_BulletList)
            {
                b.Draw(a_SpriteBatch);
            }
            foreach(Heart h in m_PlayerHearts)
            {
                h.Draw(a_SpriteBatch);
            }
            //m_PlayerHeart.Draw(a_SpriteBatch);
            //a_SpriteBatch.Draw(m_Texture, m_PlayerPosition, Color.White);
        }


        //new update for when using a Level object. pre summer work
        /// <name>Player::Update()</name>
        /// <summary>
        /// Function is called every time the player needs to update. this function allows for player movement, player rotation, shooting, Updating the players hearts,
        /// and updating the pplayers hitbox to move with the player. It will also keep the player from walking through walls
        /// </summary>
        /// <param name="a_GameTime"></param>
        /// <param name="a_CurrentLevel">The level that the player is currently on</param>
        /// <author>Douglas Wardle</author>
        /// <date></date>
        public void Update(GameTime a_GameTime, Level a_CurrentLevel)
        {
            KeyboardState keyState = Keyboard.GetState();
            if (keyState.IsKeyDown(Keys.W))
            {
                m_PlayerRotation = ((float)Math.PI / 2.0f) * 4;
                m_PlayerPosition.Y = m_PlayerPosition.Y - m_PlayerSpeed;
            }
            if (keyState.IsKeyDown(Keys.A))
            {
                m_PlayerRotation = ((float)Math.PI / 2.0f) * 3;
                m_PlayerPosition.X = m_PlayerPosition.X - m_PlayerSpeed;
            }
            if (keyState.IsKeyDown(Keys.S))
            {
                m_PlayerRotation = ((float)Math.PI / 2.0f) * 2;
                m_PlayerPosition.Y = m_PlayerPosition.Y + m_PlayerSpeed;
            }
            if (keyState.IsKeyDown(Keys.D))
            {
                m_PlayerRotation = ((float)Math.PI / 2.0f);
                m_PlayerPosition.X = m_PlayerPosition.X + m_PlayerSpeed;
            }

            if (keyState.IsKeyDown(Keys.Up))
            {
                m_PlayerRotation = ((float)Math.PI / 2.0f) * 4;
                Shoot(Keys.Up);

                //line below is for testing the switch from closed door to open door
                //m_CurrentGame.m_Door.m_IsDoorOpen = true;
            }
            else if (keyState.IsKeyDown(Keys.Right))
            {
                m_PlayerRotation = ((float)Math.PI / 2.0f);
                Shoot(Keys.Right);
            }
            else if (keyState.IsKeyDown(Keys.Left))
            {
                m_PlayerRotation = ((float)Math.PI / 2.0f) * 3;
                Shoot(Keys.Left);
            }
            else if (keyState.IsKeyDown(Keys.Down))
            {
                m_PlayerRotation = ((float)Math.PI / 2.0f) * 2;
                Shoot(Keys.Down);
            }
            //old update logic////////////////////////////////////////
            //List<Rooms> currentLevelRooms = m_CurrentLevel.GetRoomList();
            //UpdateBullet(currentLevelRooms[RoomIndex]);
            //////////////////////////////////////////////

            //448 is (the width of room wall - 64) / 2 or (960 - 64)/2
            //768 is (the length of room wall - 64) or (832 - 64)
            //896 is (the width of room wall - 64) or (960 - 64)
            //384 is (the length of room wall - 64) / 2 or (832 - 64) / 2


            //new player update logic
            Rooms currentRoom = a_CurrentLevel.GetCurrentRoom();
            UpdateBullet(currentRoom);
            int crp_X = currentRoom.GetRoomCoord_X();
            int crp_Y = currentRoom.GetRoomCoord_Y();
            //Left Wall
            if (m_PlayerPosition.X <= (64 + crp_X) + m_Texture.Width / 2)
            {
                if (currentRoom.DoorExists((int)Level.m_DoorPlacement.Left))
                {
                    int LeftDoor = currentRoom.FindDoor((int)Level.m_DoorPlacement.Left);
                    if (currentRoom.m_RoomDoors[LeftDoor].m_IsDoorOpen && m_PlayerPosition.Y >= crp_Y + 384 && m_PlayerPosition.Y <= crp_Y + 448)
                    {
                        if (m_PlayerPosition.Y >= crp_Y + 384 && m_PlayerPosition.X < crp_X + 64)
                        {
                            m_PlayerPosition.Y = (crp_Y + 384) + m_Texture.Height / 2;
                        }
                    }
                    else
                    {
                        m_PlayerPosition.X = (64 + crp_X) + m_Texture.Width / 2;
                    }
                }
                else
                {
                    m_PlayerPosition.X = (64 + crp_X) + m_Texture.Width / 2;
                }
            }

            //Right Wall
            if (m_PlayerPosition.X >= (832 + crp_X) + m_Texture.Width / 2)
            {
                if (currentRoom.DoorExists((int)Level.m_DoorPlacement.Right))
                {
                    int RightDoor = currentRoom.FindDoor((int)Level.m_DoorPlacement.Right);
                    if (currentRoom.m_RoomDoors[RightDoor].m_IsDoorOpen && m_PlayerPosition.Y >= crp_Y + 384 && m_PlayerPosition.Y <= crp_Y + 448)
                    {
                        if (m_PlayerPosition.Y >= crp_Y + 384 && m_PlayerPosition.X > crp_X + 864)
                        {
                            m_PlayerPosition.Y = (crp_Y + 384) + m_Texture.Height / 2;
                        }
                    }
                    else
                    {
                        m_PlayerPosition.X = (832 + crp_X) + m_Texture.Width / 2;
                    }
                }
                else
                {
                    m_PlayerPosition.X = (832 + crp_X) + m_Texture.Width / 2;
                }

            }

            //Top Wall
            if (m_PlayerPosition.Y <= (64 + crp_Y) + m_Texture.Height / 2)
            {
                if (currentRoom.DoorExists((int)Level.m_DoorPlacement.Up))
                {
                    int UpDoor = currentRoom.FindDoor((int)Level.m_DoorPlacement.Up);
                    if (currentRoom.m_RoomDoors[UpDoor].m_IsDoorOpen && m_PlayerPosition.X >= crp_X + 448 && m_PlayerPosition.X <= crp_X + 512)
                    {
                        if (m_PlayerPosition.X >= crp_X + 448 && m_PlayerPosition.Y < crp_Y + 64)
                        {
                            m_PlayerPosition.X = (crp_X + 448) + m_Texture.Width / 2;
                        }
                    }
                    else
                    {
                        m_PlayerPosition.Y = (64 + crp_Y) + m_Texture.Height / 2;
                    }
                }
                else
                {
                    m_PlayerPosition.Y = (64 + crp_Y) + m_Texture.Height / 2;
                }
            }

            //Down Wall
            if (m_PlayerPosition.Y >= (704 + crp_Y) + m_Texture.Height / 2)
            {
                if (currentRoom.DoorExists((int)Level.m_DoorPlacement.Down))
                {
                    int DownDoor = currentRoom.FindDoor((int)Level.m_DoorPlacement.Down);
                    if (currentRoom.m_RoomDoors[DownDoor].m_IsDoorOpen && m_PlayerPosition.X >= crp_X + 448 && m_PlayerPosition.X <= crp_X + 512)
                    {
                        if (m_PlayerPosition.X >= crp_X + 448 && m_PlayerPosition.Y > crp_Y + 736)
                        {
                            m_PlayerPosition.X = (crp_X + 448) + m_Texture.Width / 2;
                        }
                    }
                    else
                    {
                        m_PlayerPosition.Y = (704 + crp_Y) + m_Texture.Height / 2;
                    }
                }
                else
                {
                    m_PlayerPosition.Y = (704 + crp_Y) + m_Texture.Height / 2;
                }
            }
            m_HitBox = new Rectangle((int)m_PlayerPosition.X - 32, (int)m_PlayerPosition.Y - 32, m_Texture.Width, m_Texture.Height);


            //Move hearts to current room
            for(int i = 0; i < m_MaxHealth; i++)
            {
                m_PlayerHearts[i].SetPosition(new Vector2((currentRoom.GetRoomCoord_X() + 40) + (i * 40), currentRoom.GetRoomCoord_Y() + 16));
            }
        }

        

        /// <name>Player::Shoot()</name>
        /// <summary>
        /// Function for when the player tries to shoot the gun. allows player to shoot one bullet every time its shot delay hits zero.
        /// if the shot delay is zero, a new bullet is created at the and rotated to the direction that the player is shooting. this new bullet
        /// is then set to visible, added to the players bullet list, and then the shot delay is reset.
        /// </summary>
        /// <param name="a_ShotDirection">The direction that the player is shooting</param>
        /// <author>Douglas Wardle</author>
        /// <date></date>
        public void Shoot(Keys a_ShotDirection)
        {
            if(m_ShotDelay >= 0)
            {
                m_ShotDelay--;
            }

            if(m_ShotDelay <= 0)
            {
                Bullet NewBullet = new Bullet(m_BulletTexture);
                if(a_ShotDirection == Keys.Up)
                {
                    NewBullet.m_Position = new Vector2(m_PlayerPosition.X + 20, m_PlayerPosition.Y - 32);
                    NewBullet.m_BulletRotaion = m_PlayerRotation;
                    NewBullet.m_BulletDirection = Keys.Up;
                }
                else if(a_ShotDirection == Keys.Down)
                {
                    NewBullet.m_Position = new Vector2(m_PlayerPosition.X - 20, m_PlayerPosition.Y + 32);
                    NewBullet.m_BulletRotaion = m_PlayerRotation;
                    NewBullet.m_BulletDirection = Keys.Down;
                }
                else if(a_ShotDirection == Keys.Right)
                {
                    NewBullet.m_Position = new Vector2(m_PlayerPosition.X + 32, m_PlayerPosition.Y + 20);
                    NewBullet.m_BulletRotaion = m_PlayerRotation;
                    NewBullet.m_BulletDirection = Keys.Right;
                }
                else if(a_ShotDirection == Keys.Left)
                {
                    NewBullet.m_Position = new Vector2(m_PlayerPosition.X - 32, m_PlayerPosition.Y - 20);
                    NewBullet.m_BulletRotaion = m_PlayerRotation;
                    NewBullet.m_BulletDirection = Keys.Left;
                }

                NewBullet.m_IsVisible = true;
                m_BulletList.Add(NewBullet);

                if(m_ShotDelay <= 0)
                {
                    m_ShotDelay = 20;
                }

            }
        }

        /// <name>Player::</name>
        /// <summary>
        /// Function to update all bullets in the bullet list. this will be called every time Player::Update() is called. This allows the bullet to move
        /// in the direction that it was shot until it either hits the wall or reaches the players shot range.
        /// </summary>
        /// <param name="a_CurrentRoom">the current room that the player is in</param>
        /// <author>Douglas Wardle</author>
        /// <date></date>
        public void UpdateBullet(Rooms a_CurrentRoom)
        {
            foreach(Bullet b in m_BulletList)
            {
               // b.m_HitBox = new Rectangle((int)b.m_Position.X, (int)b.m_Position.Y, b.m_Texture.Width, b.m_Texture.Height);
                if(b.m_BulletDirection == Keys.Up)
                {
                    float shotStart = m_PlayerPosition.Y;
                    b.m_Position.Y = b.m_Position.Y - (m_ShotSpeedMultiplyer * b.GetSpeed());
                    b.m_HitBox = new Rectangle((int)b.m_Position.X - (b.m_Texture.Width/2), (int)b.m_Position.Y - (b.m_Texture.Height/2), b.m_Texture.Width, b.m_Texture.Height);
                    if (b.m_Position.Y <= shotStart - m_ShotRange || b.m_Position.Y <= a_CurrentRoom.m_RoomPosition.Y + 64)
                    {
                        b.m_IsVisible = false;
                    }
                    
                }

                if (b.m_BulletDirection == Keys.Down)
                {
                    float shotStart = m_PlayerPosition.Y;
                    b.m_Position.Y = b.m_Position.Y + (m_ShotSpeedMultiplyer * b.GetSpeed());
                    //b.m_HitBox = new Rectangle((int)b.m_Position.X, (int)b.m_Position.Y, b.m_Texture.Width, b.m_Texture.Height);
                    b.m_HitBox = new Rectangle((int)b.m_Position.X - (b.m_Texture.Width / 2), (int)b.m_Position.Y - (b.m_Texture.Height / 2), b.m_Texture.Width, b.m_Texture.Height);
                    if (b.m_Position.Y >= shotStart + m_ShotRange || b.m_Position.Y >= a_CurrentRoom.m_RoomPosition.Y + 768)
                    {
                        b.m_IsVisible = false;
                    }
                }

                if (b.m_BulletDirection == Keys.Left)
                {
                    float shotStart = m_PlayerPosition.X;
                    b.m_Position.X = b.m_Position.X - (m_ShotSpeedMultiplyer * b.GetSpeed());
                    //b.m_HitBox = new Rectangle((int)b.m_Position.X, (int)b.m_Position.Y, b.m_Texture.Height, b.m_Texture.Width);
                    b.m_HitBox = new Rectangle((int)b.m_Position.X - (b.m_Texture.Height / 2), (int)b.m_Position.Y - (b.m_Texture.Width / 2), b.m_Texture.Height, b.m_Texture.Width);
                    if (b.m_Position.X <= shotStart - m_ShotRange || b.m_Position.X <= a_CurrentRoom.m_RoomPosition.X + 64)
                    {
                        b.m_IsVisible = false;
                    }
                }

                if (b.m_BulletDirection == Keys.Right)
                {
                    float shotStart = m_PlayerPosition.X;
                    b.m_Position.X = b.m_Position.X + (m_ShotSpeedMultiplyer * b.GetSpeed());
                    //b.m_HitBox = new Rectangle((int)b.m_Position.X, (int)b.m_Position.Y, b.m_Texture.Height, b.m_Texture.Width);
                    b.m_HitBox = new Rectangle((int)b.m_Position.X - (b.m_Texture.Height / 2), (int)b.m_Position.Y - (b.m_Texture.Width / 2), b.m_Texture.Height, b.m_Texture.Width);
                    if (b.m_Position.X >= shotStart + m_ShotRange || b.m_Position.X >= a_CurrentRoom.m_RoomPosition.X + 896)
                    {
                        b.m_IsVisible = false;
                    }
                }
            }

            for(int i = 0; i < m_BulletList.Count; i++)
            {
                if(m_BulletList[i].m_IsVisible == false)
                {
                    m_BulletList.RemoveAt(i);
                    i--;
                }
            }
        }

        /// <name>Player::LowerHealth()</name>
        /// <summary>
        /// Lowers the players health by the ammount of damage passed to the function. then updates the players hearts
        /// to show the damage taken
        /// </summary>
        /// <param name="a_Damage">the amount of damage the player is taking</param>
        /// <author>Douglas Wardle</author>
        /// <date></date>
        public void LowerHealth(float a_Damage)
        {
            m_PlayerHealth -= a_Damage;
            ChangeHeartTexture();
        }

        /// <name>Player::IncreaseHealth()</name>
        /// <summary>
        /// Increases the players Maximum health by the amount passed to the function and fulls the players health
        /// </summary>
        /// <param name="a_Health"></param>
        /// <author>Douglas Wardle</author>
        /// <date></date>
        public void IncreaseHealth(float a_Health)
        {
            m_MaxHealth += a_Health;
            m_PlayerHealth = m_MaxHealth;
            //ChangeHeartTexture();
            
            //Heart addHeart = new Heart(new Vector2(40 + (m_PlayerHearts.Count * 40), 16));
            //m_PlayerHearts.Add(new Heart(new Vector2(40 + (m_PlayerHearts.Count * 40), 16)));
            
        }

        /// <name>Player::LowerSpeed()</name>
        /// <summary>
        /// Function lowers the players movement speed by the amount passed to the function
        /// </summary>
        /// <param name="a_SpeedDecrease">amount to decrease the players speed</param>
        /// <author>Douglas Wardle</author>
        /// <date></date>
        public void LowerPlayerSpeed(float a_SpeedDecrease)
        {
            m_PlayerSpeed -= a_SpeedDecrease;
        }

        /// <name>Player::IncreasePlayerSpeed()</name>
        /// <summary>
        /// Function increases the players movement speed by the amount passed to the function
        /// </summary>
        /// <param name="a_SpeedIncrease">amount to increase player speed</param>
        /// <author>Douglas Wardle</author>
        /// <date></date>
        public void IncreasePlayerSpeed(float a_SpeedIncrease)
        {
            m_PlayerSpeed += a_SpeedIncrease;
        }

        /// <name>Player::LowerShotDelay</name>
        /// <summary>
        /// Function lowers the players shot delay by the amount passed to the function
        /// </summary>
        /// <param name="a_DecreaseDelay">amount to decrease shot delay</param>
        /// <author>Douglas Wardle</author>
        /// <date></date>
        public void LowerShotDelay(float a_DecreaseDelay)
        {
            m_ShotDelay -= a_DecreaseDelay;
        }

        /// <name>Player::IncreaseShotDelay()</name>
        /// <summary>
        /// Function increases the players shot delay by the amount passed to the function
        /// </summary>
        /// <param name="a_IncreaseDelay">amount to increase shot delay</param>
        /// <author>Douglas Wardle</author>
        /// <date></date>
        public void IncreaseShotDelay(float a_IncreaseDelay)
        {
            m_ShotDelay += a_IncreaseDelay;
        }

        /// <name>Player::TakeDamage()</name>
        /// <summary>
        /// First checks if the player can take damage. if the player can take damage lower the player health by the amount of damage passed to the function.
        /// if the player cannot take damage, decrease the damage delay
        /// </summary>
        /// <param name="a_Damage"></param>
        /// <author>Douglas Wardle</author>
        /// <date></date>
        public void TakeDamage(float a_Damage)
        {
            if(this.m_CantTakeDamage == false)
            {
                LowerHealth(a_Damage);
                //m_PlayerHealth -= a_Damage;
                m_CantTakeDamage = true;
            }
            else
            {
                m_DamageDelay--;
            }
            //if(m_PlayerHealth <= 0)
            //{
            //    System.Environment.Exit(0);
            //}

            if(m_DamageDelay <= 0)
            {
                m_CantTakeDamage = false;
                m_DamageDelay = 30;
            }
        }

        /// <name>Player::IncreaseShotSpeed()</name>
        /// <summary>
        /// Increases the shot speed multiplier for the player to increase the speed at which bullets travel
        /// </summary>
        /// <param name="a_Multiplier">amount to increase the shot speed multiplier</param>
        /// <author>Douglas Wardle</author>
        /// <date></date>
        public void IncreaseShotSpeed(float a_Multiplier)
        {
            m_ShotSpeedMultiplyer = m_ShotSpeedMultiplyer + a_Multiplier;
            
        }

        /// <name>Player::ChangeHeartTexture</name>
        /// <summary>
        /// Function changes the players Heart textures to show the amount of health the player currently has
        /// </summary>
        /// <author>Douglas Wardle</author>
        /// <date></date>
        public void ChangeHeartTexture()
        {
            for(int i = 0; i < m_MaxHealth; i++)
            {
                if(i + 1 > m_PlayerHealth)
                {
                    
                    if((i+1) - m_PlayerHealth == .5)
                    {
                        m_PlayerHearts[i].SetTextureType(1);
                    }
                    else
                    {
                        m_PlayerHearts[i].SetTextureType(2);
                    }
                }
                else if(m_PlayerHealth == m_MaxHealth)
                {
                    m_PlayerHearts[i].SetTextureType(0);
                }
            }
        }

        /// <name>Player::GetHearts()</name>
        /// <summary>
        /// Accesses the players Heart list
        /// </summary>
        /// <returns>returns a list containing the players hearts</returns>
        /// <author>Douglas Wardle</author>
        /// <date></date>
        public List<Heart> GetHearts()
        {
            return m_PlayerHearts;
        }

        /// <name>Player::SetPosition()</name>
        /// <summary>
        /// Accepts a Vector2 and assigns it to the players position
        /// </summary>
        /// <param name="a_Position">Vector2 containing the new player position</param>
        /// <author>Douglas Wardle</author>
        /// <date></date>
        public void SetPosition(Vector2 a_Position)
        {
            m_PlayerPosition = a_Position;
        }

        /// <name>Player::GetHealth()</name>
        /// <summary>
        /// Accesses the players current health
        /// </summary>
        /// <returns>floating point value for the players health</returns>
        /// <author>Douglas Wardle</author>
        /// <date></date>
        public float GetHealth()
        {
            return m_PlayerHealth;
        }
    }
}