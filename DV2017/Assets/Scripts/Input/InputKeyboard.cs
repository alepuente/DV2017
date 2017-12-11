using UnityEngine;
using UnityEngine.EventSystems;

public class InputKeyboard : IInput
{
    Ray ray;
    RaycastHit hit;

    public Vector3 getSelection()
    {
        if (MenuManager.instance.gameState == GameState.Game || MenuManager.instance.gameState == GameState.Tutorial)
            if (Input.GetButton("Fire1") && !EventSystem.current.IsPointerOverGameObject())
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

}