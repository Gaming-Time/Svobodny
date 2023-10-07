using System;

namespace CodeBase.Data
{
    [Serializable]
    public class PlayerProgress
    {
        public string LevelName;

        public PlayerProgress(string levelName)
        {
            LevelName = levelName;
        }
    }
}