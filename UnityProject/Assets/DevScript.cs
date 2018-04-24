using System;
using UnityEngine;


public class DevScript : MonoBehaviour
{
    public event Action<bool> SpaceClicked; 
    
    private void Start()
    {
        
    }

    private void FixedUpdate()
    {
        
    }

    public virtual void OnSpaceClicked(bool e)
    {
        if (SpaceClicked != null) SpaceClicked.Invoke(e);
    }
}