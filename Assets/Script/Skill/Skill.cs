using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class Skill : ScriptableObject
{

    [SerializeField] int m_skillNumber;
    [SerializeField] string m_skillName;
    [SerializeField] string m_skillDetial;

    [SerializeField] Sprite m_spr;
    [SerializeField] GameObject m_obj;

}
