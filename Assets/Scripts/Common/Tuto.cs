using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Tuto : MonoBehaviour
{
    [SerializeField] GameObject canvas;

    private bool isActive =false;
    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            canvas.SetActive(true);
            isActive = true;
            PlayerController.StopPlayer = true;
            Time.timeScale = 0;
        }
    }

    void Update()
    {
        if (isActive)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                canvas.SetActive(false);
                Destroy(gameObject);
                PlayerController.StopPlayer = false;
                Time.timeScale = 1;
            }
        }
    }

}
