using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VanceUtility.Util;

namespace VanceUtility.Grid {
    public class CustomGrid<T> {
        public Transform parentTransform { get; private set; }
        public int Rows { get; private set; }
        public int Columns { get; private set; }
        public float CellWidth { get; private set; }
        public float CellHeight { get; private set; }
        public Vector3 Origin { get; private set; }

        private bool _centeredOnParent = false;
        [SerializeField] private Dictionary<(int, int), T> _cellData;

        public CustomGrid(int rows, int columns, float cellWidth, float cellHeight, Transform parentTransform, bool centeredOnParent = false) {
            Rows = rows;
            Columns = columns;
            CellWidth = cellWidth;
            CellHeight = cellHeight;
            _cellData = new Dictionary<(int, int), T>();
            this.parentTransform = parentTransform;
            this._centeredOnParent = centeredOnParent;
        }

        private Vector3 GetGridOrigin() {
            if (!this._centeredOnParent) {
                return this.parentTransform.position;
            } else {
                return this.parentTransform.position - (Vector3.right * ((Columns / 2f) * CellWidth)) - (Vector3.forward * ((Rows / 2f) * CellHeight));
            }
        }

        public Vector3 GetCellCenter(int gridX, int gridY) {
            Vector3 origin = GetGridOrigin();
            float x = origin.x + gridX * CellWidth + CellWidth / 2;
            float z = origin.z + gridY * CellHeight + CellHeight / 2;
            return new Vector3(x, origin.y, z);
        }

        public void DrawGrid(bool withCenters = true) {
            DrawGrid(Color.green, withCenters); // Call the main method with a default color
        }
        public void DrawGrid(Color color, bool withCenters) {
            for (int y = 0; y < Rows; y++)
            {
                for (int x = 0; x < Columns; x++)
                {
                    Vector3 cellCenter = GetCellCenter(x, y);

                    // Draw.DrawArrow(cellCenter, Vector3.right, 0.6f, Color.red);
                    //Top Line
                    Debug.DrawLine((cellCenter - (Vector3.right * CellWidth / 2) + (Vector3.forward * CellHeight / 2)), (cellCenter + (Vector3.right * CellWidth / 2) + (Vector3.forward * CellHeight / 2)) , color);
                    //Bottom Line
                    Debug.DrawLine((cellCenter - (Vector3.right * CellWidth / 2) - (Vector3.forward * CellHeight / 2)), (cellCenter + (Vector3.right * CellWidth / 2) - (Vector3.forward * CellHeight / 2)) , color);
                    // Left Line
                    Debug.DrawLine((cellCenter - (Vector3.forward * CellHeight / 2) - (Vector3.right * CellWidth / 2)), (cellCenter + (Vector3.forward * CellHeight / 2) - (Vector3.right * CellWidth / 2)), color);
                    // Right Line
                    Debug.DrawLine((cellCenter - (Vector3.forward * CellHeight / 2) + (Vector3.right * CellWidth / 2)), (cellCenter + (Vector3.forward * CellHeight / 2) + (Vector3.right * CellWidth / 2)), color);
  
                    if(withCenters) {
                        Draw.DrawX(cellCenter, 0.5f, color);
                    }
                }
            }
        }

        public void DrawGridText() {
             for (int row = 0; row < Rows; row++)
            {
                for (int col = 0; col < Columns; col++)
                {
                    Vector3 cellCenter = GetCellCenter(row, col);

                    var key = (row, col);
                    if (_cellData.TryGetValue(key, out T value)) {
                        // Successfully found the value
                        Debug.Log($"Cell at ({row}, {col}) contains: {value}");
                        TextMesh text = Text.CreateWorldText(value.ToString(), parentTransform, cellCenter, 20, Color.white);
                        // text.transform.forward = parentTransform.up;
                    } else {
                        // Key not found in the dictionary
                        Debug.Log($"Cell at ({row}, {col}) is empty or has no data");
                    }

                }
            }

        }

        public void SetCellData(int x, int y, T data) {
            Debug.Log("x");
            Debug.Log(x);
            Debug.Log("y");
            Debug.Log(y);
            if( x >= 0 && y >= 0 && x < Columns && y < Rows){
                _cellData[(x, y)] = data;
            }
        }

        public void SetCellData(Vector3 worldPosition, T data) {
            var (x, y) = GetXY(worldPosition);
            SetCellData(x, y, data);
        }

        public void SetAllCellData(T data) {
            for(int i = 0; i < Rows; i++) {
                for(int j = 0; j < Columns; j++) {
                    SetCellData(i, j, data);
                }
            }
        }

        public T GetCellData(int x, int y) {
            return _cellData[(x,y)];
        }

        public Vector3 GetWorldPosition(int row, int column) {
            return new Vector3(row * (float)CellHeight, column * (float)CellWidth) + Origin;
        }

        public (int, int) GetXY(Vector3 worldPosition) {
            int x = Mathf.FloorToInt(worldPosition.x / CellWidth);
            int y = Mathf.FloorToInt(worldPosition.y / CellHeight);
            return (x, y);
        }

        public (int, int) GetGridCellPosition(Vector3 worldPosition) {
            int y = Mathf.FloorToInt((worldPosition - Origin).y / CellHeight);
            int x = Mathf.FloorToInt((worldPosition - Origin).x / CellWidth);

            return (x, y);
        }

        public void PrintGridContents() {
            foreach (var cell in _cellData) {
                var coordinates = cell.Key;
                var data = cell.Value;

                string output;
                
                // Check if data is a string
                if (data is string strData)
                {
                    output = strData;
                }
                // Check if data is a basic type (int, float, etc.)
                else if (data is int || data is float || data is double || data is bool)
                {
                    output = data.ToString();
                }
                // Check if data has a meaningful ToString implementation
                else if (data != null && data.ToString() != data.GetType().ToString())
                {
                    output = data.ToString();
                }
                // Default message for unsupported types
                else
                {
                    output = "Unsupported type";
                }

                Debug.Log($"Cell ({coordinates.Item1}, {coordinates.Item2}): {output}");
            }
        }

    }
}
