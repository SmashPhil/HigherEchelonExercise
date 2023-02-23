using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HigherEchelon
{
	public class GameOverHandler : MonoBehaviour
	{
		private const int TotalClicksAlotted = 10;
		private const float TimeFullRotation = (1 / 3f) * 360; //Rotate 360deg once per 3 seconds

		[SerializeField]
		private GameObject gameOverPrompt;
		[SerializeField]
		private Animator dialAnimator;

		private static int buttonClicks;
		private static int switchFlicks;

		public static Animator Animator => Instance.dialAnimator;

		public static GameOverHandler Instance { get; private set; } //Singleton instance

		public static bool Clockwise { get; set; } = true;

		public static bool Rotating { get; set; } = false;

		public static int ButtonClicks
		{
			get
			{
				return buttonClicks;
			}
			set
			{
				if (value == ButtonClicks)
				{
					return;
				}
				buttonClicks = value;
				VerifyQuitConditions();
				Button.NotifyCounterChanged();
			}
		}

		public static int SwitchFlicks
		{
			get
			{
				return switchFlicks;
			}
			set
			{
				if (value == switchFlicks)
				{
					return;
				}
				switchFlicks = value;
				VerifyQuitConditions();
				Button.NotifyCounterChanged();
			}
		}

		/// <summary>
		/// Started with Animator, but persistent states upon transitions and restarting animator were too much for the alotted time.
		/// Went for the simpler solution to finish this up
		/// </summary>
		/// <remarks>Should not be necessary to use FixedUpdate here, rotates based on time between frames</remarks>
		private void Update()
		{
			if (Rotating)
			{
				int dir = Clockwise ? -1 : 1; //Right-hand rotation is negative on the z-axis
				transform.Rotate(Vector3.forward, TimeFullRotation * Time.deltaTime * dir);
			}
		}

		private void Awake()
		{
			Instance = this;
		}

		public static void VerifyQuitConditions()
		{
			if (ButtonClicks + SwitchFlicks >= TotalClicksAlotted)
			{
				Instance.PromptQuit();
			}
		}

		private void PromptQuit()
		{
			gameOverPrompt.SetActive(true);
		}

		public void Continue()
		{
			buttonClicks = 0;
			switchFlicks = 0;
			gameOverPrompt.SetActive(false);
			Button.NotifyCounterChanged();
			Switch.NotifyCounterChanged();

			//Stop rotation, reset to 0
			Rotating = false;
			transform.rotation = Quaternion.identity;
			//dialAnimator.Play("Default", -1, 0);
			//dialAnimator.enabled = false;
		}

		public void Quit()
		{
#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
#else
			Application.Quit();
#endif
		}
	}
}
