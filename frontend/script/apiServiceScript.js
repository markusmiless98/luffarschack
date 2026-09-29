// Define the API URL
const apiUrl = 'http://localhost:5090/api/Piece';
const gameUrl = 'http://localhost:5090/api/game';

// Make a GET request
export async function GetFetch()
{
	try{
		const response = await fetch(apiUrl);
		if (!response.ok){
			throw new Error(`Response status: ${response.status}`);
		}
		
		const result = await response.json();
		console.log(result);
	}
	catch (error){
		console.error(error.message);
	}
}

// Make a POST request
export async function MoveRequest(_x,_y,_z)
{
	try{
		const data = { "x":_x, "y":_y, "z":_z };
		console.log(data)
		console.log(JSON.stringify(data))
		const response = await fetch(gameUrl, {
		  method: "POST",
		  headers: {'Content-Type': 'application/json'},
		  body: JSON.stringify(data),
		});
		console.log(response)
		if (!response.ok){
			throw new Error(`Response status: ${response.status}`);
		}
		
		const result = await response.json();
		console.log(result);
	}
	catch (error){
		console.error(error.message);
	}
}