using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierCurve : MonoBehaviour {

    public Vector3[] points;


    public void Reset() {
        points = new Vector3[] {
            new Vector3(1f,0f,0f),
            new Vector3(2f,0f,0f),
            new Vector3(3f,0f,0f),
            new Vector3(4f,0f,0f)
        };
    }

    // Get a point in the line related to the given parametric position (0 to 1)
    public Vector3 GetPoint(float t) {

        Vector3 bezierLocalPoint = Bezier.GetPoint(points[0], points[1], points[2], points[3], t);

        return transform.TransformPoint(bezierLocalPoint);

    }

    // the speed in which we move along the curve
    public Vector3 GetVelocity(float t) {

        Vector3 localDerivative = Bezier.GetFirstDerivative(points[0], points[1], points[2], points[3], t);

        // since the derivative produces a velocity vector (not a point), it should not be affected by
        // the position of the curve, so we subtract that after transforming
        return transform.TransformPoint(localDerivative) - transform.position;
    }

    public Vector3 GetDirection(float t) {
        return GetVelocity(t).normalized;
    }
}
