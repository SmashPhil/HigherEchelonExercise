using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace HigherEchelon
{
    public class Button : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI counterLabel;

        public static Button Instance { get; private set; }

        private void Awake()
		{
            Instance = this;
        }

        public void ToggleAnimation()
		{
            //GameOverHandler.Animator.enabled = !GameOverHandler.Animator.enabled;
            GameOverHandler.Rotating = !GameOverHandler.Rotating;
            GameOverHandler.ButtonClicks++;
        }

        public static void NotifyCounterChanged()
		{
            Instance.counterLabel.text = GameOverHandler.ButtonClicks.ToString();
        }
    }
}
