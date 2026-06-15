using Godot;
using System;

public partial class Flags : TileMapLayer
{
	private mainGameCode parent;
	
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
				SetCell(tilePosition, 1, (Vector2I)parent.flag, 0); //SetCell arguments: tileLayer, tile Vector2I position, atlas ID, atlas coordinates
			}
		}
	}
}
