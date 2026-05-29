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
		}
		
		
		
		
		//Number of bombs starts at 10 and increases by 5 each level. Final level has 55 bombs
		

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
