namespace TPU_TestTask.Features.ObjectPlacementSystem
{
    using Object;
    using ObjectMovingSytem;
    using UnityEngine;
    
    /// <summary>
    /// Система позиционирования объектов
    /// </summary>
    public class ObjectPlacementSystem : MonoBehaviour
    {
        private ObjectMovingSystem _objectMovingSystem = default;

        private ResearchObject _currentResearchObject = default;

        private int _indexObjectState = default;
        private void Start()
        {
            _objectMovingSystem = FindObjectOfType<ObjectMovingSystem>();

            _objectMovingSystem.OnMovingObjectOnSelectionPoint += PlaceObjectDefaultState;
        }

        private void PlaceObjectDefaultState(GameObject objectToPlace)
        {
            _currentResearchObject = objectToPlace.GetComponent<ResearchObject>();
            
            _indexObjectState = 0;
            
            objectToPlace.transform.position = _currentResearchObject._objectStates[_indexObjectState].Position;
            objectToPlace.transform.rotation = _currentResearchObject._objectStates[_indexObjectState].Rotation;
        }

        private void PlaceObjectNextState()
        {
            _indexObjectState++;
            
            if (_indexObjectState > _currentResearchObject._objectStates.Count - 1)
            {
                _indexObjectState = 0;
            }
            
            _currentResearchObject.transform.position = _currentResearchObject._objectStates[_indexObjectState].Position;
            _currentResearchObject.transform.rotation = _currentResearchObject._objectStates[_indexObjectState].Rotation;
            
        }
        
        private void PlaceObjectPreviousState()
        {
            _indexObjectState--;
            
            if (_indexObjectState < 0)
            {
                _indexObjectState = _currentResearchObject._objectStates.Count - 1;
            }
            
            _currentResearchObject.transform.position = _currentResearchObject._objectStates[_indexObjectState].Position;
            _currentResearchObject.transform.rotation = _currentResearchObject._objectStates[_indexObjectState].Rotation;
            
        }

        private void PlaceObjectStateByIndex(int indexState)
        {
            _currentResearchObject.transform.position = _currentResearchObject._objectStates[_indexObjectState].Position;
            _currentResearchObject.transform.rotation = _currentResearchObject._objectStates[_indexObjectState].Rotation;
        }
        
        
        private void OnDisable()
        {
            _objectMovingSystem.OnMovingObjectOnSelectionPoint -= PlaceObjectDefaultState;
        }
    }

}
