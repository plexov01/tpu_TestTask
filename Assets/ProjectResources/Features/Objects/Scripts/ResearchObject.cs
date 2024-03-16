namespace  TPU_TestTask.Features.Object
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    /// <summary>
    /// Объект, который совмещается с инструментом
    /// </summary>
    public class ResearchObject : MonoBehaviour
    {
        /// <summary>
        /// На объект кликнули
        /// </summary>
        public event Action<GameObject> OnObjectClicked = delegate {  };
        [SerializeField] private List<ObjectState> _objectStates = new List<ObjectState>();

        private Vector3 _startPosition = default;
        private Quaternion _startRotation = default;

        private void Awake()
        {
            _startPosition = transform.position;
            _startRotation = transform.rotation;
        }
        
        private void OnMouseDown()
        {
            OnObjectClicked(gameObject);
        }
        /// <summary>
        /// Вернуть объекты в стартовое положение
        /// </summary>
        public void ReturnStartState()
        {
            transform.position = _startPosition;
            transform.rotation = _startRotation;
        }
    }

}
