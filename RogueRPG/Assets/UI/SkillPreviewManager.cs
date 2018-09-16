using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillPreviewManager : MonoBehaviour {

	[SerializeField] List<SkillPreview> skillPreviews = new List<SkillPreview> ();

	public void showSkillPreviewsOf(Equip equip){
		gameObject.SetActive(true);
		skillPreviews [0].getText ().text = equip.getMeleeEffect ().getEffectName ();
		skillPreviews [1].getText ().text = equip.getRangedEffect ().getEffectName ();
		skillPreviews [2].getText ().text = equip.getSelfEffect ().getEffectName ();
		skillPreviews [3].getText ().text = equip.getAlliesEffect ().getEffectName ();
		skillPreviews [0].getCircle().color = new Color(0.925f,0.258f,0.258f,1);
		skillPreviews [1].getCircle().color = new Color(0.427f,0.745f,0.266f,1);
		skillPreviews [2].getCircle().color = new Color(0.309f,0.380f,0.674f,1);
		skillPreviews [3].getCircle().color = new Color(0.952f,0.921f,0.235f,1);
	}

	public void hideSkillPreviews (){
		gameObject.SetActive(false);
	}
}
