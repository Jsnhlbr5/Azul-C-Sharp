using AzulApp;
using System.Threading.Tasks;


namespace GameServer
{
    public interface IGameClient
    {
        Task RecieveState(int numPlayers, int activePlayer, Color[][] tiles);

        // The center area is always "factory" 10
        Task PickTiles(int player, int factory, Color color);

        Task PlaceTiles(int player, int row);
    }
}
