using Godot;
using System;
using System.Linq;

public partial class Flags : TileMapLayer{
	private mainGameCode parent;
	
	public int flagCount = 0;
	
	public override void _Ready()
	{
		parent = GetParent<mainGameCode>();
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
					//check that the tile the player wants to place a flag on is within bounds of the board, and that the game is still running
					if(tilePosition[0]<8 && tilePosition[1]<8 && flagCount<parent.bombsAmount && !parent.gameOver){
						
						//If a flag hasn't already been placed in this square
						if (Array.IndexOf(parent.flagsPlaced, tilePosition) == -1){
							SetCell(tilePosition, 1, (Vector2I)parent.flag, 0);

							//If the location of placed flag coincides with a bomb, a bomb has been found, so score increases
							if(parent.bombLocations.Contains(tilePosition)){
								parent.score++;
								GD.Print(parent.score);
							}

							parent.flagsPlaced[flagCount] = tilePosition;

							flagCount++;

							//Debugging stuff
							GD.Print("Flag #" + flagCount + ": " + tilePosition[0] + "," + tilePosition[1]);

							//Calling method to mark tile as flagged using an instance variable of the tile class
							flagTile(tilePosition, parent.board);
						} else{
							//If this tile is already flagged (position is in the array already), remove the tile
							SetCell(tilePosition, 6, parent.numbers[0], 1);

							//debugging output
							GD.Print("Flag #" + Array.IndexOf(parent.flagsPlaced, tilePosition) + ": " + tilePosition[0] + "," + tilePosition[1] + " REMOVED");
							
							//If this location was initially correct, score is deducted, as this flag has been removed
							if(parent.bombLocations.Contains(tilePosition)){
								parent.score--;
								GD.Print(parent.score);
							}
							
							//Fixing array by clearing data at this removed point and sliding all data down one
							if (flagCount != parent.bombsAmount){
								//Starting at o, the index of the flag being removed within the flags array, data from the next square within the array (o+1) is moved to the current square, o at any given moment
								for(int o = Array.IndexOf(parent.flagsPlaced, tilePosition); o<flagCount-1; o++){
									parent.flagsPlaced[o] = parent.flagsPlaced[o+1];
								}

								//The last point is replaced with 0,0
								parent.flagsPlaced[flagCount - 1] = new Vector2I(0, 0);
								flagCount--;
							}
							//Unflag tile
							flagTile(tilePosition, parent.board);
						}
					//For the case where max flags have been placed and some need to be removed
					} else if(Array.IndexOf(parent.flagsPlaced, tilePosition) != -1){
						SetCell(tilePosition, 6, parent.numbers[0], 1);
						GD.Print("Flag #" + Array.IndexOf(parent.flagsPlaced, tilePosition) + ": " + tilePosition[0] + "," + tilePosition[1] + " REMOVED");
							
						
						if (flagCount != parent.bombsAmount){
							for(int o = Array.IndexOf(parent.flagsPlaced, tilePosition); o<flagCount-1; o++){
								parent.flagsPlaced[o] = parent.flagsPlaced[o+1];
							}

							parent.flagsPlaced[flagCount - 1] = new Vector2I(0, 0);
							flagCount--;
						}
							flagTile(tilePosition, parent.board);
					} else if(parent.bombsAmount==parent.score){
						parent.triggerEnd(parent.score, true);
					}
				}
				
			}
		}
	}

	//Method to check if tile has been revealed already
	public bool tileRevealed(Vector2I pos, Tile[,] map){
		Tile clicked = map[(pos[0]+7),(pos[1]+7)];
		if(clicked.revealed){
			return true;
		} else{
			return false;
		}
	}

	//Method to mark tiles as flagged or unflagged as neccessary within the tile class with the instance boolean variable, "flagged"
	public void flagTile(Vector2I pos, Tile[,] map){
		Tile clicked = map[(pos[0]+7),(pos[1]+7)];
		if(clicked.position == pos){
			if(!clicked.flagged){
				clicked.flagged = true;
			} else{
				clicked.flagged = false;
			}
		}
	}

}