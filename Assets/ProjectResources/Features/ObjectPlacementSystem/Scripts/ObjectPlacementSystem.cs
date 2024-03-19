namespace TPU_TestTask.Features.ObjectPlacementSystem
{
    using CircleMenu;
    using Object;
    using SelectionPoint;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    
    /// <summary>
    /// Система позиционирования объектов
    /// </summary>
    public class ObjectPlacementSystem : MonoBehaviour
    {
        private ResearchObject _currentResearchObject = default;
        private SelectionPoint _currentSelectionPoint = default;

        private int _indexObjectState = default;
        
        private CircleMenuController _circleMenuController = default;
        
        private List<ResearchObject> _researchObjects = new List<ResearchObject>();
        private List<SelectionPoint> _selectionPoints = new List<SelectionPoint>();

        private void Start()
        {
            _circleMenuController = FindObjectOfType<CircleMenuController>();
            _researchObjects = FindObjectsOfType<ResearchObject>().ToList();
            _selectionPoints = FindObjectsOfType<SelectionPoint>().ToList();

            _circleMenuController.OnChangingIndex += PlaceObjectStateByIndex;
            
            foreach (var researchObject in _researchObjects)
            {
                researchObject.OnObjectClicked += GetCurrentResearchObject;
            }
            
            foreach (var selectionPoint in _selectionPoints)
            {
                selectionPoint.OnMousePointed += GetCurrentSelectionPoint;
            }
        }

        private void GetCurrentResearchObject(GameObject objectToPlace)
        {
            _currentResearchObject = objectToPlace.GetComponent<ResearchObject>();
        }

        private void GetCurrentSelectionPoint(GameObject selectionPoint)
        {
            _currentSelectionPoint = selectionPoint.GetComponent<SelectionPoint>();
        }

        private void PlaceObjectNextState()
        {
            _indexObjectState++;
            
            if (_indexObjectState > _currentSelectionPoint._objectStates.Count - 1)
            {
                _indexObjectState = 0;
            }
            
            _currentResearchObject.transform.rotation = _currentSelectionPoint._objectStates[_indexObjectState].Rotation;
            
        }
        
        private void PlaceObjectPreviousState()
        {
            _indexObjectState--;
            
            if (_indexObjectState < 0)
            {
                _indexObjectState = _currentSelectionPoint._objectStates.Count - 1;
            }
            
            _currentResearchObject.transform.rotation = _currentSelectionPoint._objectStates[_indexObjectState].Rotation;
            
        }

        private void PlaceObjectStateByIndex(int indexState)
        { 
            _indexObjectState = indexState;
            _currentResearchObject.transform.rotation = _currentSelectionPoint._objectStates[_indexObjectState].Rotation;
        }
        
        
        private void OnDisable()
        {
            _circleMenuController.OnChangingIndex -= PlaceObjectStateByIndex;
            
            foreach (var researchObject in _researchObjects)
            {
                researchObject.OnObjectClicked -= GetCurrentResearchObject;
            }
            
            foreach (var selectionPoint in _selectionPoints)
            {
                selectionPoint.OnMousePointed -= GetCurrentSelectionPoint;
            }
            
        }
    }

}
