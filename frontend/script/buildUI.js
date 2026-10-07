import { NewGameRequest } from "./apiServiceScript.js";
import { CheckUpdateBoard } from "./buildBoard.js";
import { BuildPopUp } from "./confirmWindow.js";

const ui_layout = document.getElementById("ui_side");
const reset_layout = document.getElementById("reset_buttons");
const option_side = document.getElementById("option_side");

async function BuildUI()
{
	for (let i = 2; i < 5; i++)
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
		reset_layout.appendChild(reset_but);
	}
	CreateYesNoOptionElement("Disable Placement Popups?", "skip_popup");
}

async function CreateYesNoOptionElement(title, id)
{
	// Temporary variables in here thus _ in front of them
	if (id == null){
		console.log("Failed");
		return;
	}
	const _parent = document.createElement("div");
	if (title != null){
		_parent.innerText = title;
	}
	const _select = document.createElement("select")
	_select.id = id;
	const _option_no = document.createElement("option");
	_option_no.innerText = "No";
	const _option_yes = document.createElement("option");
	_option_yes.innerText = "Yes";
	_select.appendChild(option_no);
	_select.appendChild(option_yes);
	_parent.appendChild(_select);
	option_side.appendChild(_parent);
}

async function CallRequest(num){
	const check = await NewGameRequest(num);
	if (check == true)
	{
		CheckUpdateBoard();
	}
}

BuildUI();