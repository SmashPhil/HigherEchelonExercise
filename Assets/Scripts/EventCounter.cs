using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

namespace HigherEchelon
{
	/// <summary>
	/// Base class for behaviours counting towards the GameOver count
	/// </summary>
	/// <remarks>Automatically fetched from <see cref="DialHandler"/></remarks>
	public abstract class EventCounter : MonoBehaviour
	{
		[SerializeField]
		private TextMeshProUGUI counterLabel;
		private int count;

		public int Count
		{
			get
			{
				return count;
			}
			set
			{
				if (count == value)
				{
					return;
				}
				count = value;
				DialHandler.Instance.VerifyQuitConditions();
				CountChanged();
			}
		}

		public virtual void CountChanged()
		{
			counterLabel.text = Count.ToString();
		}
	}
}
