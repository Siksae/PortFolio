using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class SkillManager : MonoBehaviour
{
    [SerializeField] public GameObject m_SkillWindow;

    [Header("스킬 이미지")]
    [SerializeField] private Sprite Unknown;
    [SerializeField] private List<Sprite> AttackSkill;
    [SerializeField] private List<Sprite> BuffSkill;

    static public SkillManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

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

    private void initSkill()
    {
        m_SkillWindow.GetComponent<Image>().sprite = Unknown;
    }
    public void SkillWindowOutDetail(Sprite _img, GameObject now_obj, GameObject chan_obj)
    {
        now_obj.GetComponent<Image>().sprite = _img;
    }
}
           
