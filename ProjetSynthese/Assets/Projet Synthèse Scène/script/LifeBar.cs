using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeBar : MonoBehaviour
{
    [SerializeField] private player _player;
    private float _health;
    void Update()
    {
        _health = _player.Getlife();
        this.GetComponent<Image>().fillAmount = _health / 100f;
    }

}
