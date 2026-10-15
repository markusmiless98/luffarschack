# Luffarshack: 3D Tic-Tac-Toe (4x4x4)
 
A turn-based, 3D version of tic-tac-toe played on a 4x4x4 board, shown as four stacked 4x4 layers with cubes. 
Two to four players take turns on the same computer (there is no online play), placing pieces by clicking cubes on the board. 
The first player to get four in a row wins. A line can run along a row, column or pillar, across a diagonal of a layer, 
or diagonally through the whole board.

## Features
 
- 4x4x4 board shown as four layer images with clickable cubes
- 2 to 4 players, with turns decided by the server
- Win detection for all line types: straight lines, planar diagonals and the four cube diagonals
- Draw detection
-  API (ASP.NET Core Web API) where the backend validates every move and tracks whose turn it is
- Game state is saved to SQLite so an interrupted game can be reloaded
- Unit and integration tests, run automatically in GitHub Actions

## Tech Stack

- **Backend:** C# / ASP.NET (controllers), SQLite
- **Frontend:** HTML, CSS and JavaScript, calling the backend over HTTP
- **Tests:** xUnit
- **CI:** GitHub Actions
