namespace  TPU_TestTask.Features.Camera
{
    using UnityEngine;
    /// <summary>
    /// Главная камера
    /// </summary>
    public class MainCamera : MonoBehaviour
    {
        /// <summary>
        /// Экзмепляр главной камеры
        /// </summary>
        public Camera Camera = default;

        private void Awake() => Camera = GetComponent<Camera>();
    }

}
