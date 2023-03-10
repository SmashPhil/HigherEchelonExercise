using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace HigherEchelon
{
    public class Button : EventCounter
    {
        public void ToggleAnimation()
        {
            DialHandler.Active = !DialHandler.Active;
            Count++;
        }
    }
}
