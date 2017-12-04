namespace WorldOfImagination.Game
{
    public class GameState
    {
        public readonly Camera Camera;

        public GameState(WorldOfImaginationGame game)
        {
            Camera = new Camera(game);
        }
    }
}
