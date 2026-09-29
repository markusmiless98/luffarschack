using Luffarschack.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Luffarschack.Orchestration.Interface
{
    public interface IGameService
    {
        public string GetGameState();
        public void MakeMove(Move move);

    }
}
