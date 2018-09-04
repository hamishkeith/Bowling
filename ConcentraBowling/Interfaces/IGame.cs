namespace ConcentraBowling.Interfaces
{
    interface IGame
    {
        void Roll(int pins);
        int Score();
        int NumberOfPinsRemainingInFrame();
    }
}
