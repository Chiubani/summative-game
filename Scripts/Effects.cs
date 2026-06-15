using Godot;
using System;

public partial class Effects : TileMapLayer{
	private mainGameCode parent;
	
	
	public void createMap(Tile[,] gameBoard, Vector2I[] atlasPos, Vector2I boomPos){
		for(int r = 0; r<15; r++){
			for(int c = 0; c<15; c++){
				//SetCell(tilePosition, 1, (Vector2I)parent.numbers[0], 0);
				SetCell(gameBoard[r,c].position,1,parent.numbers[gameBoard[r,c].type],0);
			}
		}
	}



	public override void _Ready(){
		parent = GetParent<mainGameCode>();
		
	}

	public override void _Process(double delta)
	{
	}
}
//}
//}
