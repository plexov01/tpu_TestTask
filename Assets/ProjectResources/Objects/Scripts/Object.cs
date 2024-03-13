namespace  TPU_TestTask.Features.Object
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    /// <summary>
    /// Объект, который совмещается с инструментом
    /// </summary>
    public class Object : MonoBehaviour
    {
        [SerializeField] private List<ObjectState> _objectStates = new List<ObjectState>();
    }

}
