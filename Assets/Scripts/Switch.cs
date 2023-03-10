using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

namespace HigherEchelon
{
	public class Switch : EventCounter, IPointerDownHandler, IPointerUpHandler
	{
		private const float TimeToResetSwitch = 2;
		private const string DialRotationParameter = "Clockwise";

		private float onPointerDownMouseY = float.MaxValue;
		
		private bool Resetting { get; set; } = false;

		public void OnPointerDown(PointerEventData eventData)
		{
			onPointerDownMouseY = Input.mousePosition.y;
		}

		public void OnPointerUp(PointerEventData eventData)
		{
			if (Input.mousePosition.y > onPointerDownMouseY && !Resetting)
			{
				Count++;
				StartCoroutine(SwitchResetRoutine());
			}
		}

		private IEnumerator SwitchResetRoutine()
		{
			Resetting = true;
			{
				DialHandler.Clockwise = !DialHandler.Clockwise;
				
				//Flip switch
				transform.Rotate(new Vector3(0, 0, 90), Space.Self);

				yield return new WaitForSeconds(TimeToResetSwitch);

				transform.Rotate(new Vector3(0, 0, -90), Space.Self);
			}
			Resetting = false;
		}
	}
}
