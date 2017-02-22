using System;

namespace Senior_Project
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (TopDownGame game = new TopDownGame())
            {
                game.Run();
            }
        }
    }
#endif
}

