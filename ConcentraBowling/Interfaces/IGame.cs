namespace ConcentraBowling.Interfaces
{
    public interface IGame
    {
        bool isPlaying { get; }
        void Roll(int pins);
        int Score();
        int NumberOfPinsRemainingInFrame();
    }
}
