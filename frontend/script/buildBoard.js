import { MoveRequest, GetFetch, NewGameRequest } from "./apiServiceScript.js";
import { BuildPopUp } from "./confirmWindow.js";

const board = document.getElementById("board");
const pop_up_check = document.getElementById("skip_popup");
const size = 4;

async function BuildBoard()
{
	for (let x = 0; x < size; x++) {
		for (let y = 0; y < size; y++) {
			const cell = document.createElement("button");
			cell.id = "Cell " + x.toString() + " " + y.toString();
			cell.className = "cell";
			cell.addEventListener("click", () => {
				// Have info sent elsewhere instead of popup in future
				// So that instead of popup it is click and then press button to confirm
				if (pop_up_check.value == "No"){
					cell.disabled = true;
					const popup = BuildPopUp(
						"Want to place here?",
						"yes",
						() => TryApiCall(x, y, cell),
						"no",
						() => cell.disabled = false,
					); //put what you want to run when user presses cancel
				}
				else{
					TryApiCall(x, y, cell)
				}

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
	}
	catch (error) {
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
					UpdateBoard(x,y,0,_column, _winner);
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
		
		if (_winner == true)
		{
			//console.log("Player " + _board.winner " won");
			let win_text = "Player " + _board.winner.toString() + " won!";
			if (_board.winner == "-1"){
				win_text = "Draw!"
			}
				
			let _popup = BuildPopUp(
				win_text.toString(),
				"yes",
				() => console.log("yes works"));//you can exlude the cancel value and you wont have that button appear
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
	
	let _id = "Cell " + x.toString() + " " + y.toString();
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