namespace Luffarschack.Core.Interface
{
    public interface IVictoryStatusValidator
    {
        public int CheckGameOutcome(GameState gameState, MoveRequest move);
        public int CheckGameOutcome(int[,,] board, int player, int turn, int x, int y, int z);
    }
}