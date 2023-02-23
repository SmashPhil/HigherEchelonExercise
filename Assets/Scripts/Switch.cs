using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

namespace HigherEchelon
{
	public class Switch : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
	{
		private const float TimeToResetSwitch = 2;
		private const string DialRotationParameter = "Clockwise";

		[SerializeField]
		private TextMeshProUGUI counterLabel;

		private float onPointerDownMouseY = float.MaxValue;
		
		private bool Resetting { get; set; } = false;

		public static Switch Instance { get; private set; }

		private void Awake()
		{
			Instance = this;
			//clockwise = GameOverHandler.Animator.GetBool(DialRotationParameter);
		}

		public void OnPointerDown(PointerEventData eventData)
		{
			onPointerDownMouseY = Input.mousePosition.y;
		}

		public void OnPointerUp(PointerEventData eventData)
		{
			if (Input.mousePosition.y > onPointerDownMouseY && !Resetting)
			{
				GameOverHandler.SwitchFlicks++;
				counterLabel.text = GameOverHandler.SwitchFlicks.ToString();
				StartCoroutine(SwitchResetRoutine());
			}
		}

		private IEnumerator SwitchResetRoutine()
		{
			Resetting = true;
			{
				GameOverHandler.Clockwise = !GameOverHandler.Clockwise;
				//GameOverHandler.Animator.SetBool(DialRotationParameter, clockwise);
				//GameOverHandler.Animator.Play("Default", -1, 0);

				//Flip switch
				transform.Rotate(new Vector3(0, 0, 90), Space.Self);

				yield return new WaitForSeconds(TimeToResetSwitch);

				transform.Rotate(new Vector3(0, 0, -90), Space.Self);
			}
			Resetting = false;
		}

		public static void NotifyCounterChanged()
		{
			Instance.counterLabel.text = GameOverHandler.ButtonClicks.ToString();
		}
	}
}
