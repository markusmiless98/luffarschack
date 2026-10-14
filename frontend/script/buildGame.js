import { MoveRequest, GetFetch, NewGameRequest } from "./apiServiceScript.js";
import { BuildPopUp } from "./popup.js";
import { buildGameView } from "./gameViewBuilder.js"
import { applyPlayerColor, resetColor } from "./playerColorSetUp.js"

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
                        if (gamedata.winner != 0) {
                            winPopUp(gamedata.winner)
                        }
                        const cellValueFromApi = gamedata.boardState[x][y][z]
                        console.log(`cell value from api: ${cellValueFromApi} x:${x} y:${y} z:${z}`)

                        const viewCell = view.getCell(x, y, z)
                        viewCell.dataset.value = cellValueFromApi

                        console.log(`cell value in view: ${viewCell.dataset.value} x:${x} y:${y} z:${z}`)
                        console.log(`stored cell pos: ${viewCell.dataset.pos} x:${x} y:${y} z:${z}`)
                        
                        const playerNum = gamedata.currentPlayer
                        updateCell(viewCell, playerNum)
                    }

                },
                "no",
                () => console.log("did not place move"),
            );
        }
        else {
            const ok = await TryMakeMove(x, y, z);
            console.log("api response:", ok);
            console.log("x/y/z sent to api", x, y, z)

            if (ok) {
                const gamedata = await getGameData()
                if (gamedata.winner != 0) {
                    winPopUp(gamedata.winner)
                }
                const cellValueFromApi = gamedata.boardState[x][y][z]
                console.log(`position from api: ${cellValueFromApi} x:${x} y:${y} z:${z}`)

                const viewCell = view.getCell(x, y, z)
                viewCell.dataset.value = cellValueFromApi

                console.log(`position in view: ${viewCell.dataset.value} x:${x} y:${y} z:${z}`)
                console.log(`stored cell pos: ${viewCell.dataset.pos} x:${x} y:${y} z:${z}`) //maybe get rid of this

                const playerNum = gamedata.currentPlayer
                console.log("this is player", playerNum)
                updateCell(viewCell, playerNum)
            }
        }
    });
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
          "boardState": [
            0[[0,0,0,0],[0,0,0,0],[0,0,0,0],[0,0,0,0]]3,
            1[[0,0,0,0],[0,0,0,0],[0,0,0,0],[0,0,0,0]]2,
            2[[0,0,0,0],[0,0,0,0],[0,0,0,0],[0,0,0,0]]1,
            3[[0,0,0,0],[0,0,0,0],[0,0,0,0],[0,0,0,0]]0
          ],
          "winner": 0,
          "turn": 0,
          "currentPlayer": 0
        }
        */
}

export async function refreshBoard() {// update all view cells from game data from api
    const gameData = await getGameData()
    console.log("gamedata after new game", gameData)
    const board = gameData.boardState;

    for (let z = 0; z < board.length; z++) {
        for (let y = 0; y < board[z].length; y++) {
            for (let x = 0; x < board[z][y].length; x++) {
                let cell = document.getElementById(`cube ${x}${y}${z}`)
                if (board[x][y][z] == 0) {
                    resetColor(cell)
                }
                else {
                    updateCell(cell, board[x][y][z]);
                }
            }
        }
    }
}

function updateCell(cell, playerValue) {
    if (!cell) {
        console.log("cell to update had no value")
        return;
    }
    if (playerValue == Number) {
        console.log("no player number", playerValue);
        return;
    }

    if (playerValue != 0) {
        console.log(playerValue)
        applyPlayerColor(cell, playerValue) //set player color here
        cell.classList.add("locked");
    }
    else {
        resetColor(cell)
    }
}

async function winPopUp(winner) {
    let text = "Player " + winner.toString() + " won!\nWanna start new game?";

    if (winner > 0) {
        BuildPopUp(text, "Yes", async () => {
            await NewGameRequest(2)//need to get how many players played last game
            await refreshBoard()
        }, "No", () => {
            window.location.href = 'index.html';
            console.log("Didn't start new game")
        });
    }
    if (winner === -1) {
        text = "It's a Draw!\nWant to start new game?"
        BuildPopUp(text, "Yes", async () => {
            await NewGameRequest(2)
            await refreshBoard();
        }, "No", () => {
            window.location.href = 'index.html';
            console.log("Didn't start new game")
        });
    }

}
