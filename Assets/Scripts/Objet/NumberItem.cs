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
        if ( _nb == null ) Debug.LogError("not find TextMeshProUGUI in " + transform.name);
    }

    public void PrintAmount(int amount)
    {
        _nb.text = amount.ToString();
    }

}
