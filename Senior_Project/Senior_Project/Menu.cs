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
    public class Menu
    {
        public Texture2D m_Texture;
        public Vector2 m_Position;
        public MenuItem m_StartOption;
        public int m_MenuType;
        public List<MenuItem> m_MenuOptions = new List<MenuItem>();

        public Menu()
        {
            //m_StartOption = new MenuItem();
            m_Position.X = 0;
            m_Position.Y = 0;
            m_MenuType = 0;
            //m_MenuOptions.Add(new MenuItem(0));
        }


        public void LoadContent(ContentManager a_Content)
        {
            if(m_MenuType == 0)
            {
                m_Texture = a_Content.Load<Texture2D>("Menus/Menu");
            }
            else if(m_MenuType == 1)
            {
                m_Texture = a_Content.Load<Texture2D>("Menus/PauseMenu");
            }
            else if(m_MenuType == 2)
            {
                m_Texture = a_Content.Load<Texture2D>("Menus/LevelSize");
            }
            else if(m_MenuType == 3)
            {
                m_Texture = a_Content.Load<Texture2D>("Menus/WinMenu");
            }
            else if(m_MenuType == 4)
            {
                m_Texture = a_Content.Load<Texture2D>("Menus/GameOverMenu");
            }
            //AddMenuItem(a_Content, 0);
            foreach(MenuItem mi in m_MenuOptions)
            {
                mi.LoadContent(a_Content);
            }
            //m_StartOption.LoadContent(a_Content);
        }

        public void Draw(SpriteBatch a_SpriteBatch)
        {
            //a_SpriteBatch.Begin();
            a_SpriteBatch.Draw(m_Texture, m_Position, Color.White);
            foreach (MenuItem mi in m_MenuOptions)
            {
                mi.Draw(a_SpriteBatch);
            }
            //m_StartOption.Draw(a_SpriteBatch);
            //a_SpriteBatch.End();
        }

        public void AddMenuItem(ContentManager a_Content, int a_Option)
        {
            MenuItem tempItem = new MenuItem(a_Option);
            m_MenuOptions.Add(tempItem);
        }

        public void SetOptionPositions()
        {
            Vector2 newPosition = new Vector2(m_Position.X + 350, m_Position.Y + 350);
            for(int i = 0; i < m_MenuOptions.Count; i++)
            {
                m_MenuOptions[i].SetPosition(newPosition);
                newPosition.Y = newPosition.Y + 192;
            }
        }

        public void SetOptionPositions(float a_PositionX, float a_PositionY)
        {
            Vector2 newPosition = new Vector2(m_Position.X + a_PositionX, m_Position.Y + a_PositionY);
            for (int i = 0; i < m_MenuOptions.Count; i++)
            {
                m_MenuOptions[i].SetPosition(newPosition);
                newPosition.Y = newPosition.Y + 192;
            }
        }

        public void SetOptionPositions2()
        {
            Vector2 newPosition = new Vector2(m_Position.X + 300, m_Position.Y + 500);
            for (int i = 0; i < m_MenuOptions.Count; i++)
            {
                m_MenuOptions[i].SetPosition(newPosition);
                newPosition.X = newPosition.X + 128;
            }
        }

        public void SetMenuType(int a_MenuType)
        {
            m_MenuType = a_MenuType;
        }
    }
}
