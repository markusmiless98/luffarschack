import { MoveRequest, GetFetch } from "./apiServiceScript.js";

const board = document.getElementById("board");
const size = 4;

async function BuildBoard() {
    for (let x = 0; x < size; x++) {
        for (let y = 0; y < size; y++) {
            const cell = document.createElement("button");
			cell.id = "Cell " + x.toString() + " " + y.toString();
            cell.className = "cell";
            cell.addEventListener("click", () => {
                console.log(x, y);
                TryApiCall(x, y, cell)
            });
            board.appendChild(cell);
        }
    }
}

async function TryApiCall(x, y, cell) {
    cell.disabled = true;
    try {
        const result = await MoveRequest(x, y, 0);
        console.log(result);
		UpdateBoard(result);
    } catch (error) {
        cell.disabled = false;
        console.error(error);
    }
}

async function UpdateBoard(move)
{
    try {
        //const result = GetFetch();
		//console.log(result);
		// Use parse later
		const json_temp = `{
		  "BoardState": [
			[[0,0,0,0],[1,0,0,0],[2,0,0,0],[3,0,0,0]],
			[[0,1,0,0],[1,1,0,0],[2,1,0,0],[3,1,0,0]],
			[[0,2,0,0],[1,2,0,0],[2,2,0,0],[3,2,0,0]],
			[[0,3,0,0],[1,3,0,0],[2,3,0,0],[3,3,0,0]]
		  ],
		  "Winner": 0,
		  "Turn": 0
		}`;
		console.log(json_temp);
		const _board = JSON.parse(json_temp);
		console.log(_board);
		console.log(_board.BoardState);
		let x = 0;
		let y = 0;
		for (const _layer of _board.BoardState)
		{
			for (const _position of _layer){
				if (_position[3] != 0){
					console.log("Occupied Space");
				}
				if (x == move.x && y == move.y){
					console.log("Placed at " + _position);
				}
				x++;
			}
			x = 0;
			y++;
		}
    }
	catch (error) {
        console.error(error);
    }
}

BuildBoard()