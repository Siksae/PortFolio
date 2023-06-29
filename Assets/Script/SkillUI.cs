using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class SkillUI : MonoBehaviour , IPointerEnterHandler , IPointerClickHandler, IPointerExitHandler
{

    private Image Skill_1;
    private Sprite spr_1;

    class Skill
    {
        [Header("스킬이름 이미지")]
        private Image img_skillImage;
        private string m_skillName;
    }
    // Start is called before the first frame update
    void Start()
    {
        Skill_1 = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Color color = Skill_1.color;
        color.a = 2f;
        Skill_1.color = color;

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }
}
