using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Collect : MonoBehaviour
{
    public static float Money;
    public Text TextMoney;

    void Start()
    {
        TextMoney = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        TextMoney.text = Money.ToString("0");
    }
}
