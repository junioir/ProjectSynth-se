using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


public class SkillDisplaying : MonoBehaviour
{
    [SerializeField] private Image _skillCoolDown;
    [SerializeField] private frieBall _fireBall;
   void Update()
    {
        _skillCoolDown.fillAmount = _fireBall.GetCoolDownRatio();
    }
}
