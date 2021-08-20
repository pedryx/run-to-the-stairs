using System;
using System.Threading;

namespace GameLib
{
    public class Game
    {
        /// <summary>
        /// Date and time of prevous call of <see cref="CalcDeltaTime(float)"/>.
        /// </summary>
        private DateTime last_;
        /// <summary>
        /// Determine if next call of <see cref="Render(float)"/> should be skipped.
        /// </summary>
        private bool skipRender_;

        /// <summary>
        /// Calculate time between last and current call of <see cref="CalcDeltaTime(float)"/>
        /// and put current thread to sleep if deltaTime is too small or skip next
        /// <see cref="Render(float)"/> call if is it too big. The first call of this method
        /// returns invalid result!.
        /// </summary>
        /// 
        /// <param name="fps">Target framerate.</param>
        /// 
        /// <returns>
        /// Ellapsed time between last and current call of <see cref="CalcDeltaTime(float)"/>.
        /// If this is a first call of this method than the returned delta time is invalid!
        /// </returns>
        public float CalcDeltaTime(float fps = 60)
        {
            if (fps == 0)
                throw new ArgumentException("Argument cannot be zero.", nameof(fps));

            DateTime current = DateTime.Now;
            float deltaTime = (float)(current - last_).TotalSeconds;
            float desiredDeltaTime = 1 / fps;

            if (deltaTime < desiredDeltaTime)
            {
                Thread.Sleep((int)((desiredDeltaTime - deltaTime) * 1000));
                current = DateTime.Now;
                deltaTime = (float)(current - last_).TotalSeconds;
            }
            else if (deltaTime > desiredDeltaTime)
                skipRender_ = true;

            last_ = current;
            return deltaTime;
        }

        /// <summary>
        /// Update game logic.
        /// </summary>
        /// <param name="deltaTime">Time ellapsed between previous and current frame.</param>
        public void Update(float deltaTime)
        {

        }

        /// <summary>
        /// Render game. This call could not always happen see description of
        /// <see cref="CalcDeltaTime(float)"/> for more info.
        /// </summary>
        /// <param name="deltaTime">Time ellapsed between previous and current frame.</param>
        public void Render(float deltaTime)
        {
            if (skipRender_)
            {
                skipRender_ = false;
                return;
            }

        }

    }
}
