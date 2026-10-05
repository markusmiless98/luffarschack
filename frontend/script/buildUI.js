import { NewGameRequest } from "./apiServiceScript.js";
import { CheckUpdateBoard } from "./buildBoard.js";
import { BuildPopUp } from "./confirmWindow.js";

const ui_layout = document.getElementById("ui_side");

async function BuildUI()
{
	const _button = document.createElement("Button");
	_button.id = "restart 1";
	_button.innerText = "restart"
	_button.addEventListener("click", async() => {
		const popup = BuildPopUp(
			"Start New Game?",
			"yes",
			() => CallRequest(2),
			"no",
			() => console.log("Close Window"),
		); //put what you want to run when user presses cancel
	});
	ui_side.appendChild(_button);
	for (let i = 0; i < 5; i++)
	{
		const reset_but = document.createElement("Button");
		reset_but.id = "reset " + i.toString();
		reset_but.innerText = i.toString() + " Players";
		reset_but.addEventListener("click", async() => {
			const popup = BuildPopUp(
				"Start New Game? " + i + " players",
				"yes",
				() => CallRequest(i),
				"no",
				() => console.log("Close Window"),
				); //put what you want to run when user presses cancel
		});
		ui_side.appendChild(reset_but);
	}
}

async function CallRequest(num){
	const check = await NewGameRequest(2);
	if (check == true)
	{
		CheckUpdateBoard();
	}
}

BuildUI();