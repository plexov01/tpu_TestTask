namespace TPU_TestTask.Features.SelectionPoint
{
    using System;
    using UnityEngine;
    /// <summary>
    /// Точки выбора позиций объетов
    /// </summary>
    public class SelectionPoint : MonoBehaviour
    {
        /// <summary>
        /// Событие при наведении мыши на точку выбора
        /// </summary>
        public event Action<GameObject> OnMousePointed = delegate {  };
        
        /// <summary>
        /// Событие при сведении мыши с точки выбора
        /// </summary>
        public event Action<GameObject> OnMouseNotPointed = delegate {  };
        
        private void OnMouseEnter()
        {
            OnMousePointed(gameObject);
            Debug.Log("in");
        }

        private void OnMouseExit()
        {
            OnMouseNotPointed(gameObject);
            Debug.Log("out");
        }
    }

}
