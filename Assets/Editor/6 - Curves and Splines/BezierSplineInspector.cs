using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor (typeof(BezierSpline))]
public class BezierSplineInspector : Editor {

    private const int lineSteps = 10;
    private const float directionScale = 0.5f;

    private BezierSpline spline;
    private Transform handleTransform;
    private Quaternion handleRotation;

    private void OnSceneGUI() {

        spline = target as BezierSpline;
        handleTransform = spline.transform;
        handleRotation = Tools.pivotRotation == PivotRotation.Local ?
            handleTransform.rotation : Quaternion.identity;

        Vector3 p0 = ShowPoint(0);
        for (int i = 1; i < spline.points.Length; i+=3) {

            Vector3 p1 = ShowPoint(i);
            Vector3 p2 = ShowPoint(i+1);
            Vector3 p3 = ShowPoint(i+2);

            // Draw direct lines in gray
            Handles.color = Color.gray;
            Handles.DrawLine(p0, p1);
            Handles.DrawLine(p2, p3);

            // DrawLines();
            Handles.DrawBezier(p0, p3, p1, p2, Color.white, null, 2f);

            p0 = p3;
        }

        ShowDirections();

    }


    public override void OnInspectorGUI() {
        //base.OnInspectorGUI();

        DrawDefaultInspector();
        spline = target as BezierSpline;

        if (GUILayout.Button("Add Curve")) {

            Undo.RecordObject(spline, "Add Curve");
            spline.AddCurve();
            EditorUtility.SetDirty(spline);

        }

    }

    // Draw line velocity in green
    private void ShowDirections() {

        Handles.color = Color.green;
        Vector3 point = spline.GetPoint(0f);

        Handles.DrawLine(point, point + spline.GetDirection(0f) * directionScale);

        for (int i = 0; i < lineSteps; i++) {

            point = spline.GetPoint(i / (float)lineSteps);
            Handles.DrawLine(point, point + spline.GetDirection(i / (float)lineSteps) * directionScale);
        }
    }


    // Deprecated in favor of "DrawBezier"
    private void DrawLines() {

        Vector3 lineStart = spline.GetPoint(0f); // starting point

        // Draw line veocity in green
        Handles.color = Color.green;
        Handles.DrawLine(lineStart, lineStart + spline.GetDirection(0f));

        for (int i = 0; i < lineSteps; i++) {

            // Bezier curves are parametric - 0.0 is the start 1.0 is the end
            float linePosition = i / (float)lineSteps;
            Vector3 lineEnd = spline.GetPoint(linePosition);


            // Draw curve in white
            Handles.color = Color.white;
            Handles.DrawLine(lineStart, lineEnd);

            // Draw line veocity in green
            Handles.color = Color.green;
            Handles.DrawLine(lineEnd, lineEnd + spline.GetDirection(i / (float)lineSteps));

            lineStart = lineEnd;

        }


    }

    private Vector3 ShowPoint(int index) {

        Vector3 point = handleTransform.TransformPoint(spline.points[index]);

        EditorGUI.BeginChangeCheck();
        point = Handles.DoPositionHandle(point, handleRotation);
        if (EditorGUI.EndChangeCheck()) {
            // undo drag operations before changes
            Undo.RecordObject(spline, "Move Point");
            EditorUtility.SetDirty(spline);

            // convert point from world coordinates (handle) to local coordinates (curve)
            spline.points[index] = handleTransform.InverseTransformPoint(point);
        }

        return point;


    }

}
