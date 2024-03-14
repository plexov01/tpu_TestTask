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
        public event Action<GameObject> onObjectClicked = delegate {  };
        [SerializeField] private List<ObjectState> _objectStates = new List<ObjectState>();
        

        private void OnMouseDown()
        {
            
            onObjectClicked(gameObject);
        }
    }

}
