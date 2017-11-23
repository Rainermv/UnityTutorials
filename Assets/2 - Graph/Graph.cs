using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour {

    public GraphType type;

    public Transform pointPrefab;

    Transform[] points;

    const float SIZE = 1F;

    [Range (10, 100)]
    public int resolution = 10;

    void Awake() {

        points = new Transform[resolution];

        float step = 2f / resolution;
        float shift = SIZE * 0.5f;

        Vector3 local_scale = Vector3.one * step;
        Vector3 position;

        position.y = 0f;
        position.z = 0f;

        for (int i = 0; i < resolution; i++) {

            Transform point = Instantiate(pointPrefab);

            position.x = (i + shift) * step - SIZE;
            //position.y = position.x * position.x;

            point.localPosition = position;
            point.localScale = local_scale;

            point.SetParent(transform, false);

            points[i] = point;
        }
        
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        for (int i = 0; i < points.Length; i++) {

            Transform point = points[i];

            Vector3 position = point.localPosition;

            switch (type) {
                case GraphType.LINEAR:
                    position.y = position.x;
                    break;
                case GraphType.SQUARE:
                    position.y = position.x * position.x;
                    break;
                case GraphType.CUBIC:
                    position.y = position.x * position.x * position.x;
                    break;
                case GraphType.SIN:
                    position.y = Mathf.Sin(Mathf.PI * (position.x + Time.time));
                    break;
                default:
                    break;
            }

            point.localPosition = position;
        }
	}
}
