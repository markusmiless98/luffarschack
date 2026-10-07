import { MoveRequest, GetFetch, NewGameRequest } from "./apiServiceScript.js";
//refactor for building form when button clicked


async function SetUpMenu()
{
	const global_fetch = await GetFetch();
	try
	{
		if (global_fetch != null)
		{
			let _json = JSON.stringify(global_fetch);
			let _parsedJSON = JSON.parse(_json);
			
			if (_parsedJSON.turn > 0)
			{
				SetUpContinue(_parsedJSON);
			}
		}
		await SetUpStartNewGame();
	}
	catch(error)
	{
		console.error(error.message)
	}
}

function SetUpContinue(data)
{
	try
	{
		let game_state_text = document.getElementById("game_status_text");
		game_state_text.innerText = "There is an ongoing game right now.\nIt is currently Player " + data.currentPlayer + " turn."
		
		const continue_button = document.createElement("button");
		continue_button.innerText = "Continue Current Game";
		continue_button.addEventListener("click", async() => {
			const success = await GetFetch();
			if (success != null)
			{
				GoToGame();
			}
			else
			{
				console.log("failed to reach game");
			}
		});
		
		let container = document.getElementById("continue_game_container");
		container.appendChild(continue_button);
	}
	catch(error)
	{
		console.error(error.message)
	}
}

async function SetUpStartNewGame()
{
	try
	{
		const new_game_container = document.getElementById("new_game_container");
		
		let _label = document.createElement("label");
		_label.innerText = "Player Amount: "
		
		const players_sel = document.createElement("select");
		players_sel.id = "player_amount_dropdown";
		players_sel.innerText = "Player Amount"
		
		for (let i = 2; i < 5; i++)
		{
			let _option = document.createElement("option");
			_option.innerHTML = i.toString();
			players_sel.appendChild(_option);
		}
		
		let ui_msg = document.getElementById("setup_status_text");
		ui_msg.innerText = "Finished loading!"
		
		new_game_container.appendChild(_label);
		new_game_container.appendChild(players_sel);
		
		let new_game_button = document.createElement("button");
		new_game_button.innerText = "Start Game";
		new_game_button.addEventListener("click", async() => {
			let num_players = document.getElementById("player_amount_dropdown").value;
			const success = await NewGameRequest(num_players);
			if (success == true)
			{
				GoToGame();
			}
			else
			{
				console.log("failed to reach game");
			}
		});
		new_game_container.appendChild(new_game_button);
	}
	catch(error)
	{
		console.error(error.message)
	}
}

function GoToGame()
{
	let currentLocation = window.location.href;
	currentLocation = currentLocation.replace("index.html", "game.html");
	
	// Simulate a mouse click:
	window.location.href = currentLocation.toString();
}

SetUpMenu();