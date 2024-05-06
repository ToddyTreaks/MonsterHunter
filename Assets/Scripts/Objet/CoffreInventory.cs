﻿using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Character.Objet
{
    public class CoffreInventory : MonoBehaviour
    {

        [SerializeField] private GameObject _canva;
        private bool _playerIsAround = false;
        private bool _canvaIsActive = false;

        private void Start()
        {
            _canva.SetActive(false);
        }
        private void Update()
        {
            inputInteract();
        }

        #region input
        private void inputInteract()
        {
            Debug.Log(PlayerController.isInteract);
            if ( PlayerController.isInteract && _playerIsAround) Interact();
        }

        void OnTriggerEnter()
        {
            _playerIsAround = true;
        }

        void OnTriggerExit()
        {
            _playerIsAround = false;
            _canva.SetActive(false);
        }
        private void Interact()
        {
            Debug.Log("h");
            PlayerController.isInteract = false;
            _canvaIsActive = !_canvaIsActive;
            PlayerController.StopPlayer = _canvaIsActive;
            _canva.SetActive(_canvaIsActive);
            Cursor.lockState = (_canvaIsActive) ? CursorLockMode.Confined :CursorLockMode.Locked;
        }
        #endregion

    }
}