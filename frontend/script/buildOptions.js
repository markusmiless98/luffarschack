import { NewGameRequest } from "./apiServiceScript.js";
import { BuildManyButtonPopUp } from "./popup.js";
import { loadBoard } from "./buildGame.js"

export function buildOptions() {
    const options = document.getElementById("options");
    const form = document.getElementById("options_form");

    form.append(createYesNoSelection("skip_all_popup", "Disable ALL Popups? "))
    form.append(createYesNoSelection("skip_popup", "Disable Placement Popups? ",))
    const reset_container = document.getElementById("reset_container");

    const reset_button = document.createElement("Button");
    reset_button.id = "reset"
    reset_button.innerText = "reset";
    bindNewGamePopup(reset_button)

    reset_container.append(reset_button)
}

const MAX_PLAYERS = 4
const MIN_PLAYERS = 2
function bindNewGamePopup(button) {
    //prepare pop upp button text and onClick code
    const popUppText = "how many players?"
    const buttonValues = []
    
    for (let i = MIN_PLAYERS; i <= MAX_PLAYERS; i++) {
        buttonValues.push({
            text: `${i} players`,
            onClick: async () => await tryNewGameRequest(i)
        })
    }
    buttonValues.push({
        text: "cancel",
        onClick: () => { }
    })

    //add popupp to button
    button.addEventListener("click", () => BuildManyButtonPopUp(popUppText, buttonValues))
}

async function tryNewGameRequest(num) {//CallRequest
    const check = await NewGameRequest(num);
    if (check == true) {
        loadBoard()
    }
}

function createYesNoSelection(id, title) {//CreateYesNoOptionElement
    const container = document.createElement("div")

    if (id == null) {
        console.log("Failed");
        return;
    }

    if (title != null) {
        const label = document.createElement("label");
        label.textContent = title;
        label.htmlFor = id;
        container.append(label);
    }
    const select = document.createElement("select")
    select.id = id

    const option_no = document.createElement("option");
    option_no.innerText = "No";

    const option_yes = document.createElement("option");
    option_yes.innerText = "Yes";

    select.append(option_no, option_yes,)
    container.append(select)
    return container
}
