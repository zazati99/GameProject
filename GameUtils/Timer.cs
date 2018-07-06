using System;

namespace GameProject.GameUtils
{
    public class Timer
    {
        // Time til meme
        int timeInFrames;

        // Constructors
        public Timer(int timeInFrames)
        {
            this.timeInFrames = timeInFrames;
        }
        public Timer()
        {
            timeInFrames = 0;
        }

        // Set timer
        public void Set(int frames)
        {
            timeInFrames = frames;
        }

        // Tick timer
        public void Tick()
        {
            timeInFrames--;
        }

        // Check timer
        public bool Check()
        {
            return timeInFrames <= 0;
        }

        // Time and tick
        public bool TickAndCheck()
        {
            Tick();
            return Check();
        }
    }
}
