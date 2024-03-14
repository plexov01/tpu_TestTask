namespace TPU_TestTask.Features.ObjectMovingSytem
{
    using Camera;
    using Object;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    /// <summary>
    /// Система, перемещающая объекты
    /// </summary>
    public class ObjectMovingSystem : MonoBehaviour
    {

        private List<ResearchObject> _researchObjects = new List<ResearchObject>();
        private MainCamera _mainCamera = default;

        private Vector3 _pointScreen = default;
        private GameObject _movingObject = default;
        private void Start()
        {
            _mainCamera = FindObjectOfType<MainCamera>();
            
            _researchObjects = FindObjectsOfType<ResearchObject>().ToList();
            
            foreach (var researchObject in _researchObjects)
            {
                researchObject.onObjectClicked += MoveObject;
            }
        }
        /// <summary>
        /// Передвинуть объект
        /// </summary>
        /// <param name="movingObject"></param>
        public void MoveObject(GameObject movingObject)
        {
            _movingObject = movingObject;
            

            _pointScreen = _mainCamera.Camera.WorldToScreenPoint(_movingObject.transform.position);
            _pointScreen.z = 2f;

            _movingObject.transform.rotation = _mainCamera.transform.rotation;

        }

        private void FixedUpdate()
        {
            if (_movingObject != null)
            {
                //Двигаем объект за курсором
                Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _pointScreen.z);
                Vector3 curPosition = _mainCamera.Camera.ScreenToWorldPoint(curScreenPoint);
                _movingObject.transform.position = Vector3.Lerp(_movingObject.transform.position,curPosition , 5f * Time.deltaTime);
                
                // Debug.Log(curPosition);
            }
        }

        private void OnDisable()
        {
            foreach (var researchObject in _researchObjects)
            {
                researchObject.onObjectClicked -= MoveObject;
            }
        }
        
    }

}
