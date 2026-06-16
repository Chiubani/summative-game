using Godot;
using System;

//Tile class, made up of different instance variables that store each tile's properties on the game map

public partial class Tile : Resource{
            //Numerical type assigned to each square in the gameMap[,] array in mainGameCode, where 0 means an empty square, -2 means a bomb, and numerical values from 1 to 8 show number of surrounding bombs
			public int type;
            
            //
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
}
