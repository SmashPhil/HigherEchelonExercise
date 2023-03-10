using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HigherEchelon
{
	public class DialHandler : MonoBehaviour
	{
		private const int TotalClicksAlotted = 10;
		private const float TimeFullRotation = (1 / 3f) * 360; //Rotate 360deg once per 3 seconds

		[SerializeField]
		private GameObject gameOverPrompt;
		[SerializeField]
		private Animator dialAnimator;

		private static DialHandler instance;

		private List<EventCounter> counters = new List<EventCounter>();

		public static bool Clockwise { get; set; } = true;

		public static bool Active { get; set; } = false;

		//Singleton isn't really necessary in the scope of this project.
		//Note: If scope of project were to scale up, the game over mechanic and dial behaviour script would want to be split up, and singleton removed
		public static DialHandler Instance
		{
			get
			{
				return instance;
			}
			set
			{
				if (instance)
				{
					Destroy(instance.gameObject); //Destroy previous instance to avoid overlapping singleton script
				}
				instance = value;
			}
		}

		private void Update()
		{
			if (Active)
			{
				int dir = Clockwise ? -1 : 1; //Right-hand rotation is negative on the z-axis
				transform.Rotate(Vector3.forward, TimeFullRotation * Time.deltaTime * dir);
			}
		}

		private void Awake()
		{
			Instance = this;

			//Pull all EventCounters in scene.
			//Note: This is a slow operation, however the project size is small so it's negligeable.
			counters = FindObjectsOfType<EventCounter>().ToList(); 
		}

		public void VerifyQuitConditions()
		{
			if (counters.Sum(eventCounter => eventCounter.Count) >= TotalClicksAlotted)
			{
				PromptQuit();
			}
		}

		private void PromptQuit()
		{
			gameOverPrompt.SetActive(true);
		}

		public void Continue()
		{
			foreach (EventCounter counter in counters)
			{
				counter.Count = 0; //Count setter will trigger callback for updating text
			}
			gameOverPrompt.SetActive(false);

			//Reset dial to original state
			Active = false;
			transform.rotation = Quaternion.identity;
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
