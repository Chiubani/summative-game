using Godot;
using System;

public partial class StoneLayer : TileMapLayer
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready(){

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta){
		/*if (Input.IsMouseButtonPressed(MouseButton.Left)){
			Vector2 mousePos = GetViewport().GetMousePosition();
			Vector2I pos = (Vector2I)(mousePos/93.75f);
			SetCell(pos, -1, new Vector2I(0,0), 0);
			SetCell(pos, -1, new Vector2I(1,0), 0);
			//StoneLayer.EraseCell(0, pos);
		}*/
	}

	public override void _Input(InputEvent @event){
		if (Input.IsMouseButtonPressed(MouseButton.Left)){
			Vector2 mousePos = GetViewport().GetMousePosition();
			Vector2I pos = (Vector2I)(mousePos/93.75f);
			SetCell(pos, -1, new Vector2I(0,0), 0);
			SetCell(pos, -1, new Vector2I(1,0), 0);
			//StoneLayer.EraseCell(0, pos);
		}
	}
}
