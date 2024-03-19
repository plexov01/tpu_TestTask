namespace  TPU_TestTask.Features.Object
{
    using ObjectMovingSytem;
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

        private Vector3 _startPosition = default;
        private Quaternion _startRotation = default;

        private ObjectMovingSystem _objectMovingSystem = default;

        private void Awake()
        {
            _startPosition = transform.position;
            _startRotation = transform.rotation;
        }

        private void Start()
        {
            _objectMovingSystem = FindObjectOfType<ObjectMovingSystem>();
            _objectMovingSystem.OnReturningStartStateObjects += ReturnStartState;
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

        private void OnDisable()
        {
            _objectMovingSystem.OnReturningStartStateObjects -= ReturnStartState;
        }
    }

}
