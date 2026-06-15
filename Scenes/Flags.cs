using Godot;
using System;
using System.Linq;

public partial class Flags : TileMapLayer{
	private mainGameCode parent;
	
	public int flagCount = 0;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		parent = GetParent<mainGameCode>();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta){

	}

	//Dropping Flags
	public override void _Input(InputEvent @event){
		bool flagHere = false;
		//Placing flags on rightclicks
		if(@event is InputEventMouseButton mouseButton){
			if(!mouseButton.Pressed) return;
			if(mouseButton.ButtonIndex == MouseButton.Right){
				Vector2I tilePosition = LocalToMap(GetLocalMousePosition());
				
				//If the tile has not been revealed already:

				if(!tileRevealed(tilePosition, parent.board)){
					if(tilePosition[0]<8 && tilePosition[1]<8 && flagCount<parent.bombsAmount){
						
						if (Array.IndexOf(parent.flagsPlaced, tilePosition) == -1){
							SetCell(tilePosition, 1, (Vector2I)parent.flag, 0);
							if(parent.bombLocations.Contains(tilePosition)){
								parent.score++;
							}

							parent.flagsPlaced[flagCount] = tilePosition;

							flagCount++;
							GD.Print("Flag #" + flagCount + ": " + tilePosition[0] + "," + tilePosition[1]);
						} else{
							SetCell(tilePosition, 6, parent.numbers[0], 1);
							GD.Print("Flag #" + Array.IndexOf(parent.flagsPlaced, tilePosition) + ": " + tilePosition[0] + "," + tilePosition[1] + " REMOVED");
							
							
							if (flagCount != parent.bombsAmount){
								for(int o = Array.IndexOf(parent.flagsPlaced, tilePosition); o<flagCount-1; o++){
									parent.flagsPlaced[o] = parent.flagsPlaced[o+1];
								}


								parent.flagsPlaced[flagCount - 1] = new Vector2I(0, 0);
								flagCount--;
							}
						}
						
						//NOTE: MAKE SURE MULTIPLE FLAGS CAN'T BE PLACED ON SAME TILE
						
					} 
				}

				//If the max number of flags haven't been placed, place a flag tile on the existing stone tile
				
			}
		}
	}

	//Method to check if tile has been revealed already
	public bool tileRevealed(Vector2I pos, Tile[,] map){
		for(int r = 0; r<15; r++){
			for(int c = 0; c<15; c++){
				if(map[r,c].position == pos){
					if(map[r,c].revealed){
						return true;
					} else{
						return false;
					}
				}
			}
		}
		return false;
	}

	public void flagTile(Vector2I pos, Tile[,] map){
		for(int r = 0; r<15; r++){
			for(int c = 0; c<15; c++){
				if(map[r,c].position == pos){
					map[r,c].flagged = true;
					break;
				}
			}
		}
	}

}

/*
To do for flags:
- Set a cap on flags to be placed
- Let placed flags be stored for a score thing (have a bomb positions public Vector2I variable)
- Make flags removable by right click ONLY IF a flag is already placed there
*/