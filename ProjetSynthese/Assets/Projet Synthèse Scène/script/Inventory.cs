using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Inventory : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _CountCoinText;
    [SerializeField] private int _CountCoin;
    public static Inventory _Instance;

    private void Awake()
    {
        if (_Instance != null)
        {
            Debug.Log("The is more inventory instance in the scene");
            return;
        }
        _Instance = this;
    }

    public void Addcoin(int coin)
    {
        _CountCoin += coin;
        _CountCoinText.text = _CountCoin.ToString();

    }

    public static implicit operator Inventory(PlayerHealth v)
    {
        throw new NotImplementedException();
    }
}
