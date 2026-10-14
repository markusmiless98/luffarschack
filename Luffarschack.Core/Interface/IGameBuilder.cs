namespace Luffarschack.Core.Interface
{
    public interface IGameBuilder
    {
        abstract GameState NewGame(int playerAmount);
    }
}