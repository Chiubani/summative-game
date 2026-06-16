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
					SetCell(tilePosition, 6, (Vector2I)parent.numbers[0], 0);
					//Storing data of revealed tiles for flags
					for(int l = 0; l<15; l++){
						for(int m = 0; m<15; m++){
							if(tilePosition == parent.board[l,m].position){
								parent.board[l,m].revealed = true;
							}
						}
					}
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
	}
}
