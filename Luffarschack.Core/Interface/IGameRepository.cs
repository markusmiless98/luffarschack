using System;
using System.Collections.Generic;
using System.Text;

namespace Luffarschack.Core.Interface
{
    public interface IGameRepository
    {
        public void StartNewGame(int playerNum);
        public GameState LoadGame();
        public void SaveGame(GameState state);
        public void MakeMove(int x, int y, int z, int player, GameState game);
        
        // Starta Spel (alla spelare plaserade sätts till 0, turn till 0, håller koll på antalet spelare) - när spelare väljer att starta nytt spel
        // Spara Spel (sparar datan ovan) - när drag händer eller när API stänger (om möjligt)
        // Ladda Spel (hämtar datan från gamla spelet) - när requested eller när API startar
        // Gör Drag (spara infon av senaste draget/uppdaterar databasen för 3D layout) - när drag görs
    }
}
