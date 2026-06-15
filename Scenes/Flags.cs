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
		//Placing flags on rightclicks
		if(@event is InputEventMouseButton mouseButton){
			if(!mouseButton.Pressed) return;
			if(mouseButton.ButtonIndex == MouseButton.Right){
				Vector2I tilePosition = LocalToMap(GetLocalMousePosition());

				//If the max number of flags haven't been placed, place a flag tile on the existing stone tile

				if(tilePosition[0]<8 && tilePosition[1]<8 && flagCount<parent.bombsAmount){
					if(!parent.flagsPlaced.Contains(tilePosition)){
						SetCell(tilePosition, 1, (Vector2I)parent.flag, 0); //SetCell arguments: tileLayer, tile Vector2I position, atlas ID, atlas coordinates

						if(parent.bombLocations.Contains(tilePosition)){
							parent.score++;
						}

						flagCount++;
						GD.Print("Flag #" + flagCount + ": " + tilePosition[0] + "," + tilePosition[1]);
					} else{
						SetCell(tilePosition, 1, parent.numbers[0], 0);
						GD.Print("Flag #" + Array.IndexOf(parent.flagsPlaced, tilePosition) + ": " + tilePosition[0] + "," + tilePosition[1] + " REMOVED");

						for(int o = Array.IndexOf(parent.flagsPlaced, tilePosition); o<flagCount; o++){
							parent.flagsPlaced[o] = parent.flagsPlaced[o+1];
						}
						flagCount--;
						
					}
					
					//NOTE: MAKE SURE MULTIPLE FLAGS CAN'T BE PLACED ON SAME TILE

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


}

