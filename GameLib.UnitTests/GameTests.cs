using System;

using Xunit;

namespace GameLib.UnitTests
{
    public class GameTests
    {
        [Theory]
        [InlineData(60)]
        [InlineData(30)]
        [InlineData(10000)]
        public void CalcDeltaTime_FrameLength_DeltaTimeSimiliarToFrameLength(float fps)
        {
            Game game = new Game();

            game.CalcDeltaTime(fps);  // first is invalid -- see summary for CalcDeltaTime
            DateTime before = DateTime.Now;
            float deltaTime = game.CalcDeltaTime(fps);
            DateTime after = DateTime.Now;
            float frameLength = (float)(after - before).TotalSeconds;

            Assert.True((frameLength <= deltaTime) && (deltaTime <= frameLength * 1.1f));
        }
    }
}
