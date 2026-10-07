import { MoveRequest, GetFetch, NewGameRequest } from "./apiServiceScript.js";

const board = document.getElementById("board");
const size = 4;

async function BuildBoard() {
	for (let z = 0; z < size; z++){
		const board_containter = document.createElement("div");
		board_containter.className = "board";
		for (let y = 0; y < size; y++) {
			for (let x = 0; x < size; x++) {
				const cell = document.createElement("button");
				cell.id = "Cell " + x.toString() + " " + y.toString() + " " + z.toString();
				cell.className = "cell";
				cell.addEventListener("click", () => {
					TryApiCall(x, y, z, cell)
				});
				board_containter.appendChild(cell);
			}
		}
		board.appendChild(board_containter);
	}
}

async function TryApiCall(x, y, z, cell) {
	cell.disabled = true;
	try {
		const result = await MoveRequest(z,y,x); // Due to button setup this has to be reversed
		if (result.ok)
		{
			await CheckUpdateBoard();
		}
	}
	catch (error) {
		cell.disabled = false;
		console.error(error);
	}
}

export async function CheckUpdateBoard()
{
	try {
		const json_fetch = await GetFetch();
		const _temp = JSON.stringify(json_fetch);
		
		const _board = JSON.parse(_temp);
		
		let x = 0;
		let y = 0;
		let z = 0;
		let _winner = (_board.winner != 0);
		for (const _layer of _board.boardState)
		{
			for (const _row of _layer)
			{
				for (const _column of _row)
				{
					UpdateBoard(x,y,z,_column, _winner);
					x++;
				}
				x = 0;
				y++;
			}
			x = 0;
			y = 0;
			z++;
		}
	}
	catch (error) {
		console.error(error);
	}
}

// Should maybe be moved
function UpdateBoard(x,y,z,tar,win)
{
	if (x == null || y == null || z == null) return false;
	
	let _id = "Cell " + x.toString() + " " + y.toString() + " " + z.toString();
	const _boardPlace = document.getElementById(_id);
	_boardPlace.innerText = tar.toString();
	if (win == true || _boardPlace.innerText != "0")
	{
		// Game is over don't change buttons now
		_boardPlace.disabled = true;
	}
	else
	{
		_boardPlace.disabled = false;
	}
}


BuildBoard()