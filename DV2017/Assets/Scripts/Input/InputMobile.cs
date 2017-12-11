using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputMobile : IInput
{
    Ray ray;
    RaycastHit hit;

    public Vector3 getSelection()
    {
        if (MenuManager.instance.gameState == GameState.Game || MenuManager.instance.gameState == GameState.Tutorial)
            if (Input.touchCount > 0)
                if (Input.GetButton("Fire1") && !EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId) && !IsPointerOverUIObject())
                {
                    ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                    if (Physics.Raycast(ray, out hit, 1000, 1 << LayerMask.NameToLayer("Water")))
                    {
                        switch (hit.collider.tag)
                        {
                            case "Water": return hit.point;
                            case "Limit": return hit.point;
                        }
                    }
                }

        return Vector3.zero;
    }


    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}
