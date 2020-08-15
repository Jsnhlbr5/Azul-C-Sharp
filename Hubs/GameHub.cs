using AzulApp;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace GameServer.Hubs
{
    public class GameHub : Hub<IGameClient>
    {
        public async Task SendPick(int player, int factory, Color color)
        {
            await Clients.Others.PickTiles(player, factory, color);
        }

        public async Task SendPlace(int player, int row)
        {
            await Clients.Others.PlaceTiles(player, row);
        }

        public async Task SendState(int numPlayers, int activePlayer, Color[][] tiles)
        {
            await Clients.Others.RecieveState(numPlayers, activePlayer, tiles);
        }
    }
}
