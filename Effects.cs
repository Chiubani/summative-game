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
		
		for (int i = 0; i<16; i++)
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
