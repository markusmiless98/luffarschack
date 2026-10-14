
//const optionsContainer = document.getElementById('color_options');
const COLORS = ["blue", "red", "green", "orange"];

export function getPlayerColor(playerNumber) {
    return COLORS[playerNumber - 1];       
    // player 1 -> index 0, player 2 -> index 1
}

export function applyPlayerColor(cell, playerNumber) {
    console.log("no player", playerNumber);
    if (!cell) {
        console.log("no cell value");
        return;
    }
    if (!playerNumber) {
        console.log("no player", playerNumber);
        return;
    }

    const color = getPlayerColor(playerNumber);
    if (!color) return;

    if (color) {
        cell.classList.add(`cube_color_${color}`);
        console.log("adding player color", `cube_color_${color}`)
    }
    else {
        console.log("no color value")
    }
}

export function resetColor(cell) {
    for (const c of COLORS) {
        cell.classList.remove(`cube_color_${c}`);
    }
    cell.classList.remove("locked");
}
