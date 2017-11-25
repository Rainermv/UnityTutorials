using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierSpline : MonoBehaviour {

    public Vector3[] points;

    public int CurveCount {
        get {
            return (points.Length - 1) / 3;
        }
    }


    public void Reset() {
        points = new Vector3[] {
            new Vector3(1f,0f,0f),
            new Vector3(2f,0f,0f),
            new Vector3(3f,0f,0f),
            new Vector3(4f,0f,0f)
        };
    }

    public void AddCurve() {

        Vector3 point = points[points.Length - 1];

        Array.Resize(ref points, points.Length + 3); // add 3 points to the array

        AddPoint(point, 3);
        AddPoint(point, 2);
        AddPoint(point, 1);

    }

    private void AddPoint(Vector3 point, int posDiff) {

        point.x += 1f;
        points[points.Length - posDiff] = point;

    }


    // Get a point in the line related to the given parametric position (0 to 1)
    public Vector3 GetPoint(float t) {

        // T is the fractional part (0 to 1) to get the interpolation value for the sline curve 
        // where 0 is the beggining of the spline, 1 is the end
        
        // However, we first have to find witch curve we are in

        int i; // the point in which will be returned
        
        // if t is 1 or more, set it to the last curve
        if (t >= 1f) {

            t = 1f;
            i = points.Length - 4;
        }
        else {

            t = Mathf.Clamp01(t) * CurveCount;
            i = (int)t;
            t -= 1;
            i *= 3;

            // to get the ACTUAL points, the curve index must be multiplied by 3
        }

        Vector3 bezierLocalPoint = Bezier.GetPoint(points[i], points[i+1], points[i+2], points[i+3], t);

        return transform.TransformPoint(bezierLocalPoint);

    }

    // the speed in which we move along the curve
    public Vector3 GetVelocity(float t) {
        int i;
        if (t >= 1f) {
            t = 1f;
            i = points.Length - 4;
        }
        else {
            t = Mathf.Clamp01(t) * CurveCount;
            i = (int)t;
            t -= i;
            i *= 3;
        }

        Vector3 localDerivative = Bezier.GetFirstDerivative(points[i], points[i+1], points[i + 2], points[i + 3], t);

        // since the derivative produces a velocity vector (not a point), it should not be affected by
        // the position of the curve, so we subtract that after transforming
        return transform.TransformPoint(localDerivative) - transform.position;
    }

    public Vector3 GetDirection(float t) {
        return GetVelocity(t).normalized;
    }
}
