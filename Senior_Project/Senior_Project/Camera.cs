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
    class Camera
    {
        public Matrix m_Transform;
        public Viewport m_View;
        Vector2 m_Center;

        public Camera(Viewport a_View)
        {
            m_View = a_View;
            m_Center = new Vector2(0, 0);
            m_Transform = Matrix.CreateScale(new Vector3(1, 1, 0)) * Matrix.CreateTranslation(new Vector3(-m_Center.X, -m_Center.Y, 0));
        }

        public void Update(GameTime a_GameTime, int a_NewX, int a_NewY)
        {
            m_Center = new Vector2(a_NewX, a_NewY);
            m_Transform = Matrix.CreateScale(new Vector3(1, 1, 0)) * Matrix.CreateTranslation(new Vector3(-m_Center.X, -m_Center.Y, 0));
        }

    }
}
