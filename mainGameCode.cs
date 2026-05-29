using Godot;
using System;

public partial class MainGameCode : Node2D{
	public mainGameCode(){
		Random rnd = new Random();
	}
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		int level = 1;
		int[][] gameMap = new int[15][15];
		int[][] bombLocation = new int[2][55]; //tracks where bombs have been placed with x and y coordinates so multiple aren't placed on same square
		int bombsAmount = 10;
		int bombR = 0;
		int bombC = 0;
		
		//Loop places bombs in 
		for(int b = 0; b<bombsAmount; b++){
			do{
				bombR = rnd.Next(1,16);
				bombC = Math.round(15*Math.random()+1);
			} while(bombThere(bombX,bombY,bombLocation,b));
			gameMap[bombR][bombC] = -2;
			bombLocation[0][b] = bombR;
			bombLocation[1][b] = bombC;
		}
		for(int r = 0; r<15; r++){
			for (int c = 0; c<15; c++){
				Console.WriteLine(gameMap[r][c]);
			}
		}
	}
		
	//Making sure multiple bombs are not placed in the same square by checking if a bomb of same coordinates has been placed in one of the squares
	public boolean bombThere(int bombR, int bombC, int[][] arr, int b){
		for(int i = 0; i<b;i++){
			//if 
			if(arr[0][i]==bombR && arr[1][i]==bombC){
				return true;
			}
		}
		return false;
	}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
