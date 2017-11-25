using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor (typeof(BezierCurve))]
public class BezierCurveInspector : Editor {

    private const int lineSteps = 10;
    private const float directionScale = 0.5f;

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
        Vector3 p3 = ShowPoint(3);

        // Draw direct lines in gray
        Handles.color = Color.gray;
        Handles.DrawLine(p0, p1);
        Handles.DrawLine(p1, p2);
        Handles.DrawLine(p2, p3);

        // DrawLines();

        ShowDirections();
        Handles.DrawBezier(p0, p3, p1, p2, Color.white, null, 2f);


    }

    // Draw line velocity in green
    private void ShowDirections() {

        Handles.color = Color.green;
        Vector3 point = curve.GetPoint(0f);

        Handles.DrawLine(point, point + curve.GetDirection(0f) * directionScale);

        for (int i = 0; i < lineSteps; i++) {

            point = curve.GetPoint(i / (float)lineSteps);
            Handles.DrawLine(point, point + curve.GetDirection(i / (float)lineSteps) * directionScale);
        }
    }


    // Deprecated in favor of "DrawBezier"
    private void DrawLines() {

        Vector3 lineStart = curve.GetPoint(0f); // starting point

        // Draw line veocity in green
        Handles.color = Color.green;
        Handles.DrawLine(lineStart, lineStart + curve.GetDirection(0f));

        for (int i = 0; i < lineSteps; i++) {

            // Bezier curves are parametric - 0.0 is the start 1.0 is the end
            float linePosition = i / (float)lineSteps;
            Vector3 lineEnd = curve.GetPoint(linePosition);


            // Draw curve in white
            Handles.color = Color.white;
            Handles.DrawLine(lineStart, lineEnd);

            // Draw line veocity in green
            Handles.color = Color.green;
            Handles.DrawLine(lineEnd, lineEnd + curve.GetDirection(i / (float)lineSteps));

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
