namespace TPU_TestTask.Features.CircleMenu
{
    using Camera;
    using InputController;
    using Object;
    using ObjectMovingSytem;
    using SelectionPoint;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    /// <summary>
    /// Контроллер кругового меню
    /// </summary>
    public class CircleMenuController : MonoBehaviour
    {
        /// <summary>
        /// Состояние объекта не выбрано
        /// </summary>
        public event Action OnNotSelectingItem = delegate {  };
        /// <summary>
        /// Индекс выбранного элемента изменился
        /// </summary>
        public event Action<int> OnChangingIndex = delegate {  };

        private List<CircleMenuSectorData> _circleMenuSectorsData = new List<CircleMenuSectorData>();
        private int _countCircleSectors = default;

        private readonly Vector2 _refVector = new Vector2(0, 1);
        private Vector2 _currentCenterCircle = default;
        private float _radiusMenu = default;

        private bool _activeCircleMenu = default;
        private int _lastChosenSectorIndex = -1;

        private GameObject _currentResearchObject = default;
        
        private List<ResearchObject> _researchObjects = new List<ResearchObject>();
        private List<SelectionPoint> _selectionPoints = new List<SelectionPoint>();
        
        private CircleMenuView _circleMenuView = default;
        private ObjectMovingSystem _objectMovingSystem = default;
        private MainCamera _mainCamera = default;
        private MouseInputController _mouseInputController = default;
        
        private void Start()
        {
            _circleMenuView = FindObjectOfType<CircleMenuView>();
            _objectMovingSystem = FindObjectOfType<ObjectMovingSystem>();
            _mainCamera = FindObjectOfType<MainCamera>();
            _mouseInputController = FindObjectOfType<MouseInputController>();
            
            _researchObjects = FindObjectsOfType<ResearchObject>().ToList();
            _selectionPoints = FindObjectsOfType<SelectionPoint>().ToList();

            _objectMovingSystem = FindObjectOfType<ObjectMovingSystem>();
            
            foreach (var researchObject in _researchObjects)
            {
                researchObject.OnObjectClicked += SetupCurrentResearchObject;
            }
            
            foreach (var selectionPoint in _selectionPoints)
            {
                selectionPoint.OnMousePointed += StartChooseItemAtSelectionPoint;
            }
            
            _mouseInputController.OnMouseLeftButtonUp += StopChooseItem;
            
            _activeCircleMenu = false;
            _circleMenuView.HideUI();
            
            _radiusMenu = _circleMenuView.GetSectorRadius();

        }

        private void OnDisable()
        {
            foreach (var researchObject in _researchObjects)
            {
                researchObject.OnObjectClicked -= SetupCurrentResearchObject;
            }
            
            foreach (var selectionPoint in _selectionPoints)
            {
                selectionPoint.OnMousePointed -= StartChooseItemAtSelectionPoint;
            }
            
            _mouseInputController.OnMouseLeftButtonUp -= StopChooseItem;
        }

        private void StartChooseItemAtSelectionPoint(GameObject selectionPoint)
        {
            if (_currentResearchObject==null)
            {
                return;
            }
            
            Vector3 currentCircleMenuPosition =
                _mainCamera.Camera.WorldToScreenPoint(selectionPoint.transform.position);
            List<ObjectState> statesAtSelectionPoint = selectionPoint.GetComponent<SelectionPoint>()._objectStates;
            
            StartChooseItem(currentCircleMenuPosition, statesAtSelectionPoint.Count);
        }

        private void SetupCurrentResearchObject(GameObject researchObject)
        {
            _currentResearchObject = researchObject;
        }

        private int GetCurrentMenuSectorIndex()
        {
            Vector2 mousePosition = Input.mousePosition;
            
            // Если вышел за пределы меню
            if (Vector2.Distance(mousePosition,_currentCenterCircle) > _radiusMenu)
            {
                return -1;
            }
            
            float angle = Vector2.SignedAngle(mousePosition - _currentCenterCircle, _refVector);

            if (angle < 0)
            {
                angle += 360;
            }
            
            int currentSectorIndex = (int)(angle/(360.01f / _countCircleSectors));

            return currentSectorIndex;
        }

        private void StartChooseItem(Vector3 centerCircleVector2, int numberItems)
        {
            _currentCenterCircle = centerCircleVector2;
            _countCircleSectors = numberItems;
            
            _circleMenuSectorsData.Clear();
            
            for (int i = 0; i < numberItems; i++)
            {
                _circleMenuSectorsData.Add(new CircleMenuSectorData());
            }


            _circleMenuView.HideUI();
            _activeCircleMenu = true;
            _circleMenuView.SetupCircleMenu(_currentCenterCircle, _circleMenuSectorsData);
            _circleMenuView.ShowUI();
        }

        private void StopChooseItem()
        {
            if (_currentResearchObject == null)
            {
                return;
            }

            if (_lastChosenSectorIndex==-1)
            {
                OnNotSelectingItem();
            }
            
            _currentResearchObject = null;
            
            _activeCircleMenu = false;
            _circleMenuView.HideUI();
            
        }

        private void FixedUpdate()
        {
            if (!_activeCircleMenu)
            {
                return;
            }
            int currentSectorIndex = GetCurrentMenuSectorIndex();

            if (currentSectorIndex != _lastChosenSectorIndex)
            {
                _lastChosenSectorIndex = currentSectorIndex;
                
                if (_lastChosenSectorIndex != -1)
                {
                    OnChangingIndex(_lastChosenSectorIndex);
                }
                
                _circleMenuView.SetChosenSectorByIndex(_lastChosenSectorIndex);

            }
        }
    }

}
