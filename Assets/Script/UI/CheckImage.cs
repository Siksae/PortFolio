using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class CheckImage : MonoBehaviour , IPointerEnterHandler , IPointerClickHandler, IPointerExitHandler
{

    private Image Skill_1;

    // Start is called before the first frame update
    void Start()
    {
        Skill_1 = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        Skill_1.color = Color.red;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Skill_1.color = Color.white;
    }
}
