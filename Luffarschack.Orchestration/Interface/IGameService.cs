using Luffarschack.Core;
using System;
using System.Collections.Generic;
using System.Text;
using Luffarschack.Orchestration.dto;

namespace Luffarschack.Orchestration.Interface
{
    public interface IGameService
    {
        public GameStateDTO GetGameStateDTO();
        public bool MakeMove(MoveRequest move);

    }
}
