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
				if(!tileFlagged(tilePosition, parent.board)){
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
			}
		}
	}

	public bool tileFlagged(Vector2I pos, Tile[,] map){
		for(int r = 0; r<15; r++){
			for(int c = 0; c<15; c++){
				if(map[r,c].position == pos){
					if(map[r,c].flagged==true){
						GD.Print("Flagged");
						return true;
					} else{
						GD.Print("Not Flagged");
						return false;
					}
				}
			}
		}
		return false;
	}
}
