using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentCoin : MonoBehaviour
{
    public static CurrentCoin i;
    public int coin;
    private Text coin_txt;

    // Start is called before the first frame update
    void Awake()
    {
        if(i == null)
            i = this;
    }

    private void Start()
    {
        coin_txt = GetComponent<Text>();
    }

    public static void AddCoin(int amount)
    {
        i.coin += amount;
        i.coin_txt.text = "Coin = " + i.coin.ToString();
    }
}
