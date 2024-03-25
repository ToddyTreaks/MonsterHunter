using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Character.Objet
{
    public class CoffreInventory : MonoBehaviour
    {
        [SerializeField] private GameObject _canva;
        private bool _playerIsAround = false;
        private bool _canvaIsActive = false;
        public static bool _StopMoveInteract;

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
            if (Input.GetButtonDown("Interact") && _playerIsAround) Interact();
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
            _canvaIsActive = !_canvaIsActive;
            PlayerController.StopPlayer = _canvaIsActive;
            _canva.SetActive(_canvaIsActive);
            Cursor.lockState = (_canvaIsActive) ? CursorLockMode.Confined :CursorLockMode.Locked;
        }
        #endregion

    }
}