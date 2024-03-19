namespace TPU_TestTask.Features.InputController
{
    using System;
    using UnityEngine;
    /// <summary>
    /// Контроллер нажатий кнопок мыши
    /// </summary>
    public class MouseInputController : MonoBehaviour
    {
        /// <summary>
        /// Левая кнопка мыши отжата
        /// </summary>
        public event Action OnMouseLeftButtonUp = delegate {  };
        
        void Update()
        {
            if (Input.GetMouseButtonUp(0))
            {
                OnMouseLeftButtonUp();
            }
        }
    }

}
