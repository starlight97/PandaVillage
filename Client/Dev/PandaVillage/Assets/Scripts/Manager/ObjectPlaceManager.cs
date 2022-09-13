using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObjectPlaceManager : MonoBehaviour
{
    private Coroutine buildingEditRoutine;
    private GameObject gridGo;
    public UnityAction onEditComplete;



    private int width = 10;
    private int height = 10;

    private void Start()
    {
        gridGo = transform.Find("Grid").gameObject;
    }

    public void BuildingEdit(GameObject selectedBuildingGo)
    {
        if (buildingEditRoutine == null)
            buildingEditRoutine = StartCoroutine(BuildingEditRoutine(selectedBuildingGo));
    }

    private IEnumerator BuildingEditRoutine(GameObject selectedBuildingGo)
    {
        yield return new WaitForSeconds(0.1f);
        while(true)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (Input.GetMouseButtonDown(0))
            {
                break;
            }
            selectedBuildingGo.transform.position = new Vector3(mousePos.x, mousePos.y, 0);
            yield return null;
        }
        this.onEditComplete();
        buildingEditRoutine = null;
    }

    private void CreateTile()
    {
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {

            }
        }
    }
}
