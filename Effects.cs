using Godot;
using System;

public partial class Effects : TileMapLayer
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready(){
		public enum number{
			BOMB = -2;
			NONE = 0;
			FLAG = -1;
			ONE = 1;
			TWO = 2;
			THREE = 3;
			FOUR = 4;
			FIVE = 5;
		}
		
		int[][] tileValues = new int[16][16];
		
		int level = 1;
		int bombLocation[][] = new int[2][55];
		int bombsAmount = 10;
		int bombR = 0;
		int bombC = 0;
		
		for(int r = 0; r<2; r++){
			for(int b = 0; b<bombsAmount; b++){
				
				do{
					bombR = Math.round(15*Math.random()+1);
					bombC = Math.round(15*Math.random()+1);
				} while(bombThere(bombR,bombC,bombLocation));
				bombLocation[bombR][bombC];
			}
		}
		
		public boolean bombThere()
		
		//Number of bombs starts at 10 and increases by 5 each level. Final level has 55 bombs
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
