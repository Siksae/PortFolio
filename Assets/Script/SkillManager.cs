using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class SkillManager : MonoBehaviour
{
    [SerializeField] private GameObject m_SkillWindow;


    

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        callSkillWindow();
    }

    private void callSkillWindow()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (m_SkillWindow.activeSelf == true)
            {
                m_SkillWindow.SetActive(false);
            }
            else
            {
                m_SkillWindow.SetActive(true);
            }
        }
    }
}
           
