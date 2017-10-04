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
    public class Enemy
    {
        //enemy size 64 x 56
        public Texture2D m_Texture;
        public Vector2 m_Position;
        public bool m_IsAlive = true;
        public bool m_IsActive = false;
        public float m_Health;
        public float m_Speed;
        public float m_Damage = 0.5f;
        public float m_Rotation = 0;
        public Vector2 m_EnemyOrigin = new Vector2();
        public Rectangle m_HitBox = new Rectangle();
        public int m_MoveDelay = 0;
        public int m_MoveCount = 10;
        public int m_Type;

        
        /// <name>Enemy::Enemy()</name>
        /// <summary>
        /// Basic constructor for Enemy object
        /// </summary>
        /// <author>Douglas Wardle</author>
        /// <date>10/4/2017</date>
        public Enemy()
        {
            m_Texture = null;
            m_Speed = 2;
            m_Health = 10;
            m_IsAlive = true;
        }

        
        /// <name>Enemy::Draw()</name>
        /// <summary>
        /// Function is called to draw an Enemy object to the game window
        /// </summary>
        /// <param name="a_SpriteBatch">A SpriteBatch object to allow for drawing of sprites</param>
        /// <author>Douglas Wardle</author>
        /// <date>10/4/2017</date>
        public void Draw(SpriteBatch a_SpriteBatch)
        {
            m_EnemyOrigin.X = m_Texture.Width / 2;
            m_EnemyOrigin.Y = m_Texture.Height / 2;
            a_SpriteBatch.Draw(m_Texture, m_Position, null, Color.White, m_Rotation, m_EnemyOrigin, 1.0f, SpriteEffects.None, 0f);
        }

        
        /// <name>Enemy::SetTexture()</name>
        /// <summary>
        /// Function accepts a Texture2D object and sets the m_Texture to that object
        /// </summary>
        /// <param name="a_Texture">A Texture2D object containing the texture to be set</param>
        /// <author>Douglas Wardle</author>
        /// <date>10/4/2017</date>
        public void SetTexture(Texture2D a_Texture)
        {
            m_Texture = a_Texture;
        }

        /// <name>Enemy::SetPosition()</name>
        /// <summary>
        /// Sets the position of the enemy object in game window cooridinates
        /// </summary>
        /// <param name="a_Xpos">new x coordinate</param>
        /// <param name="a_Ypos">new y coordinate</param>
        /// <author>Douglas Wardle</author>
        /// <date>10/4/2017</date>
        public void SetPosition(float a_Xpos, float a_Ypos)
        {
            m_Position.X = a_Xpos;
            m_Position.Y = a_Ypos;
        }

        /// <name>Enemy::SetRotation()</name>
        /// <summary>
        /// Function sets the rotation of the object
        /// </summary>
        /// <param name="a_Direction">Char value to represent the current rotation of an object</param>
        /// <author>Douglas Wardle</author>
        /// <date>10/4/2017</date>
        public void SetRotation(char a_Direction)
        {
            if (a_Direction == 'W')
            {
                m_Rotation = ((float)Math.PI / 2.0f) * 4;
                
            }
            else if (a_Direction == 'A')
            {
               
                m_Rotation = ((float)Math.PI / 2.0f) * 3;
            }
            else if (a_Direction == 'S')
            {
                m_Rotation = ((float)Math.PI / 2.0f) * 2;
                
            }
            else if (a_Direction == 'D')
            {
                
                m_Rotation = ((float)Math.PI / 2.0f);
                
            }
        }

        /// <name>Enemy::SetIsActive()</name>
        /// <summary>
        /// Set the m_IsActive variable for the object. accepts a bool
        /// </summary>
        /// <param name="a_IsActive">bool to set the m_IsActive too</param>
        /// <author>Douglas Wardle</author>
        /// <date>10/4/2017</date>
        public void SetIsActive(bool a_IsActive)
        {
            m_IsActive = a_IsActive;
        }

        /// <name>Enemy::SetMoveDelay()</name>
        /// <summary>
        /// Sets the enemy move delay
        /// </summary>
        /// <param name="a_MoveDelay">integer value that m_MoveDelay will be set too</param>
        /// <author>Douglas Wardle</author>
        /// <date>10/4/2017</date>
        public void SetMoveDelay(int a_MoveDelay)
        {
            m_MoveDelay = a_MoveDelay;
        }

        /// <name>Enemy::GetMoveDelay()</name>
        /// <summary>
        /// Function is used to access the enemy m_MoveDelay variable
        /// </summary>
        /// <returns>integer containing the enemy move delay</returns>
        /// <author>Douglas Wardle</author>
        /// <date>10/4/2017</date>
        public int GetMoveDelay()
        {
            return m_MoveDelay;
        }

        /// <name>Enemy::MoveToPlayer()</name>
        /// <summary>
        /// Function is called whenever the enemies are supposed to move. the enemy will move toward the player and rotate according to the
        /// direction it is moving
        /// </summary>
        /// <param name="a_MainPlayer">the current player the enemy is trying to move toward</param>
        /// <param name="a_RoomEnemies">list of enemies that are trying to move to the player</param>
        /// <author>Douglas Wardle</author>
        /// <date>10/4/2017</date>
        public void MoveToPlayer(Player a_MainPlayer, List<Enemy> a_RoomEnemies)
        {
            if (a_MainPlayer.m_PlayerPosition.X > this.m_Position.X)
            {
                this.SetRotation('D'); 

                
                this.m_HitBox = new Rectangle((int)this.m_Position.X - (this.m_Texture.Height / 2),
                    (int)this.m_Position.Y - (this.m_Texture.Width / 2), this.m_Texture.Height, this.m_Texture.Width);
                m_Position.X = m_Position.X + m_Speed; 
            }

            //move down, toward player
            if (a_MainPlayer.m_PlayerPosition.Y > this.m_Position.Y)
            {
                this.SetRotation('S'); 
                this.m_HitBox = new Rectangle((int)this.m_Position.X - (this.m_Texture.Width / 2),
                    (int)this.m_Position.Y - (this.m_Texture.Height / 2), this.m_Texture.Width, this.m_Texture.Height);
                m_Position.Y = m_Position.Y + m_Speed;
            }

            //move left toward player,
            if (a_MainPlayer.m_PlayerPosition.X < this.m_Position.X)
            {
                this.SetRotation('A');
                this.m_HitBox = new Rectangle((int)this.m_Position.X - (this.m_Texture.Height / 2),
                    (int)this.m_Position.Y - (this.m_Texture.Width / 2), this.m_Texture.Height, this.m_Texture.Width);
                m_Position.X = m_Position.X - m_Speed;
            }

            //move up, toward player
            if (a_MainPlayer.m_PlayerPosition.Y < this.m_Position.Y)
            {
                this.SetRotation('W');
                this.m_HitBox = new Rectangle((int)this.m_Position.X - (this.m_Texture.Width / 2),
                    (int)this.m_Position.Y - (this.m_Texture.Height / 2), this.m_Texture.Width, this.m_Texture.Height);
                m_Position.Y = m_Position.Y - m_Speed;
            }
        }

        
        /// <name>Enemy::TakeDamage()</name>
        /// <summary>
        /// Function called whenever the enemy is supposed to take damage. will lower the enemy health by the amount passed to the function
        /// if the enemy health drops below or equal to zero, sets the enemy m_IsAlve variable to false
        /// </summary>
        /// <param name="a_Damage">The amount that enemy health is to be lowered</param>
        /// <author>Douglas Wardle</author>
        /// <date>10/4/2017</date>
        public void TakeDamage(float a_Damage)
        {
            m_Health -= a_Damage;
            if (m_Health <= 0)
            {
                this.m_IsAlive = false;
            }
        }

        
        /// <name>Enemy::SetHitBox()</name>
        /// <summary>
        /// Funtion is called whenever the enemy hitbox needs to be changed. this function will accept a char value representing the rotation
        /// and will change the hitbox of the enemy to match the rotation and new location of the enemy
        /// </summary>
        /// <param name="a_Rotation">Char value to represent the enemy rotation</param>
        /// <author>Douglas Wardle</author>
        /// <date>10/4/2017</date>
        public void SetHitbox(char a_Rotation)
        {
            if (a_Rotation == 'D')
            {
                this.m_HitBox = new Rectangle((int)this.m_Position.X - (this.m_Texture.Height / 2),
                    (int)this.m_Position.Y - (this.m_Texture.Width / 2), this.m_Texture.Height, this.m_Texture.Width);
            }
            else if (a_Rotation == 'S')
            {
                this.m_HitBox = new Rectangle((int)this.m_Position.X - (this.m_Texture.Width / 2),
                    (int)this.m_Position.Y - (this.m_Texture.Height / 2), this.m_Texture.Width, this.m_Texture.Height);
            }
            else if (a_Rotation == 'A')
            {
                this.m_HitBox = new Rectangle((int)this.m_Position.X - (this.m_Texture.Height / 2),
                    (int)this.m_Position.Y - (this.m_Texture.Width / 2), this.m_Texture.Height, this.m_Texture.Width);
            }
            else if(a_Rotation == 'W')
            {
                this.m_HitBox = new Rectangle((int)this.m_Position.X - (this.m_Texture.Width / 2),
                    (int)this.m_Position.Y - (this.m_Texture.Height / 2), this.m_Texture.Width, this.m_Texture.Height);
            }
        }

        
        /// <name>Enemy::SetIsAlive()</name>
        /// <summary>
        /// Sets the enemy m_IsAlive value to the bool value passed to the function
        /// </summary>
        /// <param name="a_IsAlive">bool value to set m_IsAlive too</param>
        /// <author>Douglas Wardle</author>
        /// <date>10/4/2017</date>
        public void SetIsAlive(bool a_IsAlive)
        {
            m_IsAlive = a_IsAlive;
        }

        /// <name>Enemy::MultiplyHealth()</name>
        /// <summary>
        /// Function accepts a floating point value to multiply the enemy current health by
        /// </summary>
        /// <param name="a_Multiplier">the amount that enemy health will be multiplied by</param>
        /// <author>Douglas Wardle</author>
        /// <date>10/4/2017</date>
        public void MultiplyHealth(float a_Multiplier)
        {
            m_Health = m_Health + (m_Health * a_Multiplier);
        }


        /// <name>Enemy::MultiplySpeed()</name>
        /// <summary>
        /// Function accepts a floating point value to multiply the enemy current speed by
        /// </summary>
        /// <param name="a_Multiplier">the amount the enemy speed will be multiplied by</param>
        /// <author>Douglas Wardle</author>
        /// <date>10/4/2017</date>
        public void MultiplySpeed(float a_Multiplier)
        {
            m_Speed = m_Speed + (m_Speed * a_Multiplier);
        }

        
        /// <name>Enemy::MultiplyDamage()</name>
        /// <summary>
        /// Function accepts a floating point value to multiply the enemy current damage by. multiplyer will be multiplied by .5 because
        /// player health must take damage in multiples of .5 
        /// </summary>
        /// <param name="a_Multiplyer">the amount the enemy damage will be multiplied by</param>
        /// <author>Douglas Wardle</author>
        /// <date>10/4/2017</date>
        public void MultiplyDamage(float a_Multiplyer)
        {
            m_Damage = m_Damage + (m_Damage * (a_Multiplyer * .5f));
        }
    }
}
