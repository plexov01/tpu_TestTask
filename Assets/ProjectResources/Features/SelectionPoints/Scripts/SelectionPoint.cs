namespace TPU_TestTask.Features.SelectionPoint
{
    using Object;
    using System;
    using System.Collections.Generic;
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
        /// <summary>
        /// Список позиций объекта
        /// </summary>
        public List<ObjectState> _objectStates = new List<ObjectState>();
        
        private void OnMouseEnter()
        {
            OnMousePointed(gameObject);
        }
        
    }

}
