using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillPreviewManager : MonoBehaviour
{

    [SerializeField] List<SkillPreview> skillPreviews = new List<SkillPreview>();

    public void ShowSkillPreviewsOf(Equip equip)
    {
        gameObject.SetActive(true);
        //skillPreviews[0].getText().text = equip.GetMeleeEffect().GetSkillName();
        //skillPreviews[1].getText().text = equip.GetRangedEffect().GetSkillName();
        //skillPreviews[2].getText().text = equip.GetSelfEffect().GetSkillName();
        //skillPreviews[3].getText().text = equip.GetAlliesEffect().GetSkillName();

        //TODO decidir se vai se manter essas cores
        skillPreviews[0].getCircle().color = new Color(0.925f, 0.258f, 0.258f, 1);
        skillPreviews[1].getCircle().color = new Color(0.427f, 0.745f, 0.266f, 1);
        skillPreviews[2].getCircle().color = new Color(0.309f, 0.380f, 0.674f, 1);
        skillPreviews[3].getCircle().color = new Color(0.952f, 0.921f, 0.235f, 1);

        HideSkillPreviews();

        for (int i = 0; i < equip.GetAllSkills().Count; i++)
        {
            skillPreviews[i].gameObject.SetActive(true);
            skillPreviews[i].getText().text = equip.GetAllSkills()[i].GetSkillName();
        }
    }

    public void HideSkillPreviewManager()
    {
        gameObject.SetActive(false);
    }

    void HideSkillPreviews()
    {
        foreach (SkillPreview skillPreview in skillPreviews)
        {
            skillPreview.gameObject.SetActive(false);
        }
    }
}
