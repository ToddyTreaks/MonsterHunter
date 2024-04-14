using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpeningScene : MonoBehaviour
{
    [SerializeField] private GameObject logo;
    [SerializeField] private GameObject buttonsMainMenu;

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            logo.GetComponent<Animator>().SetTrigger("OnAnyKeyDown");
            buttonsMainMenu.GetComponent<Animator>().SetTrigger("OnAnyKeyDown");
        }
    }
}
