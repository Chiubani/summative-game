using Godot;
using System;

public partial class Effects : TileMapLayer{
	private mainGameCode parent;

	//Assigning tiles for the effects map; Arguments: 2D Tile array (board created in mainGameCode), Vector2I array of atlas positions for each number on the tileSet (used for placing each number tile), and the bomb's atlas position
	public void createMap(Tile[,] gameBoard, Vector2I[] atlasPos, Vector2I boomPos){
		//Looping through the array
		for(int r = 0; r<15; r++){
			for(int c = 0; c<15; c++){

				Tile currentTile = gameBoard[r,c];

				//Using instance variable "isBomb" the cell is either set to a number of left blank 
				if(!gameBoard[r,c].isBomb){
					SetCell(currentTile.position,6,atlasPos[currentTile.type],0);
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
