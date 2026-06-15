using Godot;
using System;

public partial class Effects : TileMapLayer{
	private mainGameCode parent;
	

	//Assigning tiles for the effects map
	public void createMap(Tile[,] gameBoard, Vector2I[] atlasPos, Vector2I boomPos){
		for(int r = 0; r<15; r++){
			for(int c = 0; c<15; c++){

				Tile currentTile = gameBoard[r,c];

				if(!gameBoard[r,c].isBomb){
					SetCell(currentTile.position,6,parent.numbers[currentTile.type],0);
				} else{
					SetCell(currentTile.position,6,boomPos,0);
				}
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
