namespace ConcentraBowling.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ConcentraBowling.Classes;
    using System.Linq;

    [TestClass]
    public class GameTests
    {
        private Game _sut;

        [TestInitialize]
        public void Initialise()
        {
            _sut = new Game();
        }

        #region Frame Counter Tests
        [TestMethod]
        public void FrameCounterShouldEqualOne()
        {
            _sut.Roll(1);
            Assert.AreEqual(1, _sut.CurrentFrame);
        }

        [TestMethod]
        public void FrameCounterShouldEqualTwoAfterStrikeOnFirstBall()
        {
            _sut.Roll(10);
            Assert.AreEqual(2, _sut.CurrentFrame);
        }

        [TestMethod]
        public void FrameCounterShouldEqualTwoAfterThreeRolls()
        {
            _sut.Roll(1);
            _sut.Roll(1);
            _sut.Roll(1);
            Assert.AreEqual(2, _sut.CurrentFrame);
        }
        #endregion

        #region Bonus Calculation Tests
        [TestMethod]
        public void SpareBonusesCalculatedCorrectly()
        {
            _sut.CurrentScore[1] = new System.Collections.Generic.List<int> { 5,5 };
            _sut.CurrentScore[2] = new System.Collections.Generic.List<int> { 4,4 };
            Assert.AreEqual(22, _sut.Score());
        }

        [TestMethod]
        public void StrikeBonusesCalculatedCorrectly()
        {
            _sut.CurrentScore[1] = new System.Collections.Generic.List<int> { 10 };
            _sut.CurrentScore[2] = new System.Collections.Generic.List<int> { 4,4 };
            Assert.AreEqual(26, _sut.Score());
        }
        #endregion

        #region Bowling Game Tests

        [DataRow(2, 3)]
        [DataRow(1, 1)]
        [DataRow(5, 5)]
        [TestMethod]
        public void RollsAreRecodedCorrectly(int firstRoll, int secondRoll)
        {
            _sut.Roll(firstRoll);
            _sut.Roll(secondRoll);

            Assert.AreEqual(firstRoll, _sut.CurrentScore[1][0]);
            Assert.AreEqual(secondRoll, _sut.CurrentScore[1][1]);
        }

        [TestMethod]
        public void PerfectGameScored300()
        {
            _sut.Roll(10);
            _sut.Roll(10);
            _sut.Roll(10);
            _sut.Roll(10);
            _sut.Roll(10);
            _sut.Roll(10);
            _sut.Roll(10);
            _sut.Roll(10);
            _sut.Roll(10);
            _sut.Roll(10);

            _sut.Roll(10);
            _sut.Roll(10);

            Assert.AreEqual(120, _sut.CurrentScore.Values.Sum(x => x.Sum()));
            Assert.AreEqual(300, _sut.Score());
        }

        [TestMethod]
        public void GutterBallGameScoresZero()
        {
            var rolls = 0;
            while (++rolls < 21)
            {
                _sut.Roll(0);
            }
            var score = _sut.Score();
            Assert.AreEqual(0, score);
        }

        //simulate shorted game for testing (3 frames).  Bonus rules applied but test does not cover the final frame rules
        [DataRow(2, 5, 0, 0, 0, 0, 7,7)]
        [DataRow(2, 5, 3, 3, 0, 0, 13,13)]
        [DataRow(5, 5, 3, 3, 0, 0, 16, 19)]
        [DataRow(10, 10, 10, 0, 0, 0, 30, 60)]
        [TestMethod]
        public void EarlyFramesScoredCorrectly(int firstRoll, int secondRoll, int thirdRoll, int fourthRoll, int fifthRoll, int sixthRoll, int expectedSumOfRows, int expectedScore)
        {
            _sut.Roll(firstRoll);
            _sut.Roll(secondRoll);
            _sut.Roll(thirdRoll);
            _sut.Roll(fourthRoll);
            _sut.Roll(fifthRoll);
            _sut.Roll(sixthRoll);

            Assert.AreEqual(expectedSumOfRows, _sut.CurrentScore.Values.Sum(x => x.Sum()));
            Assert.AreEqual(expectedScore, _sut.Score());
        }

        #endregion
    }
}
