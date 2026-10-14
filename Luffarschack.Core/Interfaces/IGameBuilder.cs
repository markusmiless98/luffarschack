using Luffarschack.Core.ValueObjects;

namespace Luffarschack.Core.Interfaces
{
    public interface IGameBuilder
    {
        abstract GameState NewGame(int playerAmount);
    }
}