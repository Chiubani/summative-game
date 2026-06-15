using Godot;
using System;

public partial class Flags : TileMapLayer
{
	private mainGameCode parent;
	
	public int flagCount = 0;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		parent = GetParent<mainGameCode>();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	//Dropping Flags
	public override void _Input(InputEvent @event){
		//Placing flags on rightclicks
		if(@event is InputEventMouseButton mouseButton){
			if(!mouseButton.Pressed) return;
			if(mouseButton.ButtonIndex == MouseButton.Right){
				Vector2I tilePosition = LocalToMap(GetLocalMousePosition());

				//If the max number of flags haven't been placed, place a flag tile on the existing stone tile

				if(tilePosition.x<8 && tilePosition.y<8 && flagCount<parent.bombsAmount){
					SetCell(tilePosition, 1, (Vector2I)parent.flag, 0); //SetCell arguments: tileLayer, tile Vector2I position, atlas ID, atlas coordinates

					for(int a = 0; a<parent.bombsAmount; a++){
						if(tilePosition == parent.bombLocations[a]){
							parent.score++;
						}
					}

					flagCount++;


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

