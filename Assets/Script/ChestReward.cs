using System;
using UnityEngine;
using UnityEngine.UI;

public class ChestReward : MonoBehaviour
{
    [Tooltip("1000MS = 1 second")]
    public float time_wait_MS;
    private Button chestButton;
    private Text txt_timer;

    private ulong LastChestOpen;
    private ulong diff;
    private ulong m;

    private float secondLeft;


    void Start()
    {
        chestButton = GetComponent<Button>();
        txt_timer = GetComponentInChildren<Text>();

        LastChestOpen = ulong.Parse(PlayerPrefs.GetString("LastChestOpen", "0"));

        if (!isChestReady())
        {
            chestButton.interactable = false;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (!chestButton.IsInteractable())
        {
            if (isChestReady())
            {
                chestButton.interactable = true;
                return;
            }

            //set timer
            string t = "";
            //hour
            t += ((int)secondLeft / 3600).ToString("00") + "H : ";
            secondLeft -= ((int)secondLeft / 3600) * 3600;
            //minutes
            t += ((int)secondLeft / 60).ToString("00") + "M : ";
            //second
            t += ((int)secondLeft % 60).ToString("00") + "S"; 

            txt_timer.text = t;

        }
    }

    public void GetReward() //use this for onclick button
    {
        LastChestOpen = (ulong) DateTime.Now.Ticks;
        PlayerPrefs.SetString("LastChestOpen", LastChestOpen.ToString());
        chestButton.interactable = false;

        //give reward to user here
        //example i added coin
        CurrentCoin.AddCoin(5);

    }

    private bool isChestReady()
    {
        diff = (ulong) DateTime.Now.Ticks - LastChestOpen;
           m = diff / TimeSpan.TicksPerMillisecond;

        secondLeft = (float)(time_wait_MS - m) / 1000.0f;

        if (secondLeft < 0)
        {
            txt_timer.text = "Reward Ready!";
            return true;
        }

        return false;
    }
}
