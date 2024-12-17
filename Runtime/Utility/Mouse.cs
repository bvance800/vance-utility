using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VanceUtility.Util {
    public class Mouse {
        public static Vector3 GetMouseWorldPosition() {
            return GetMouseWorldPosition(Input.mousePosition, Camera.main);
        }
        public static Vector3 GetMouseWorldPosition(Camera worldCamera) {
            return GetMouseWorldPosition(Input.mousePosition, worldCamera);
        }
        public static Vector3 GetMouseWorldPosition(Vector3 screenPosition, Camera worldCamera) {
            Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
            return worldPosition;
        }
    }

}
