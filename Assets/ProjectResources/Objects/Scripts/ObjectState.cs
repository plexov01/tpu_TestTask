namespace TPU_TestTask.Features.Object
{
    using System;
    using UnityEngine;

    /// <summary>
    /// Состояние объекта
    /// </summary>
    [Serializable]
    public class ObjectState
    {
        public Vector3 Position = default;
        public Quaternion Rotation = default;
    }

}
