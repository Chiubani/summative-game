using Godot;
using System;

public partial class StoneLayer : TileMapLayer
{
	private mainGameCode parent;

	public override void _Ready(){
		parent = GetParent<mainGameCode>();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta){
		
	}

	public override void _Input(InputEvent @event){
		//Revealing Tiles on left click
		if(@event is InputEventMouseButton mouseButton){
			if(!mouseButton.Pressed) return;
			if(mouseButton.ButtonIndex == MouseButton.Left){
				Vector2I tilePosition = LocalToMap(GetLocalMousePosition());
				if(!tileFlagged(tilePosition, parent.board) && !parent.gameOver){
					revealTiles(tilePosition, parent.board);
				}
				checkLoss(tilePosition, parent.board);
			}
		}
	}

	//Check if tile has already been flagged by sorting through the array with a loop
	public bool tileFlagged(Vector2I pos, Tile[,] map){
		Tile clicked = map[(pos[0]+7),(pos[1]+7)];
		if(clicked.flagged){
			return true;
		} else{
			return false;
		}

		return true;
	}

	//Recursive method: Method that calls itself
	//After each tile is revealed, check surrounding tiles for type 0, then reveal them
	public void revealTiles(Vector2I pos, Tile[,] map){
		SetCell(pos, 6, (Vector2I)parent.numbers[0], 0);
		Tile clicked = map[(pos[0]+7),(pos[1]+7)];
		clicked.revealed = true;
		int clickedX = clicked.position[0]+7;
		int clickedY = clicked.position[1]+7;

		if(clicked.type == 0){
			//Check surrounding tiles (similar code to mainGameCode for identifying surrounding bombs)
			for(int a = -1; a<=1; a++){
				for(int b = -1; b<=1; b++){
					//Checking if tile is on the map
					if((a + clickedX < 15 && a + clickedX > -1)&&(b+clickedY < 15 && clickedY+b >-1)){
						if(map[(clickedX+a),(clickedY+b)].type==0 && !map[(clickedX+a),(clickedY+b)].revealed){
							revealTiles(map[(clickedX+a),(clickedY+b)].position, map);
						} else{
							SetCell(map[(clickedX+a),(clickedY+b)].position, 6, (Vector2I)parent.numbers[0], 0);
						}
					}
				}
			}
		}
	}

	//Check if a bomb tile has been clicked
	public void checkLoss(Vector2I pos, Tile[,] map){
		Tile clicked = map[(pos[0]+7),(pos[1]+7)];
		if(clicked.isBomb){
			triggerEndGame(parent.bombLocations);
		}
	}

	public void triggerEndGame(Vector2I[] bombsL){
		for(int x = 0; x<parent.bombsAmount; x++){
			SetCell(bombsL[x], 6, (Vector2I)parent.numbers[0], 1);
		}
		parent.gameOver = true;
		//GetTree().ChangeSceneToFile("res://Scenes/tile_map.tscn");
	}
}
