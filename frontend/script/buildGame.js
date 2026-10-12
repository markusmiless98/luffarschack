import { MoveRequest, GetFetch, NewGameRequest } from "./apiServiceScript.js";
import { BuildPopUp } from "./confirmWindow.js";
import { buildGameView } from "./gameViewBuilder.js"
//import { applyPlayerColor, setupPlayers } from "./playerColor.js"

async function buildGame() {

    const view = buildGameView(async (x, y, z, cell) => {
        let pop_up_check = document.getElementById("skip_all_popup");
        if (pop_up_check.value == "No") {

            BuildPopUp(
                "Want to place here?",
                "yes",
                async () => {

                    const ok = await TryMakeMove(x, y, z);
                    console.log("api response:",ok);
                    console.log("x/y/z sent to api",x, y, z)

                    if (ok) {
                        const gamedata = await getGameData()
                        const cellValueFromApi = gamedata.boardState[x][y][z]
                        console.log(`cell value from api: ${cellValueFromApi} x:${x} y:${y} z:${z}`)

                        const viewCell = view.getCell(x, y, z)
                        viewCell.dataset.value = cellValueFromApi

                        console.log(`cell value in view: ${viewCell.dataset.value} x:${x} y:${y} z:${z}`)
                        console.log(`stored cell pos: ${viewCell.dataset.pos} x:${x} y:${y} z:${z}`)

                        viewCell.classList.add("selected", "locked");
                    }

                },
                "no",
                () => console.log("did not place move"),
            );
        }
        else {
            if (TryMakeMove(x, y, z)) cell.classList.add('selected', "locked");
        }
    })
}

buildGame();

async function TryMakeMove(x, y, z) {//was name tryApiCall before
    try {
        const result = await MoveRequest(x, y, z); // Due to button setup this has to be reversed
        if (result.ok) {
            return true
        }
        return false;
    }
    catch (error) {
        console.log("try make move got error back")
        console.error(error);
        return false;
    }
}

async function getGameData() {// i want to put this in api json file
    const json_fetch = await GetFetch();
    const json_string = JSON.stringify(json_fetch);
    const game_data = JSON.parse(json_string);
    return game_data;
    /*
        game_data:{
          "BoardState": [
            0[[0,0,0,0],[0,0,0,0],[0,0,0,0],[0,0,0,0]]3,
            1[[0,0,0,0],[0,0,0,0],[0,0,0,0],[0,0,0,0]]2,
            2[[0,0,0,0],[0,0,0,0],[0,0,0,0],[0,0,0,0]]1,
            3[[0,0,0,0],[0,0,0,0],[0,0,0,0],[0,0,0,0]]0
          ],
          "Winner": 0,
          "Turn": 0,
          "CurrentPlayer": 0
        }
        */
}

function updateAllViewCells(view, gameData) {// update all view cells from game data from api
    const board = gameData.boardState;

    const frontenddata = [] //want to see if cells get same value as data from api in backend

    for (let z = 0; z < board.length; z++) {
        const layer = []
        for (let y = 0; y < board[z].length; y++) {
            const row = []
            for (let x = 0; x < board[z][y].length; x++) {
                updateCell(view, x, y, z, board[x][y][z]);
                row.push(view.getCell(x, y, z).dataset)
            }
            layer.push(row)
        }
        frontenddata.push(layer)
    }
    console.log(board)
    console.log(frontenddata);
}

function updateCell(view, x, y, z, playerValue) {
    const cell = view.getCell(x, y, z)//get the cell from the view maybe send in instead

    if (!cell) {
        console.log("cell to update had no value")
        return;
    }

    if (playerValue != 0) {
        cell.dataset.value = playerValue
        cell.classList.add("selected", "locked"); //set player color here
    }
    else {
        console.log("cell is not selected no cell data changed")
    }
}

function winPopUp(player) {

    let win_text = "Player " + _board.winner.toString() + `${player.color} player won!\nWanna start new game?`;
    if (_board.winner == "-1") {
        win_text = "Draw!\nWanna start new game?"
    }
    let _popup = BuildPopUp(win_text, "Yes", () => {
        NewGameRequest(2)
        updateAllViewCells(view, gameData);
    }, "No", () => console.log("Didn't start new game"));// this needs to go back to main menu

}