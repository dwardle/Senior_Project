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

        /// <name>Camera::Camera()</name>
        /// <summary>
        /// Constructor for a Camera object. accepts a Viewport object to set the default viewport at the start of the game
        /// </summary>
        /// <param name="a_View">Viewport object containing the default viewport for the game</param>
        /// <author>Douglas Wardle</author>
        /// <date>10/4/2017</date>
        public Camera(Viewport a_View)
        {
            m_View = a_View;
            m_Center = new Vector2(0, 0);
            m_Transform = Matrix.CreateScale(new Vector3(1, 1, 0)) * Matrix.CreateTranslation(new Vector3(-m_Center.X, -m_Center.Y, 0));
        }

        /// <name>Camera::Update()</name>
        /// <summary>
        /// Function is called whenever the camera needs to be updated. It will move the camera to the coordiates provided
        /// </summary>
        /// <param name="a_GameTime"></param>
        /// <param name="a_NewX">new camera coordinate X</param>
        /// <param name="a_NewY">new camera coordinate Y</param>
        /// <author>Douglas Wardle</author>
        /// <date>10/4/2017</date>
        public void Update(GameTime a_GameTime, int a_NewX, int a_NewY)
        {
            m_Center = new Vector2(a_NewX, a_NewY);
            m_Transform = Matrix.CreateScale(new Vector3(1, 1, 0)) * Matrix.CreateTranslation(new Vector3(-m_Center.X, -m_Center.Y, 0));
        }

    }
}
