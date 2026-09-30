import { MoveRequest, GetFetch, NewGameRequest } from "./apiServiceScript.js";

const board = document.getElementById("board");
const size = 4;

async function BuildBoard() {
	for (let x = 0; x < size; x++) {
		for (let y = 0; y < size; y++) {
			const cell = document.createElement("button");
			cell.id = "Cell " + x.toString() + " " + y.toString();
			cell.className = "cell";
			cell.addEventListener("click", () => {
				TryApiCall(x, y, cell)
			});
			board.appendChild(cell);
		}
	}
}

async function TryApiCall(x, y, cell) {
	cell.disabled = true;
	try {
		const result = await MoveRequest(0,y,x); // Due to button setup this has to be reversed
		if (result.ok)
		{
			await CheckUpdateBoard();
		}
	} catch (error) {
		cell.disabled = false;
		console.error(error);
	}
}

export async function CheckUpdateBoard()
{
	try {
		/*
		const json_temp = `{
		  "BoardState": [
			[[0,0,0,0],[0,0,0,0],[0,0,0,0],[0,0,0,0]],
			[[0,0,0,0],[0,0,0,0],[0,0,0,0],[0,0,0,0]],
			[[0,0,0,0],[0,0,0,0],[0,0,0,0],[0,0,0,0]],
			[[0,0,0,0],[0,0,0,0],[0,0,0,0],[0,0,0,0]]
		  ],
		  "Winner": 0,
		  "Turn": 0,
		  "CurrentPlayer": 0
		}`;
		*/
		const json_fetch = await GetFetch();
		const _temp = JSON.stringify(json_fetch);
		//console.log(_temp);
		
		const _board = JSON.parse(_temp);
		
		let x = 0;
		let y = 0;
		let z = 0;
		for (const _layer of _board.boardState)
		{
			for (const _row of _layer)
			{
				for (const _column of _row)
				{
					UpdateBoard(x,y,0,_column);
					x++;
				}
				x = 0;
				y++;
			}
			x = 0;
			y = 0;
			z++;
			if (z > 0) break;
		}
	}
	catch (error) {
		console.error(error);
	}
}

// Should maybe be moved
function UpdateBoard(x,y,z,tar)
{
	let _id = "Cell " + x.toString() + " " + y.toString();
	const _boardPlace = document.getElementById(_id);
	_boardPlace.innerText = tar.toString();
	if (_boardPlace.innerText == "0"){
		_boardPlace.disabled = false;
	}
	else{
		_boardPlace.disabled = true;
	}
}


BuildBoard()