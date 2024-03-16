namespace TPU_TestTask.Features.TransparencyController
{
    using Object;
    using ObjectMovingSytem;
    using SelectionPoint;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    /// <summary>
    /// Контроллер прозрачности объектов
    /// </summary>
    public class TransparencyController : MonoBehaviour
    {
        private List<ResearchObject> _researchObjects = new List<ResearchObject>();
        private List<SelectionPoint> _selectionPoints = new List<SelectionPoint>();

        private ObjectMovingSystem _objectMovingSystem = default;

        private void Start()
        {
            _researchObjects = FindObjectsOfType<ResearchObject>().ToList();
            _selectionPoints = FindObjectsOfType<SelectionPoint>().ToList();

            _objectMovingSystem = FindObjectOfType<ObjectMovingSystem>();
            
            foreach (var researchObject in _researchObjects)
            {
                researchObject.OnObjectClicked += MakeHalfTransparent;
                researchObject.OnObjectClicked += MakeAllSelectionPointsNotTransparent;
            }
            
            foreach (var selectionPoint in _selectionPoints)
            {
                MakeFullTransparent(selectionPoint.gameObject);
                selectionPoint.OnMousePointed += MakeAllSelectionPointsFullTransparent;
                selectionPoint.OnMouseNotPointed += MakeAllSelectionPointsNotTransparent;
            }

            _objectMovingSystem.OnMovingObjectOnSelectionPoint += MakeTransparent;
            _objectMovingSystem.OnMovingObjectOnScreen += MakeHalfTransparent;


        }
        private void OnDisable()
        {
            foreach (var researchObject in _researchObjects)
            {
                researchObject.OnObjectClicked -= MakeHalfTransparent;
                researchObject.OnObjectClicked -= MakeAllSelectionPointsNotTransparent;
            }
            
            foreach (var selectionPoint in _selectionPoints)
            {
                selectionPoint.OnMousePointed -= MakeAllSelectionPointsFullTransparent;
                selectionPoint.OnMouseNotPointed -= MakeAllSelectionPointsNotTransparent;
            }
            
            _objectMovingSystem.OnMovingObjectOnSelectionPoint -= MakeTransparent;
            _objectMovingSystem.OnMovingObjectOnScreen -= MakeHalfTransparent;
        }

        private void MakeHalfTransparent(GameObject transparentObject)
        {
            Material objectMaterial = transparentObject.GetComponent<Renderer>().material;
            Color newColor = objectMaterial.color;
            newColor.a = 0.5f;
            objectMaterial.color = newColor;
            
        }

        private void MakeTransparent(GameObject transparentObject)
        {
            Material objectMaterial = transparentObject.GetComponent<Renderer>().material;
            Color newColor = objectMaterial.color;
            newColor.a = 1;
            objectMaterial.color = newColor;
        }

        private void MakeFullTransparent(GameObject transparentObject)
        {
            Material objectMaterial = transparentObject.GetComponent<Renderer>().material;
            Color newColor = objectMaterial.color;
            newColor.a = 0;
            objectMaterial.color = newColor;
        }

        private void MakeAllSelectionPointsFullTransparent(GameObject transparentObject)
        {
            foreach (var selectionPoint in _selectionPoints)
            {
                MakeFullTransparent(selectionPoint.gameObject);
            }
        }

        private void MakeAllSelectionPointsNotTransparent(GameObject transparentObject)
        {
            foreach (var selectionPoint in _selectionPoints)
            {
                MakeTransparent(selectionPoint.gameObject);
            }
        }
    }
}

