using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    public class Boss : Enemy
    {
        public Vector2 m_MoveTo;
        public bool m_CanMove;
        public bool m_IsMoving;
        public Rectangle m_HitBox2;

        /// <name>Boss::Boss()</name>
        /// <summary>
        /// Basic contructor for a boss object
        /// </summary>
        /// <author>Douglas Wardle</author>
        /// <date></date>
        public Boss()
        {
            m_CanMove = false;
            m_IsMoving = false;
            m_MoveDelay = 20;
            m_MoveTo = new Vector2(0, 0);
            m_EnemyOrigin.X = 96;
            m_EnemyOrigin.Y = 96;
            m_Speed = 2.5f;
            m_Health = 80;
        }

        /// <name>Boss::Boss()</name>
        /// <summary>
        /// Basic constructor for a boss object. accepts an integer to set the m_Type value to
        /// </summary>
        /// <param name="a_Type">integer representing the enemy type</param>
        /// <author>Douglas Wardle</author>
        /// <date></date>
        public Boss(int a_Type)
        {
            m_CanMove = false;
            m_IsMoving = false;
            m_MoveDelay = 20;
            m_MoveTo = new Vector2(0, 0);
            m_EnemyOrigin.X = 96;
            m_EnemyOrigin.Y = 96;
            m_Speed = 5;
            m_Type = a_Type;
            m_HitBox2 = new Rectangle(0, 0, 0, 0);
            m_Health = 10;
        }

        /// <name>Boss::LoadContent()</name>
        /// <summary>
        /// Sets the boss texture and sets the enemy origin. called whenever a boss object needs to load its texture
        /// </summary>
        /// <param name="a_Content">Content manager containing the texture that boss needs to be set to</param>
        /// <author>Douglas Wardle</author>
        /// <date></date>
        public void LoadContent(ContentManager a_Content)
        {

            m_Texture = a_Content.Load<Texture2D>("Enemies/Boss1-2");
            m_EnemyOrigin.X = (m_Texture.Width / 2);
            m_EnemyOrigin.Y = (m_Texture.Height / 2);
        }

        /// <name>Boss::Draw2()</name>
        /// <summary>
        /// Draws the boss to the game window when called
        /// </summary>
        /// <param name="a_SpriteBatch">SpriteBatch object that allows for drawing of sprites</param
        /// <author>Douglas Wardle</author>
        /// <date></date>
        public void Draw2(SpriteBatch a_SpriteBatch)
        {
            a_SpriteBatch.Draw(m_Texture, m_Position, Color.White);
        }

        /// <name>Boss::MoveToPlayer1()</name>
        /// <summary>
        /// Function moves the boss at boss speed toward whatever the Vector2 m_MoveTo is currently set to. Typically m_MoveTo is
        /// set to the players current position. If this is called everytime the game updates, it will cause the boss to chase
        /// the player constantly
        /// </summary>
        /// <author>Douglas Wardle</author>
        /// <date></date>
        public void MoveToPlayer1()
        {
            if (m_Position.X < m_MoveTo.X)
            {
                m_Position.X = m_Position.X + m_Speed;

            }
            if (m_Position.Y < m_MoveTo.Y)
            {
                m_Position.Y = m_Position.Y + m_Speed;
            }
            if (m_Position.X > m_MoveTo.X)
            {
                m_Position.X = m_Position.X - m_Speed;

            }
            if (m_Position.Y > m_MoveTo.Y)
            {
                m_Position.Y = m_Position.Y - m_Speed;
            }
        }


        ///Not currently using this function
        /// <name>Boss::</name>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="a_MainPlayer"></param>
        /// <author>Douglas Wardle</author>
        /// <date></date>
        //public void MoveToPlayer(Player a_MainPlayer)
        //{
        //    //follow the player
        //    float moveToX = a_MainPlayer.m_PlayerPosition.X; //+ (a_MainPlayer.m_Texture.Width/2);  //(a_MainPlayer.m_Texture.Width);
        //    float moveToY = a_MainPlayer.m_PlayerPosition.Y; //+ (a_MainPlayer.m_Texture.Height / 2);  //(a_MainPlayer.m_Texture.Height);

        //    if (m_Position.X < moveToX)
        //    {
        //        m_Position.X = m_Position.X + m_Speed;

        //    }
        //    if(m_Position.Y < moveToY)
        //    {
        //        m_Position.Y = m_Position.Y + m_Speed;
        //    }
        //    if (m_Position.X > moveToX)
        //    {
        //        m_Position.X = m_Position.X - m_Speed;

        //    }
        //    if (m_Position.Y > moveToY)
        //    {
        //        m_Position.Y = m_Position.Y - m_Speed;
        //    }

        //}

        /// <name>Boss::MoveToPlayer</name>
        /// <summary>
        /// Function will move the enemy toward whatever the Vector2 m_MoveTo is currently set to. when it reaches that location it will call
        /// SetIsMoving() and SetCanMove() changing both to false. If this function is called every time the game updates it will cause the enemy
        /// to move to the location then stop and wait for the next time it can move
        /// </summary>
        /// <author>Douglas Wardle</author>
        /// <date></date>
        public void MoveToPlayer()
        {
            //boss movement if i want the boss to move toward the players last position then stop and wait
            if (m_Position.X < m_MoveTo.X)
            {
                m_Position.X = m_Position.X + m_Speed;
                if (m_Position.Y < m_MoveTo.Y)
                {
                    //m_Position.X = m_Position.X + m_Speed;
                    m_Position.Y = m_Position.Y + m_Speed;
                }
                if (m_Position.Y > m_MoveTo.Y)
                {
                    //m_Position.X = m_Position.X + m_Speed;
                    m_Position.Y = m_Position.Y - m_Speed;
                }
                if (m_Position.X >= m_MoveTo.X)
                {
                    SetIsMoving(false);
                    SetCanMove(false);
                }
            }
            if (m_Position.X > m_MoveTo.X)
            {
                m_Position.X = m_Position.X - m_Speed;
                if (m_Position.Y < m_MoveTo.Y)
                {
                    //m_Position.X = m_Position.X - m_Speed;
                    m_Position.Y = m_Position.Y + m_Speed;
                }
                if (m_Position.Y > m_MoveTo.Y)
                {
                   // m_Position.X = m_Position.X - m_Speed;
                    m_Position.Y = m_Position.Y - m_Speed;
                }
                

                if (m_Position.X <= m_MoveTo.X)
                {
                    SetIsMoving(false);
                    SetCanMove(false);
                }
                
            }
        }

        /// <name>Boss::GetBoss()</name>
        /// <summary>
        /// Function returns the instance of this object
        /// </summary>
        /// <returns>The object that called the function</returns>
        /// <author>Douglas Wardle</author>
        /// <date></date>
        public Boss GetBoss()
        {
            return this;
        }

        /// <name>Boss::SetCanMove</name>
        /// <summary>
        /// Function sets the m_CanMove value to the opposite of whatever it is
        /// </summary>
        /// <author>Douglas Wardle</author>
        /// <date></date>
        public void SetCanMove()
        {
            m_CanMove = !m_CanMove;
        }

        /// <name>Boss::SetCanMove()</name>
        /// <summary>
        /// Function sets the m_CanMove value equal to a_CanMove
        /// </summary>
        /// <param name="a_CanMove">bool value that should be true if the enemy is allowed to move and false otherwise</param>
        /// <author>Douglas Wardle</author>
        /// <date></date>
        public void SetCanMove(bool a_CanMove)
        {
            m_CanMove = a_CanMove;
        }

        //need to fix the movement on this function so when player enters the room there is a delay before the boss moves

        /// <name>Boss::CanMove()</name>
        /// <summary>
        /// Function checks if the boss object is allowed to move. if the boss is not allowed to move it will decrement the move delay by 1
        /// if the enemy m_CanMove  value is currently set to false and it is not moving and move delay is less than or equal to 0, function will
        /// reset the delay and change the m_CanMove value to true.
        /// </summary>
        /// <returns>true if the enemy can move, false otherwise</returns>
        /// <author>Douglas Wardle</author>
        /// <date></date>
        public bool CanMove()
        {
            if(m_CanMove == false && m_IsMoving == false && m_MoveDelay <= 0)
            {
                m_MoveDelay = 20;
                SetCanMove();
            }
            else if(m_CanMove == false && m_MoveDelay >= 0)
            {
                m_MoveDelay--;
            }
            return m_CanMove;
        }

        /// <name>Boss::SetMoveLocation()</name>
        /// <summary>
        /// Sets the objects m_MoveTo location to a_MoveTo
        /// </summary>
        /// <param name="a_MoveTo">Vector2 object containing the new location to move to</param>
        /// <author>Douglas Wardle</author>
        /// <date></date>
        public void SetMoveLocation(Vector2 a_MoveTo)
        {
            m_MoveTo = a_MoveTo;
        }

        /// <name>Boss::GetTextureOriginX()</name>
        /// <summary>
        /// Returns the X value of the enemy origin
        /// </summary>
        /// <returns>returns a floating point number for the enemy origin at X</returns>
        /// <author>Douglas Wardle</author>
        /// <date></date>
        public float GetTextureOriginX()
        {
            return m_EnemyOrigin.X;
        }

        /// <name>Boss::GetTextureOriginY()</name>
        /// <summary>
        /// Returns the Y value of the enemy origin
        /// </summary>
        /// <returns>a floating point number with the enemy origin at Y</returns>
        /// <author>Douglas Wardle</author>
        /// <date></date>
        public float GetTextureOriginY()
        {
            return m_EnemyOrigin.Y;
        }

        /// <name>Boss::SetIsMoving()</name>
        /// <summary>
        /// Sets the objects m_IsMoving value to a_IsMoving
        /// </summary>
        /// <param name="a_IsMoving">bool Value, pass true if the enemy is currently moving and false if not</param>
        /// <author>Douglas Wardle</author>
        /// <date></date>
        public void SetIsMoving(bool a_IsMoving)
        {
            m_IsMoving = a_IsMoving;
        }

        /// <name>Boss::IsMoving()</name>
        /// <summary>
        /// Function accesses the m_IsMoving bool value
        /// </summary>
        /// <returns>true if the enemy is currently moving, false otherwise</returns>
        /// <author>Douglas Wardle</author>
        /// <date></date>
        public bool IsMoving()
        {
            return m_IsMoving;
        }

        /// <name>Boss::SetHitBox()</name>
        /// <summary>
        /// Function sets the objects 2 hitboxes to correspond to the current position of the object
        /// </summary>
        /// <author>Douglas Wardle</author>
        /// <date></date>
        public void SetHitBox()
        {
            //wide hitbox
            this.m_HitBox = new Rectangle((int)this.m_Position.X - ((this.m_Texture.Width / 2) - 16),
                    (int)this.m_Position.Y - (this.m_Texture.Height / 3), this.m_Texture.Width - 32, this.m_Texture.Height - 64);

            //long hitbox
            this.m_HitBox2 = new Rectangle((int)this.m_Position.X - (this.m_Texture.Width / 3),
                    (int)this.m_Position.Y - ((this.m_Texture.Height / 2) - 16), this.m_Texture.Width - 64, this.m_Texture.Height - 32);
        }

        /// <name>Boss::DeleteHitbox()</name>
        /// <summary>
        /// Function sets the objects hitboxes to empty rectangles. called when the enemy is dead or its hitboxes are not needed
        /// </summary>
        /// <author>Douglas Wardle</author>
        /// <date></date>
        public void DeleteHitbox()
        {
            m_HitBox = new Rectangle();
            m_HitBox2 = new Rectangle();
        }

        /// <name>Boss::Update1()</name>
        /// <summary>
        /// Function is called whenever the object needs to be updated. this includes movement, taking damange, and being defeated
        /// </summary>
        /// <param name="a_MainPlayer">The current player object for the game</param>
        /// <author>Douglas Wardle</author>
        /// <date></date>
        public void Update1(Player a_MainPlayer)
        {
            if (m_IsAlive)
            {
                SetMoveLocation(a_MainPlayer.m_PlayerPosition);
                SetHitBox();
                if (CanMove())
                {
                    MoveToPlayer1();
                }

                if (a_MainPlayer.m_HitBox.Intersects(m_HitBox) || a_MainPlayer.m_HitBox.Intersects(m_HitBox2))
                {
                    a_MainPlayer.TakeDamage(m_Damage);
                }
            }
            else
            {
                DeleteHitbox();
            }
        }

        /// <name>Boss::BossDefeated()</name>
        /// <summary>
        /// Function is called to check if the boss has been defeated or not. it will return the opposite of the objects
        /// m_IsAlive value
        /// </summary>
        /// <returns>true if m_IsAlive is false and false if m_IsAlive is true</returns>
        /// <author>Douglas Wardle</author>
        /// <date></date>
        public bool BossDefeated()
        {
            return !m_IsAlive;
        }
    }
}
