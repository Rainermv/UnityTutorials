using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierCurve : MonoBehaviour {

    public Vector3[] points;


    public void Reset() {
        points = new Vector3[] {
            new Vector3(1f,0f,0f),
            new Vector3(2f,0f,0f),
            new Vector3(3f,0f,0f)
        };
    }

    // Get a point in the line related to the given parametric position (0 to 1)
    public Vector3 GetPoint(float t) {

        Vector3 bezierLocalPoint = Bezier.GetPoint(points[0], points[1], points[2]);

        return transform.TransformPoint(bezierLocalPoint);

    }
}
