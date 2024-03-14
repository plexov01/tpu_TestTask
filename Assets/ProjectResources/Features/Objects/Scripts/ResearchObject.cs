namespace  TPU_TestTask.Features.Object
{
    using Camera;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    /// <summary>
    /// Объект, который совмещается с инструментом
    /// </summary>
    public class ResearchObject : MonoBehaviour
    {
        public event Action<GameObject> onObjectClicked = delegate {  };
        [SerializeField] private List<ObjectState> _objectStates = new List<ObjectState>();
        

        private void OnMouseDown()
        {
            
            onObjectClicked(gameObject);
        }
    }

}
