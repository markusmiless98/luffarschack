import { MoveRequest, GetFetch, NewGameRequest } from "./apiServiceScript.js";
import { BuildPopUp } from "./popup.js";
import { buildGameView } from "./gameViewBuilder.js"
import { applyPlayerColor, resetColor, getPlayerColor } from "./playerColorSetUp.js"
import { buildOptions } from "./buildOptions.js"
async function buildGame() {
    buildGameView(async (x, y, z) => {
        const skipConfirm = document.getElementById("skip_popup").value === "Yes";
        confirmMove(x, y, z, skipConfirm);
    });

    loadBoard()

    buildGameInfo()
    buildOptions()
}

async function confirmMove(x, y, z, skipConfirm = false) {
    if (skipConfirm) {
        await handleMove(x, y, z);
        return;
    }

    BuildPopUp(
        "Want to place here?",
        "yes",
        async () => { await handleMove(x, y, z) },
        "no",
        () => console.log("did not place move"),
    );
}

async function handleMove(x, y, z) {
    const ok = await tryMakeMove(x, y, z);
    if (!ok) return;

    const gamedata = await getGameData();
    markCell(x, y, z, gamedata.boardState[x][y][z]);
    //if we want to start the game with the same amount of players again then we need to sent the player amount of the game via the api

    updateGameInfo(gamedata);
    if (gamedata.winner != 0) winPopUp(gamedata.winner);
}

function markCell(x, y, z, player) {
    const viewCell = document.getElementById(`cube ${x}${y}${z}`);
    updateCell(viewCell, player);
}

async function tryMakeMove(x, y, z) {
    try {
        const result = await MoveRequest(x, y, z);
        return result.ok;
    } catch (error) {
        console.error("tryMakeMove failed:", error);
        return false;
    }
}

async function getGameData() {
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

export async function loadBoard() {// update all view cells from backend data
    const gameData = await getGameData()
    console.log("gamedata after new game", gameData)
    updateGameInfo(gameData);
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
        applyPlayerColor(cell, playerValue) //set player color here
        cell.classList.add("locked");
    }
    else {
        resetColor(cell)
    }
}

async function winPopUp(winner, playerAmount = 2) {
    let text = "Player " + winner.toString() + " won!\nWanna start new game?";

    if (winner > 0) {
        BuildPopUp(text, "Yes", async () => {
            await NewGameRequest(playerAmount)//need to get how many players played last game
            await loadBoard()
        }, "No", () => {
            window.location.href = 'index.html';
            console.log("Didn't start new game")
        });
    }
    if (winner === -1) {
        text = "It's a Draw!\nWant to start new game?"
        BuildPopUp(text, "Yes", async () => {
            await NewGameRequest(playerAmount)
            await loadBoard();
        }, "No", () => {
            window.location.href = 'index.html';
            console.log("Didn't start new game")
        });
    }

}

function buildGameInfo() {
    const info = document.getElementById("game_info")
    info.innerHTML = `
        <div>Turn: <span id="info-turn">-</span></div>
        <div>Player: <span id="info-player">-</span></div>
        <div>Color: <span id="info-player-color">-</span></div>
    `;
}

function updateGameInfo(gamedata) {
    document.getElementById("info-turn").textContent = gamedata.turn;
    document.getElementById("info-player").textContent = gamedata.currentPlayer;
    document.getElementById("info-player-color").textContent = getPlayerColor(gamedata.currentPlayer);
}

buildGame();