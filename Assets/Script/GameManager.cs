using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] public List<GameObject> m_playerfx;
    [SerializeField] private List<GameObject> m_playerSkillfx;
    [SerializeField] private Transform m_trsGameObj;
    [SerializeField] private Transform m_PlayerfxObj;
    [SerializeField] private GameObject m_objPlayer;
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
        
    }

    public void spawndashingfx()
    {
        GameObject obj = Instantiate(m_playerfx[0], m_PlayerfxObj.position, Quaternion.identity, m_trsGameObj);
        Destroy(obj, 0.5f);
    }


}
