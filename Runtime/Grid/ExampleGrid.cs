using VanceUtility.Grid; // Import the package namespace when using the package
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VanceUtility.Util;


public class ExampleGrid : MonoBehaviour
{
    [SerializeField] private CustomGrid<GameObject> myGrid;
    [SerializeField] private GameObject prefab;

    void Start() {
        myGrid = new CustomGrid<GameObject>(11, 11, 10f, 10f, transform, true);
        myGrid.SetAllCellData(prefab);
        
        Debug.Log("Grid initialized.");
        Debug.Log(myGrid);
        RenderCubes();
        
    }

    void Update() {
        myGrid.DrawGrid();

        if(Input.GetMouseButtonDown(0)) {
            Debug.Log("Clicked!");
            Debug.Log(Mouse.GetMouseWorldPosition());
            Vector3 worldPosition = Mouse.GetMouseWorldPosition();
            worldPosition.y = 0;
            Debug.Log(worldPosition);
            // myGrid.SetCellData(worldPosition, "boop");
        }
    }

    private void RenderCubes() {
        // for each cell, render the object there.
        for(var i = 0; i < this.myGrid.Rows; i++) {
            for (var j = 0; j < this.myGrid.Columns; j++) {
                GameObject cellPrefab = myGrid.GetCellData(i, j);
                Vector3 cellCenter = myGrid.GetCellCenter(i, j);
                Instantiate(cellPrefab, cellCenter, Quaternion.identity);
            }
        }
    }
}