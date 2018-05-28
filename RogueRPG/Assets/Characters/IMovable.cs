using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovable {
	void MoveTo (int destination);
	void setPosition (int destination);
}