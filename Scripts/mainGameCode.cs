using Godot;
using System;

public partial class mainGameCode : Node2D{
	public static Random rnd = new Random();
	// Called when the node enters the scene tree for the first time.
	public override void _Ready(){
			int[,] gameMap = new int[15,15];
			int[,] bombLocation = new int[2,55]; //tracks where bombs have been placed with x and y coordinates so multiple aren't placed on same square
			int bombsAmount = 10;
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
		//Making sure multiple bombs are not placed in the same square by checking if a bomb of same coordinates has been placed in one of the squares
		public static bool bombThere(int bombR, int bombC, int[,] arr, int b){
			for(int i = 0; i<b;i++){
				if(arr[0,i]==bombR && arr[1,i]==bombC){
					return true;
				}
			}
			return false;
		}
		
		public static int getSquareNo(int ogX, int ogY, int[,] map){
			int count = 0;
			
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

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta){
	
	}
}
