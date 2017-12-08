using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputKeyboard : IInput
{
    Ray ray;
    RaycastHit hit;
    private int fingerID = -1;

    public Vector3 getSelection()
    {
#if !UNITY_EDITOR
     fingerID = 0; 
#endif
        if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject(fingerID))
        {
            if(MenuManager.instance.gameState == GameState.Game || MenuManager.instance.gameState == GameState.Tutorial)
            if (Input.GetButton("Fire1"))
            {
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
               if (Physics.Raycast(ray, out hit, 1000, 1 << LayerMask.NameToLayer("Water")))
                {
                    switch (hit.collider.tag)
                    {
                        case "Water": return hit.point;
                        case "Limit": return hit.point;
                       /* case "Rudder": PlayerController.instance.sailorEvent.Invoke(1); break;
                        case "Nest": PlayerController.instance.sailorEvent.Invoke(2); break;
                        case "LeftCannons": PlayerController.instance.sailorEvent.Invoke(3); break;
                        case "RightCannons": PlayerController.instance.sailorEvent.Invoke(4); break;
                        case "FrontCannon": PlayerController.instance.sailorEvent.Invoke(5); break;*/
                    }
                }
            }
        }
        return Vector3.zero;
    }

}
