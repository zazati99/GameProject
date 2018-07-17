using System;

namespace GameProject.GameUtils
{
    public class Timer
    {
        // Time til meme
        float timeInFrames;

        // will gamespeed affect this?
        public bool GameSpeedDependable;

        // Constructors
        public Timer(int timeInFrames)
        {
            this.timeInFrames = timeInFrames;
            GameSpeedDependable = true;
        }
        public Timer()
        {
            timeInFrames = 0;
            GameSpeedDependable = true;
        }

        // Set timer
        public void Set(int frames)
        {
            timeInFrames = frames;
        }

        // Tick timer
        public void Tick()
        {
            timeInFrames -= GameSpeedDependable ? MainGame.GAME_SPEED : 1;
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
