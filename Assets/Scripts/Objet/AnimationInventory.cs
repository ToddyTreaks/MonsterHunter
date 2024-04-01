using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Character.Objet
{
    public class AnimationInventory : MonoBehaviour
    {
        private Animator _animator;

        private bool isOpen = false;
        private void Start()
        {
            _animator = GetComponent<Animator>();
            _animator.Play(AnimatorParametersInventory.Close);
        }


        public void TriggerInventoryAnim()
        {
            isOpen = !isOpen;
            AnimPlay();
        }

        private void AnimPlay()
        {
            if (isOpen) _animator.Play(AnimatorParametersInventory.Open);
            else _animator.Play(AnimatorParametersInventory.Close);
        }
    }

    public static partial class AnimatorParametersInventory
    {
        public static int Open = Animator.StringToHash("Open");
        public static int Close = Animator.StringToHash("Close");
    }
}