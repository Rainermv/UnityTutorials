using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor (typeof(BezierCurve))]
public class BezierCurveInspector : Editor {

    private const int lineSteps = 10;

    private BezierCurve curve;
    private Transform handleTransform;
    private Quaternion handleRotation;

    private void OnSceneGUI() {

        curve = target as BezierCurve;
        handleTransform = curve.transform;
        handleRotation = Tools.pivotRotation == PivotRotation.Local ?
            handleTransform.rotation : Quaternion.identity;

        Vector3 p0 = ShowPoint(0);
        Vector3 p1 = ShowPoint(1);
        Vector3 p2 = ShowPoint(2);

        // Draw direct lines in gray
        Handles.color = Color.gray;
        Handles.DrawLine(p0, p1);
        Handles.DrawLine(p1, p2);

        // Draw curve in white
        Handles.color = Color.white;
        Vector3 lineStart = curve.GetPoint(0f); // starting point
        for (int i = 0; i < lineSteps; i++) {

            // Bezier curves are parametric - 0.0 is the start 1.0 is the end
            float linePosition = i / (float)lineSteps;
            Vector3 lineEnd = curve.GetPoint(linePosition);
            Handles.DrawLine(lineStart, lineEnd);
            lineStart = lineEnd;

        }


    }

    private Vector3 ShowPoint(int index) {

        Vector3 point = handleTransform.TransformPoint(curve.points[index]);

        EditorGUI.BeginChangeCheck();
        point = Handles.DoPositionHandle(point, handleRotation);
        if (EditorGUI.EndChangeCheck()) {
            // undo drag operations before changes
            Undo.RecordObject(curve, "Move Point");
            EditorUtility.SetDirty(curve);

            // convert point from world coordinates (handle) to local coordinates (curve)
            curve.points[index] = handleTransform.InverseTransformPoint(point);
        }

        return point;


    }

}
