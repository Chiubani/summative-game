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
				SetCell(tilePosition, 1, (Vector2I)parent.numbers[0], 0);
				for(int l = 0; l<15; l++){
					for(int m = 0; m<15; m++){
						if(tilePosition == parent.board[l,m].position){
							if(parent.board[l,m].isBomb){
								GD.Print("BOOM");
							} else if(parent.board[l,m].isEmpty){
								GD.Print("none");
							} else{
								GD.Print(parent.board[l,m].type);
							}
							break;
						} else if(parent.board[l,m].position == null){
							GD.Print("Board is null at x:" + l + " , y:" + m + "" );
						}
					}
				}
			}
		}
	}
}
