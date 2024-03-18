namespace TPU_TestTask.Features.CircleMenu
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using UnityEngine.Serialization;
    using UnityEngine.UI;

    /// <summary>
    /// Контроллер UI круговго меню
    /// </summary>
    public class CircleMenuView : MonoBehaviour
    {
        public GameObject CircleMenuSector = default;
        
        private List<GameObject> _circleMenuSectors = new List<GameObject>();
        private List<CircleMenuSectorData> _circleMenuSectorsData = new List<CircleMenuSectorData>();

        private GameObject _circleMenu = default;
        
        private void Start()
        {
            _circleMenu = gameObject;
            HideUI();  
        }
        /// <summary>
        /// Настроить круговое меню
        /// </summary>
        /// <param name="positionMenu"></param>
        /// <param name="circleMenuSectorsData"></param>
        public void SetupCircleMenu(Vector3 positionMenu, List<CircleMenuSectorData> circleMenuSectorsData)
        {
            _circleMenu.transform.position = positionMenu;
            
            _circleMenuSectorsData = circleMenuSectorsData;
            
            for (int i = 0; i < circleMenuSectorsData.Count; i++)
            {
                if (i < _circleMenuSectors.Count)
                {
                    _circleMenuSectors[i].GetComponent<Image>().fillAmount = (float)1/circleMenuSectorsData.Count;
                }
                else
                {
                    GameObject item = Instantiate(CircleMenuSector, transform.position, Quaternion.Euler(0f, 0f, -i * (360f / circleMenuSectorsData.Count)), transform);
                    item.GetComponent<Image>().fillAmount = (float)1/circleMenuSectorsData.Count;
                                    
                    _circleMenuSectors.Add(item);
                }
                
            }

        }
        /// <summary>
        /// Выделить выбранный сектор
        /// </summary>
        /// <param name="index"></param>
        public void SetChosenSectorByIndex(int index)
        {
            for (int i = 0; i < _circleMenuSectors.Count; i++)
            {
                _circleMenuSectors[i].GetComponent<Image>().color = _circleMenuSectorsData[i].NormalColor;
            }

            if (index!=-1)
            {
                _circleMenuSectors[index].GetComponent<Image>().color = _circleMenuSectorsData[index].HighlightedColor;
            }
        }
        
        /// <summary>
        /// Показать UI
        /// </summary>
        public void ShowUI()
        {
            foreach (var circleMenuSector in _circleMenuSectors)
            {
                circleMenuSector.SetActive(true);
            }
        }
        
        /// <summary>
        /// Спрятать UI
        /// </summary>
        public void HideUI()
        {
            foreach (var circleMenuSector in _circleMenuSectors)
            {
                circleMenuSector.SetActive(false);
            }
        }
    }

}
