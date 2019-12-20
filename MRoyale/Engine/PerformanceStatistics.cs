#region Using Statements
using System.Collections.Generic;
using System.Linq;

using Microsoft.Xna.Framework;
#endregion

namespace CyuubiApps.Engine
{
    public class PerformanceStatistics : GameComponent
    {
        #region Field Region

        private const int MAXIMUM_SAMPLES = 100;

        private Queue<float> _sampleBuffer = new Queue<float>();

        #endregion

        #region Property Region

        public long TotalFrames { get; private set; }
        public float TotalSeconds { get; private set; }
        public float AverageFramesPerSecond { get; private set; }
        public float CurrentFramesPerSecond { get; private set; }
        public float HighestFramesPerSecond { get; private set; }

        #endregion

        #region Constructor Region

        public PerformanceStatistics(Game game)
            : base(game)
        { }

        #endregion

        #region Method Region

        public override void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            CurrentFramesPerSecond = 1.0f / deltaTime;

            _sampleBuffer.Enqueue(CurrentFramesPerSecond);

            if (_sampleBuffer.Count > MAXIMUM_SAMPLES)
            {
                _sampleBuffer.Dequeue();
                AverageFramesPerSecond = _sampleBuffer.Average(i => i);
            }
            else AverageFramesPerSecond = CurrentFramesPerSecond;

            TotalFrames++;
            TotalSeconds += deltaTime;

            if (CurrentFramesPerSecond > HighestFramesPerSecond && gameTime.TotalGameTime.TotalSeconds >= 5)
            {
                if (!float.IsPositiveInfinity(CurrentFramesPerSecond) && !float.IsNegativeInfinity(CurrentFramesPerSecond))
                    HighestFramesPerSecond = CurrentFramesPerSecond;
            }
        }

        #endregion
    }
}
