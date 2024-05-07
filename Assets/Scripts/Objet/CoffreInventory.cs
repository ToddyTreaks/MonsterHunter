using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

namespace Assets.Scripts.Character.Objet
{
    public class CoffreInventory : MonoBehaviour
    {

        [SerializeField] private GameObject _canva;
        [SerializeField] private Animator _animator;
        private bool _playerIsAround = false;
        private bool _canvaIsActive = false;

        private bool _coffreIsOpen = false;
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
            if ( PlayerController.tryToInteract && _playerIsAround) Interact();
        }

        void OnTriggerEnter()
        {
            _playerIsAround = true;
        }

/*        void OnTriggerExit()
        {
            _playerIsAround = false;
            _canva.SetActive(false);
        }*/
        private void Interact()
        {
            if (_coffreIsOpen)
            {
                OnCLose();
            }
            else
            {
                OnOpen();
            }
        }

        private void OnOpen()
        {
            _animator.SetBool("ChestOpen",true);
            _coffreIsOpen = true;
            _canvaIsActive = true;
            PlayerController.StopPlayer = true;
            _canva.SetActive(true);
            Cursor.lockState = (_canvaIsActive) ? CursorLockMode.Confined : CursorLockMode.Locked;
        }

        private void OnCLose()
        {
            _animator.SetBool("ChestOpen", false);
            _coffreIsOpen = false;
            _canvaIsActive = false;
            PlayerController.StopPlayer = false;
            _canva.SetActive(false);
            Cursor.lockState = (_canvaIsActive) ? CursorLockMode.Confined : CursorLockMode.Locked;
        }
        #endregion

    }
}