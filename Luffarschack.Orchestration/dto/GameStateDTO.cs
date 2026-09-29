using Luffarschack.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Luffarschack.Orchestration.dto
{
    public class GameStateDTO
    {
        public GameStateDTO(GameState _state)
        {
            BoardState = _state.BoardState.ToJagged();
            Players = _state.Players;
            Winner = _state.Winner;
            Turn = _state.Turn;
        }

        public int[][][] BoardState { get; set; }
        [JsonIgnore]
        public List<int> Players { get; set; }
        public int Winner { get; set; }
        public int Turn { get; set; }

    }
}
