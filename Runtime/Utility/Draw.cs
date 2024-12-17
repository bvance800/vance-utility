using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VanceUtility.Util {

    public class Draw
    {
        public static void DrawX(Vector3 center, float length, Color color) {
            Vector3 offset = new Vector3(length / 2, 0, length / 2);

            // Draw two diagonal lines to form an "X"
            Debug.DrawLine(center - offset, center + offset, color);          // Line from bottom-left to top-right
            Debug.DrawLine(center + new Vector3(-offset.x, 0, offset.z),      // Line from top-left to bottom-right
                        center + new Vector3(offset.x, 0, -offset.z), color);
        }

        public static void DrawArrow(Vector3 start, Vector3 direction, float length, Color color)
        {
            // Calculate the end point of the arrow shaft
            Vector3 end = start + direction.normalized * length;

            // Draw the main shaft of the arrow
            Debug.DrawLine(start, end, color);

            // Calculate arrowhead lines
            float arrowHeadAngle = 20f; // Angle of arrowhead lines in degrees
            float arrowHeadLength = length * 0.2f; // Arrowhead length as a fraction of total length

            Vector3 rightHead = Quaternion.Euler(0, arrowHeadAngle, 0) * -direction;
            Vector3 leftHead = Quaternion.Euler(0, -arrowHeadAngle, 0) * -direction;

            // Draw the arrowhead
            Debug.DrawLine(end, end + rightHead.normalized * arrowHeadLength, color);
            Debug.DrawLine(end, end + leftHead.normalized * arrowHeadLength, color);
        }
    }
}
