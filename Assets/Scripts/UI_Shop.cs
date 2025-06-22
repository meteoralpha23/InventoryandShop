using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Shop : MonoBehaviour
{
    private Transform _container;
    private Transform _shopItemTemplate;

    private void Awake()
    {
        _container = transform.Find("container");
        _shopItemTemplate = _container.Find("shopItemTemplate");
        _shopItemTemplate.gameObject.SetActive(false);  
    }

}
