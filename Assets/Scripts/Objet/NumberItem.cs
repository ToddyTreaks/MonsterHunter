using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NumberItem : MonoBehaviour
{
    private TextMeshProUGUI _nb;

    private void Start()
    {
        _nb = GetComponent<TextMeshProUGUI>();
    }

    public void PrintAmount(int amount)
    {
        _nb.text = amount.ToString();
    }

}
