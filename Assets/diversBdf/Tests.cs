using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tests : MonoBehaviour
{

    private Item item;
    // Start is called before the first frame update
    void Start()
    {
        item = GetComponent<Item>();
/*        if (item == null ) Debug.Log("bonne chance mon reuf");
        Debug.Log(item.Name +" id "+ item.GetHashCode());*/
    }
}
