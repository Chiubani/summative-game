using Godot;
using System;

public partial class Tile : Resource{
    //public class Tile{
			public int type;
			public Vector2I position = new Vector2I(0,0);
			public bool flagged;
			public bool revealed;
			public bool isBomb;
			public bool isEmpty;

			public Tile(){
				this.type = 0;
				this.position = new Vector2I(0,0);
				this.flagged = false;
				this.revealed = false;
				this.isBomb = false;
				this.isEmpty = true;
			}

			public Tile(int inputX, int inputY, int inputType){
				this.position = new Vector2I(inputX,inputY);
				this.type = inputType;
				this.flagged = false;
				this.revealed = false;
				if(inputType == -2){
					this.isBomb = true;
				} else{
					this.isBomb = false;
					if(inputType == 0){
						this.isEmpty = true;
					}
				}
			}
		//}
}
