using Godot;
using System;

public partial class mainGameCode : Node2D{
	public Random rnd = new Random();
	// Called when the node enters the scene tree for the first time.
	public int[,] gameMap = new int[15,15];
	public Tile[,] board = new Tile[15,15];

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
			
			for(int r = 0; r<15; r++){
				for (int c = 0; c<15; c++){
					GD.Print(gameMap[r,c]);
				}
				GD.Print();
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
			 
			 //Console.WriteLine(count);
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
					y++;
				}
				x++;
				y = -7;
			}
		}

		/*


		*/

		//Tile class, containing all properties of each tile
		public class Tile{
			int type;
			Vector2I position;
			bool flagged;
			bool revealed;
			bool isMine;

			public Tile(){
				this.type = 0;
				this.position = Vector2I(0,0);
				this.flagged = false;
				this.revealed = false;
				this.isMine = false;
			}

			public Tile(int inputX, int inputY, int inputType){
				this.position = Vector2I(inputX,inputY);
				this.type = inputType;
				this.flagged = false;
				this.revealed = false;
				if(inputType == -2){
					this.isMine = true;
				} else{
					this.isMine = false;
				}
			}
		}

	public override void _Ready(){
		int bombsAmount = 10;
		setupMap(gameMap,bombsAmount);
		assignTiles(board);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta){
	
	}
}
