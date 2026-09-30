// Define the API URL
const apiUrl = 'http://localhost:5090/api/Piece';
const gameUrl = 'http://localhost:5090/api/game';

// Make a GET request
export async function GetFetch()
{
	try{
		const response = await fetch(gameUrl);
		if (!response.ok){
			throw new Error(`Response status: ${response.status}`);
		}
		
		const result = await response.json();
		console.log(result)
		return result;
	}
	catch (error){
		console.error(error.message);
	}
}

// Make a POST request
export async function MoveRequest(x,y,z)
{
	try{
		const data = { "x":x, "y":y, "z":z };
		//console.log(data)
		//console.log(JSON.stringify(data))
		const response = await fetch(gameUrl, {
		  method: "POST",
		  headers: {'Content-Type': 'application/json'},
		  body: JSON.stringify(data),
		});
		//console.log(response)
		if (!response.ok){
			throw new Error(`Response status: ${response.status}`);
		}
		
		return response;
	}
	catch (error){
		console.error(error.message);
	}
}

// POST /api/game/new?players=?
export async function NewGameRequest(num)
{
	try{
		if (num == null) num = 2;
		
		let _url = gameUrl + "/new?players=" + num.toString();
		const response = await fetch(_url, {
		  method: "POST",
		});
		console.log(response)
		if (!response.ok){
			throw new Error(`Response status: ${response.status}`);
		}
		const _fet = await GetFetch();
		console.log(_fet)
	}
	catch (error){
		console.error(error.message);
	}
}
