using Godot;
using System;

public partial class StoneLayer : TileMapLayer
{
	private mainGameCode parent;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready(){
		parent = GetParent<mainGameCode>();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta){
		
	}

	public override void _Input(InputEvent @event){
		if(@event is InputEventMouseButton mouseButton){
			if(!mouseButton.Pressed) return;
			if(mouseButton.ButtonIndex == MouseButton.Left){
				Vector2I tilePosition = LocalToMap(GetLocalMousePosition());
				//SetCell(0, tilePosition, 0, Vector2I.Zero); //SetCell arguments: tileLayer, tile Vector2I position, atlas ID, atlas coordinates
				//SetCell(tilePosition, 0, new Vector2I(0,0), 0);
			}
		}
	}
}
