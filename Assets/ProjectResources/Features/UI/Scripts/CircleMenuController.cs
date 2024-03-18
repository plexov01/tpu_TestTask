namespace TPU_TestTask.Features.CircleMenu
{
    using System.Collections.Generic;
    using UnityEngine;
    /// <summary>
    /// Контроллер кругового меню
    /// </summary>
    public class CircleMenuController : MonoBehaviour
    {
        private List<CircleMenuSectorData> _circleMenuSectorsData = new List<CircleMenuSectorData>();
        private int _countCircleSectors = default;

        private readonly Vector2 _refVector = new Vector2(0, 1);
        private Vector2 _currentCenterCircle = default;

        private bool _activeCircleMenu = default;
        private int _lastChosenSectorIndex = -1;
        
        private CircleMenuView _circleMenuView = default;

        private void Start()
        {
            _circleMenuView = FindObjectOfType<CircleMenuView>();
            _activeCircleMenu = false;
            _circleMenuView.HideUI();
            
        }
        
        private int GetCurrentMenuSectorIndex()
        {
            Vector2 mousePosition = Input.mousePosition;
            
            float angle = Vector2.SignedAngle(mousePosition - _currentCenterCircle, _refVector);

            if (angle < 0)
            {
                angle += 360;
            }
            
            int currentSectorIndex = (int)(angle/(360.01f / _countCircleSectors));

            // Debug.Log(angle+" "+currentItemIndex+" "+mousePosition);
            
            return currentSectorIndex;
        }

        private void StartChooseItem(Vector2 centerCircleVector2, int numberItems)
        {
            _currentCenterCircle = centerCircleVector2;
            _countCircleSectors = numberItems;
            
            for (int i = 0; i < numberItems; i++)
            {
                _circleMenuSectorsData.Add(new CircleMenuSectorData());
            }
            
            _activeCircleMenu = true;
            _circleMenuView.SetupCircleMenu(_currentCenterCircle, _circleMenuSectorsData);
            _circleMenuView.ShowUI();
        }

        private void StopChooseItem()
        {
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
                _circleMenuView.SetChosenSectorByIndex(_lastChosenSectorIndex);
            }
        }
    }

}
