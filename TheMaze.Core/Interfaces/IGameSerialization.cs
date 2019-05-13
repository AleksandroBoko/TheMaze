namespace TheMaze.Core.Interfaces
{
    public interface IGameSerialization
    {
        void Save();
        bool Load();
    }
}
