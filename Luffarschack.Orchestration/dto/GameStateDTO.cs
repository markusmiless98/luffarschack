using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Luffarschack.Orchestration.dto
{
    public class GameStateDTO
    {
        //a dto(data transfer object) is only a data holder it should not need to reach into core nor need to map itself
        //it should not have a dependancy on core, that's why i took away the constructor and created a mapper helper instead

        public int[][][] BoardState { get; set; }
        public int CurrentPlayer { get; set; }
        public int Winner { get; set; }
        public int Turn { get; set; }

    }
}
