using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class SkillManager : MonoBehaviour, IDragHandler, IPointerClickHandler
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

    public void initSkill(GameObject _obj)
    {
        _obj.GetComponent<Image>().sprite = Unknown;
    }
    public void SkillWindowOutDetail(Sprite _img)
    {
        GameObject[] img = m_SkillWindow.GetComponentsInChildren<GameObject>();
        for(int iNum = 0; iNum < img.Length; iNum++)
        {
            if (img[iNum].ToString().Contains("SkillImage"))
            {
                img[iNum].GetComponent<Image>().sprite = _img;
                break;
            }
        }
    }
    private void MoveWindow(Vector3 _pos)
    {
        m_SkillWindow.transform.position = _pos;
    }

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        Vector3 pos = eventData.pressPosition;
        MoveWindow(pos);
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        Vector3 pos = eventData.position;
        
    }
}
           
