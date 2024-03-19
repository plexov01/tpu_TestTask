namespace TPU_TestTask.Features.ObjectMovingSytem
{
    using Camera;
    using CircleMenu;
    using InputController;
    using Object;
    using SelectionPoint;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    /// <summary>
    /// Система, перемещающая объекты
    /// </summary>
    public class ObjectMovingSystem : MonoBehaviour
    {
        /// <summary>
        /// Объекту возвращается исходная прозрачность
        /// </summary>
        public event Action<GameObject> OnReturningTransparencyObject = delegate {  };
        /// <summary>
        /// Объекты возвращаются в стартовое состояние
        /// </summary>
        public event Action OnReturningStartStateObjects = delegate {  };
        /// <summary>
        /// Объект не привязан к экрану
        /// </summary>
        public event Action<GameObject> OnMovingObjectOnSelectionPoint = delegate {  }; 
        /// <summary>
        /// Объект привязан к экрану
        /// </summary>
        public event Action<GameObject> OnMovingObjectOnScreen = delegate {  };

        private MainCamera _mainCamera = default;
        private MouseInputController _mouseInputController = default;
        private CircleMenuController _circleMenuController = default;
        
        private List<ResearchObject> _researchObjects = new List<ResearchObject>();
        private List<SelectionPoint> _selectionPoints = new List<SelectionPoint>();
        

        private Vector3 _pointScreen = default;
        private GameObject _movingObject = default;
        private Vector3 _movingPosition = default;
        
        private bool _onScreen = default;
        
        private void Start()
        {
            _mainCamera = FindObjectOfType<MainCamera>();
            _mouseInputController = FindObjectOfType<MouseInputController>();
            _circleMenuController = FindObjectOfType<CircleMenuController>();
            
            _researchObjects = FindObjectsOfType<ResearchObject>().ToList();
            _selectionPoints = FindObjectsOfType<SelectionPoint>().ToList();

            _mouseInputController.OnMouseLeftButtonUp += CheckAndReturnObjects;
            _circleMenuController.OnNotSelectingItem += ReturnObjects;
            
            foreach (var researchObject in _researchObjects)
            {
                researchObject.OnObjectClicked += MoveObjectToScreen;
            }

            foreach (var selectionPoint in _selectionPoints)
            {
                selectionPoint.OnMousePointed += MoveObjectToPoint;
            }
        }
        /// <summary>
        /// Начать передвигать объект по экрану
        /// </summary>
        /// <param name="movingObject"></param>
        private void MoveObjectToScreen(GameObject movingObject)
        {
            _onScreen = true;
            if (_movingObject!=null)
            {
                ReturnObjects();
            }
            _movingObject = movingObject;

            _movingObject.GetComponent<Collider>().enabled = false;
            
            _pointScreen = _mainCamera.Camera.WorldToScreenPoint(_movingObject.transform.position);
            _pointScreen.z = 2f;

            _movingObject.transform.rotation = _mainCamera.transform.rotation;
            OnMovingObjectOnScreen(_movingObject);
            
        }
        /// <summary>
        /// Передвинуть объект к экрану
        /// </summary>
        /// <param name="triggeredObject"></param>
        private void MovingObjectMoveToScreen(GameObject triggeredObject)
        {
            if (_movingObject==null)
            {
                return;
            }
            
            _onScreen = true;
            
            _movingObject.GetComponent<Collider>().enabled = false;
            
            _pointScreen = _mainCamera.Camera.WorldToScreenPoint(_movingObject.transform.position);
            _pointScreen.z = 2f;

            _movingObject.transform.rotation = _mainCamera.transform.rotation;
            OnMovingObjectOnScreen(_movingObject);
        }
        
        /// <summary>
        /// Передвинуть объект в точку выбора состояния
        /// </summary>
        /// <param name="pointToMove"></param>
        private void MoveObjectToPoint(GameObject pointToMove)
        {
            if (_movingObject==null)
            {
                return;
            }
            
            _onScreen = false;
            
            _movingObject.GetComponent<Collider>().enabled = true;

            _movingPosition = pointToMove.transform.position;
            
            OnMovingObjectOnSelectionPoint(_movingObject);
        }

        private void FixedUpdate()
        {
            if (_onScreen)
            {
                //Двигаем объект за курсором
                Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _pointScreen.z);
                _movingPosition = _mainCamera.Camera.ScreenToWorldPoint(curScreenPoint);
            }
            
            if (_movingObject != null)
            {
                _movingObject.transform.position = Vector3.Lerp(_movingObject.transform.position, _movingPosition,
                    25f * Time.fixedDeltaTime);
            }
            
        }

        private void CheckAndReturnObjects()
        {
            if (_onScreen)
            {
                _onScreen = false;
                _movingObject.GetComponent<Collider>().enabled = true;
                
                OnReturningTransparencyObject(_movingObject);
                OnReturningStartStateObjects();
                
                _movingObject = null;
            }
            
        }

        private void ReturnObjects()
        {
            OnReturningStartStateObjects();
            _movingObject = null;
        }

        private void OnDisable()
        {
            _mouseInputController.OnMouseLeftButtonUp -= CheckAndReturnObjects;
            _circleMenuController.OnNotSelectingItem -= ReturnObjects;
            
            foreach (var researchObject in _researchObjects)
            {
                researchObject.OnObjectClicked -= MoveObjectToScreen;
            }

            foreach (var selectionPoint in _selectionPoints)
            {
                selectionPoint.OnMousePointed -= MoveObjectToPoint;
            }
        }
        
    }

}
