using Godot;
using System;


public partial class mainGameCode : Node2D{
	public Random rnd = new Random();

	//Game board arrays => Visible Tiles and numbers(with bombs)
	
	public int[,] gameMap = new int[15,15];
	
	public Tile[,] board = new Tile[15,15];

	//Atlas Coordinates of important tiles on the tileset - Vector2I
	public Vector2 flag = new Vector2(5f,0f);
	
	public Vector2I[] numbers = {new Vector2I(7,0), new Vector2I(0,1), new Vector2I(1,1), new Vector2I(2,1), new Vector2I(3,1), new Vector2I(4,1), new Vector2I(5,1), new Vector2I(6,1)};
	
	public Vector2I bomb = new Vector2I(4,0);

	//Number of bombs on the board, and player's score
	public int bombsAmount = 20;

	public int score = 0;

	//Stores where bombs and flags are placed on the grid
	public Vector2I[] bombLocations = new Vector2I[20];

	public Vector2I[] flagsPlaced = new Vector2I[20];

	//Used for indexing when assigning bombs into bombLocations array
	public int bombCounter = 0;

	//Set off when player looses or wins
	public bool gameOver = false;

	public bool gameWin = false;

		//METHODS

		/*
		1. Game Setup
			Overview: bomb locations are decided, nearby numbers are placed. All starting game information is stored

		*/
		public void setupMap(int[,] gameMap, int bombsAmount){
			int[,] bombLocation = new int[2,bombsAmount]; //tracks where bombs have been placed with x and y coordinates so multiple aren't placed on same square
			//int bombsAmount = 10;
			int bombR = 0;
			int bombC = 0;
			
			//Loop places bombs in 
			for(int b = 0; b<bombsAmount; b++){
				do{
					bombR = rnd.Next(0,15);
					bombC = rnd.Next(0,15);
				} while(bombThere(bombR,bombC,bombLocation,b));
				gameMap[bombR,bombC] = -2;
				bombLocation[0,b] = bombR;
				bombLocation[1,b] = bombC;
			}
			
			for(int x = 0; x<15; x++){
				for (int y = 0; y<15; y++){
					if(gameMap[x,y]!=-2){
						gameMap[x,y] = getSquareNo(x,y,gameMap);
					}
				}
			}
			
			
		}

		/*
		2. Bomb There
			Overview: Ensuring multiple bombs are not placed in the same square by checking if a bomb of same coordinates has already been placed in one of the squares
		*/

		public bool bombThere(int bombR, int bombC, int[,] arr, int b){
			for(int i = 0; i<b;i++){
				if(arr[0,i]==bombR && arr[1,i]==bombC){
					return true;
				}
			}
			return false;
		}

		/*
		3. Get Square Number
			Overview: Counting number of bombs around the square to put into the gameMap array. This will eventually be the number on this square in the actual game, if any
		*/
		
		public int getSquareNo(int ogX, int ogY, int[,] map){
			int count = 0;
			
			/* Explanation:
				ogX is the x value of this square. a represents moving away from the current tile horizontally. ogX - 1 would check one of the squares to the left of x, and ogX+1 would chec one of the squares to the right
				ogY is the y value of this square. b represents moving vertically from the tile, with the same logic applying
				Altogether, within this code, all the squares touching the tile to the left are checked first, then the middle ones, then the ones to the right
				Number of touching bombs counted is returned and put into the gameMap array
			*/
			for(int a = -1; a<=1; a++){
				for(int b = -1; b<=1; b++){
					if((a+ogX<15&&a+ogX>-1)&&(b+ogY<15&&ogY+b>-1)){
						if(map[(ogX+a),(ogY+b)]==-2){
							count++;
						}
					}
				}
			 }
			 
			 return count;
		}

		/*
		4. Assign Tiles
			Overview: Assigning data to each tile in the tileMap
		*/

		public void assignTiles(Tile[,] gameBoard){
			int x = -7;
			int y = -7;
			int tileType = 0;
			for(int i = 0; i<15; i++){
				for(int j = 0; j<15; j++){
					gameBoard[i,j] = new Tile(x,y,gameMap[i,j]);
					if(gameMap[i,j]==-2){
						bombLocations[bombCounter] = new Vector2I(x,y);
						bombCounter++;
					}
					y++;
				}
				x++;
				y = -7;
			}
			Effects effectsLayer = GetNode<Effects>("Effects");
			effectsLayer.createMap(board, numbers, bomb);
		}

	public override void _Ready(){
		//Set up game map
		setupMap(gameMap,bombsAmount);
		//Assign tiles
		assignTiles(board);
	}

	public void triggerEnd(int score, bool won){
		//Panels for end message
		Window popUpPanel = GetNode<Window>("End");
		Label messageLabel = GetNode<Label>("End/Label");
		//Display end window
		popUpPanel.Visible = true;
		if(won){
			messageLabel.Text = "YOU WIN! \n You flagged all 20 bombs! \n GREAT JOB!";
		} else{
			messageLabel.Text = "BOOM!! \n You hit a bomb! \n You Lose \n Your SCORE: " + score;
		}
	}
}
