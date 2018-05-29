using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovable {
	void MoveTo (int destination);
	void Initialize (Character character);
	void setPosition (int destination);
}