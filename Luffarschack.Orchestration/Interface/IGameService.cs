using System;
using System.Collections.Generic;
using System.Text;
using Luffarschack.Orchestration.dto;
using Luffarschack.Core.ValueObjects;

namespace Luffarschack.Orchestration.Interface
{
    public interface IGameService
    {
        public GameState GameState { get; set; }
        public bool StartGame(int players);
        public GameStateDTO GetGameStateDTO();
        public bool MakeMove(MoveRequest move);

    }
}
