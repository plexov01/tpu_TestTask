namespace TPU_TestTask.Features.TransparencyController
{
    using Object;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    /// <summary>
    /// Контроллер прозрачности объектов
    /// </summary>
    public class TransparencyController : MonoBehaviour
    {
        private List<ResearchObject> _researchObjects = new List<ResearchObject>();

        private void Start()
        {
            _researchObjects = FindObjectsOfType<ResearchObject>().ToList();
            
            foreach (var researchObject in _researchObjects)
            {
                researchObject.onObjectClicked += MakeTransparent;
            }
            
            
        }
        private void OnDisable()
        {
            foreach (var researchObject in _researchObjects)
            {
                researchObject.onObjectClicked -= MakeTransparent;
            }
        }

        private void MakeTransparent(GameObject transparentObject)
        {
            Material objectMaterial = transparentObject.GetComponent<Renderer>().material;
            Color newColor = objectMaterial.color;
            newColor.a = 0.5f;
            objectMaterial.color = newColor;
            
        }

        private void MakeNotTransparent(GameObject transparentObject)
        {
            Material objectMaterial = transparentObject.GetComponent<Renderer>().material;
            Color newColor = objectMaterial.color;
            newColor.a = 1;
            objectMaterial.color = newColor;
        }
    }
}

