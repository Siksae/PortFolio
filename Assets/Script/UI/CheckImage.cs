using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class CheckImage : MonoBehaviour , IPointerEnterHandler , IPointerClickHandler, IPointerExitHandler
{

    [SerializeField] private GameObject m_checkImage;
    private Image m_imgSkill;
    private GameObject m_obj;
    private SkillManager m_skillManager;
    private Sprite m_sprSkill;
    // Start is called before the first frame update
    void Start()
    {
        m_obj = GetComponent<GameObject>();
        m_imgSkill = GetComponent<Image>();
        m_skillManager = SkillManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        m_imgSkill.color = Color.red;
        m_obj = eventData.pointerEnter;
        m_sprSkill = m_obj.GetComponent<Image>().sprite;
        if (m_obj.ToString().Contains("CheckSkill"))
        {
            
            m_skillManager.SkillWindowOutDetail(m_sprSkill, m_checkImage,m_obj);
        }
    }
    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        m_obj = eventData.pointerClick;
        if (m_obj.ToString().Contains("SkillUI"))
        {
            if (m_skillManager.m_SkillWindow.activeSelf == true)
            {
                m_skillManager.m_SkillWindow.SetActive(false);
            }
            else
            {
                m_skillManager.m_SkillWindow.SetActive(true);
            }
        }
        else if(m_obj.ToString().Contains("Exit"))
        {

        }
        
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        m_imgSkill.color = Color.white;
    }
}
