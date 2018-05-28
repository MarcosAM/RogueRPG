using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour, IMovable {

	Character character;
	int position = 0;
	Battleground battleground;

	public void MoveTo(int destination){
		if(battleground == null){
			battleground = FindObjectOfType<Battleground> ();
			battleground.MoveCharacterTo (character,destination);
		}
	}

	public void Initialize(Character character){
		this.character = character;
	}

	public void setPosition(int destination){
		this.position = destination;
	}
}