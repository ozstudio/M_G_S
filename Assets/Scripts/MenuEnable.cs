using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuEnable : MonoBehaviour
{
    [SerializeField]
    private GameObject _menu;
    // Start is called before the first frame update
    void Start()
    {
        
    }

   public void OpenCloseMenu()
    {
        if(_menu.activeInHierarchy)
        {
          _menu.SetActive(false);
        }
        else 
        _menu.SetActive(true);
        

    }
}
