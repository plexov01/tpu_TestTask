namespace TPU_TestTask.Features.CircleMenu
{
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    /// Сектор кругового меню
    /// </summary>
    [Serializable]
    public class CircleMenuSectorData
    {
        public Image ImageButton = default;
        public Color NormalColor = Color.white;
        public Color HighlightedColor = Color.gray;
        public Color PressedColor = Color.black;
    }

}
