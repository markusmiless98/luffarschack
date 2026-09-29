import { MoveRequest, GetFetch } from "./apiServiceScript.js";

const board = document.getElementById("board");
const size = 4;

async function BuildBoard() {
    for (let x = 0; x < size; x++) {
        for (let y = 0; y < size; y++) {
            const cell = document.createElement("button");
            cell.className = "cell";
            cell.addEventListener("click", () => {
                console.log(x, y);
                TryApiCall(x, y, cell)
            });
            board.appendChild(cell);
        }
    }
}

async function TryApiCall(x, y, cell) {
    cell.disabled = true;
    try {
        const result = await MoveRequest(x, y, 0);
        console.log(result);
    } catch (error) {
        cell.disabled = false;
        console.error(error);
    }
}

BuildBoard()