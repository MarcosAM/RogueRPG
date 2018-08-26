using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MomentumBar : MonoBehaviour {

	float targetAmount = 0;
	float currentDisplayedAmount = 0;
	float maxAmount = 100;
	[SerializeField]float fillSpeed;
	bool filling = false;
	[SerializeField]Slider momentumBar;

	public void changeMomentumBy (float value){
		targetAmount += value;
		if (targetAmount > maxAmount)
			targetAmount = maxAmount;
		if (targetAmount < 0)
			targetAmount = 0;
		if(!filling){
			StartCoroutine (fillBar());
		}
	}

//	void Update(){
//		if(Input.GetMouseButtonUp(0)){
//			print ("Aumenta!");
//			addMomentum (10);
//		}
//		if (Input.GetMouseButtonUp (1)) {
//			print ("Diminui!");
//			addMomentum (-10);
//		}
//	}

	IEnumerator fillBar(){
		filling = true;
		while (currentDisplayedAmount != targetAmount) {
			currentDisplayedAmount = Mathf.MoveTowards (currentDisplayedAmount,targetAmount,Time.deltaTime * fillSpeed);
			momentumBar.value = currentDisplayedAmount / maxAmount;
			yield return new WaitForSeconds (0);
		}
		filling = false;
	}

	public void Initialize(float maxAmount){
		momentumBar = GetComponent<Slider> ();
		filling = false;
		targetAmount = 0;
		currentDisplayedAmount = 0;
		this.maxAmount = maxAmount;
//		changeMomentumBy (50);
	}
}