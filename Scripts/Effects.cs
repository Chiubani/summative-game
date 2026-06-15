using Godot;
using System;

public partial class Effects : TileMapLayer{
	private mainGameCode parent;
	
	public void createMap(Tile[,] gameBoard, Vector2I[,] atlasPos, Vector2I boomPos){
		for(int r = 0; r<15; r++){
			for(int c = 0; c<15; c++){
				
			}
		}
	}



	public override void _Ready(){
		parent = GetParent<mainGameCode>();
		createMap(parent.board, parent.numbers, parent.bomb);
	}

	public override void _Process(double delta)
	{
	}
}
//}
//}
