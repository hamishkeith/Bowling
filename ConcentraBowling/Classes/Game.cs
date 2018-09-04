namespace ConcentraBowling.Classes
{
    using Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Game : IGame
    {
        const int maxPins = 10, finalFrameNumber = 10, maxRollsInFinalFrame = 3;
        public bool isPlaying = true; //used by the console app only
        public Dictionary<int, List<int>> CurrentScore { get; set; } = new Dictionary<int, List<int>>(); //holds score - key is the frame number and the array of int's is the scores
        private int CurrentRoll { get; set; } = 1;
        public int CurrentFrame { get; set; } = 1;
            
        public void Roll(int pins)
        {
            if (pins > NumberOfPinsRemainingInFrame() || pins > 10) throw new ArgumentOutOfRangeException("pins", "You can only knock down 10 pins in a standard frame and 30 pins in the final frame");
            
            if (CurrentScore.Keys.Count == finalFrameNumber)
            {
                RollInFinalFrame(pins);
            }
            else if (CurrentScore.Keys.Count < finalFrameNumber)
            {
                RollInStandardFrame(pins);
            }           
        }

        private void RollInFinalFrame(int pins)
        {
            switch (CurrentRoll)
            {
                case 1:
                    CurrentScore[finalFrameNumber] = new List<int> { pins };
                    CurrentRoll++;
                    break;
                case maxRollsInFinalFrame:
                    CurrentScore[finalFrameNumber].Add(pins);
                    //Score();
                    isPlaying = false;
                    break;
                default:
                    CurrentScore[finalFrameNumber].Add(pins);
                    if (CurrentScore[finalFrameNumber].Sum() == maxPins) //if they got a spare or strike in final game they get another go.
                    {
                        CurrentRoll++;
                    }
                    else
                    {
                        //var score = Score();
                        isPlaying = false;
                    }
                    break;
            }
        }

        private void RollInStandardFrame(int pins)
        {
            if (CurrentRoll == 1)
            {
                CurrentScore[CurrentFrame] = new List<int> { pins };
                if (pins == maxPins)
                {
                    CurrentRoll = CurrentScore.Keys.Count == finalFrameNumber ? 2 : CurrentRoll;
                    CurrentFrame++;
                }
                else
                {
                    CurrentRoll++;
                }
            }
            else if (CurrentRoll == 2)
            {
                CurrentScore[CurrentFrame].Add(pins);
                CurrentRoll = 1;
                CurrentFrame++;
            }
        }
                
        public int Score()
        {
            var finalScore = 0;
            foreach (KeyValuePair<int, List<int>> score in CurrentScore)
            {
                var bonus = score.Value.First() == maxPins ? CalcualteStrikeBonus(score.Key) : 0;
                if (bonus == 0 && score.Value.Sum() == 10) bonus = CalculateSpareBonus(score.Key);
                finalScore += score.Value.Sum() + bonus;
            }
            return finalScore;
        }

        public int NumberOfPinsRemainingInFrame()
        {
            if (CurrentScore.Keys.Count != finalFrameNumber)
            {
                return CurrentScore.Keys.Contains(CurrentFrame) ? (maxPins - CurrentScore[CurrentFrame].Sum()) : maxPins;
            }
            else
            {
                var wasFinalFrameAStrike = CurrentScore[finalFrameNumber].Sum() == 10 ? true : false;
                if (wasFinalFrameAStrike)
                {
                    var maxPinsInFinalFrame = maxPins * maxRollsInFinalFrame;
                    return CurrentScore.Keys.Contains(CurrentFrame) ? (maxPinsInFinalFrame - CurrentScore[CurrentFrame].Sum()) : maxPinsInFinalFrame;
                }
                else
                {
                    return CurrentScore.Keys.Contains(CurrentFrame) ? (maxPins - CurrentScore[CurrentFrame].Sum()) : maxPins;
                }
            }
        }

        private int CalculateSpareBonus(int gameNumber)
        {
            return gameNumber != finalFrameNumber ? CurrentScore[gameNumber + 1][0] : CurrentScore[gameNumber][2];
        }

        private int CalcualteStrikeBonus(int gameNumber)
        {
            var strikeBonus = 0;
            if (gameNumber != finalFrameNumber)
            {
                strikeBonus = CurrentScore[gameNumber + 1][0];
                if (strikeBonus == 10 && gameNumber != (finalFrameNumber - 1)) //two strikes in a row & not penultimate game
                {
                    strikeBonus += CurrentScore[gameNumber + 2][0];
                }
                else
                {
                    strikeBonus = CurrentScore[gameNumber + 1].Take(2).Sum();
                }
            }
            return strikeBonus;
        }
    }
}