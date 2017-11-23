using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fractal : MonoBehaviour {

    public float maxRotationSpeed;
    public float maxTwist;
    public float spawnProbability;
    public int maxDepth;
    public float childScale = 0.5f;
    public Mesh[] meshes;
    public Material base_material;


    private float rotationSpeed;
    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;

    private bool root = true;

    private Material[,] materials;

    private int depth;

    private static Vector3[] childDirections = {
        Vector3.up,
        Vector3.right,
        Vector3.left,
        Vector3.forward,
        Vector3.back
    };

    private static Quaternion[] childOrientations = {
        Quaternion.identity,
        Quaternion.Euler(0f,0f,-90f),
        Quaternion.Euler(0f,0f,90f),
        Quaternion.Euler(90f,0f,0f),
        Quaternion.Euler(-90f,0f,0f)    };

	// Use this for initialization
	void Start () {

        rotationSpeed = Random.Range(-maxRotationSpeed, maxRotationSpeed);
        transform.Rotate(Random.Range(-maxTwist, maxTwist), 0f, 0f);

        if (materials == null) {
            InitializeMaterials();
        }

        meshFilter = gameObject.AddComponent<MeshFilter>();
        meshFilter.mesh = meshes[Random.Range(0, meshes.Length)];

        meshRenderer = gameObject.AddComponent<MeshRenderer>();
        meshRenderer.material = materials[depth, Random.Range(0,2)];

        if (depth < maxDepth) {
            StartCoroutine(CreateChildren(root));
        }

        root = false;

    }

    private void InitializeMaterials() {
        materials = new Material[maxDepth + 1, 2];

        for (int i = 0; i < maxDepth; i++) {

            float t = i / (maxDepth - 1f);
            t *= t;

            materials[i, 0] = SetMaterial(Color.Lerp(Color.white, Color.yellow, t));
            materials[i, 1] = SetMaterial(Color.Lerp(Color.white, Color.cyan, t));
        }

        materials[maxDepth, 0] = SetMaterial(Color.red);
        materials[maxDepth, 1] = SetMaterial(Color.magenta);
    }

    private Material SetMaterial(Color color) {
        Material mat = new Material(base_material);
        mat.color = color;
        return mat;
    }

    private IEnumerator CreateChildren(bool root) {

        for (int i = 0; i < childDirections.Length; i++) {

            if (Random.value < spawnProbability) {

                yield return new WaitForSeconds(Random.Range(0.1f, 0.5f));

                GameObject child = new GameObject("Fractal Child");
                child.AddComponent<Fractal>().Initialize(this, i);

            }
        }

        yield return new WaitForSeconds(Random.Range(0.1f, 0.5f));

        GameObject child2 = new GameObject("Fractal Child");
        child2.AddComponent<Fractal>().Initialize(this, 0, true);

    }

    private void Initialize(Fractal parent, int index, bool inverse = false) {

        maxTwist = parent.maxTwist;
        spawnProbability = parent.spawnProbability;
        meshes = parent.meshes;
        materials = parent.materials;
        maxDepth = parent.maxDepth;
        depth = parent.depth + 1;
        transform.parent = parent.transform;
        maxRotationSpeed = parent.maxRotationSpeed;

        childScale = parent.childScale;

        transform.localScale = Vector3.one * childScale;
        transform.localPosition = childDirections[index] * (0.5f + 0.5f * childScale);
        transform.localRotation = childOrientations[index];
        
        if (inverse) {
            transform.localPosition = transform.localPosition * -1;
            transform.localRotation = Quaternion.Inverse(transform.localRotation);
        }
        
    }

   
	
	// Update is called once per frame
	void Update () {

        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);

	}
}
