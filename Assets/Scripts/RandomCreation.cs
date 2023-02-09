using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RandomCreation : MonoBehaviour
{
    [SerializeField] GameObject prefab;

    [Header("Raycast Settings")]
    [SerializeField] int density;

    [Space]

    [SerializeField] float minHeight;
    [SerializeField] float maxHeight;
    [SerializeField] Vector2 xRange;
    [SerializeField] Vector2 zRange;

    [Header("Prefab Variation Settings")]
    [SerializeField, Range(0, 10)] float rotateTowardsNormal;
    [SerializeField] Vector2 rotationRange;
    [SerializeField] Vector3 minScale;
    [SerializeField] Vector3 maxScale;

    public bool autoUpdate;

    private void Start()
    {
        Generate();
    }

    public void Generate()
    {
        Clear();

        for (int i = 0; i < density; i++)
        {
            float sampleX = Random.Range(xRange.x, xRange.y);
            float sampleY = Random.Range(xRange.x, xRange.y);
            Vector3 rayStart = new Vector3(sampleX, maxHeight, sampleY);

            if(!Physics.Raycast(rayStart,Vector3.down,out RaycastHit hit, Mathf.Infinity))
            {
                continue;
            }
            if (hit.point.y < minHeight)
            {
                continue;
            }

            GameObject instantiatedPrefab = (GameObject)PrefabUtility.InstantiatePrefab(this.prefab, transform);
            instantiatedPrefab.transform.position = hit.point;
            instantiatedPrefab.transform.Rotate(Vector3.up, Random.Range(rotationRange.x, rotationRange.y), Space.Self);
            //Since importing from blender makes the trees be laying on the side,
            //The X rotation is set to -90 until i fix that.
            //Once we start to instantiate the other ones, uncomment the line below and comment the other one out.
            instantiatedPrefab.transform.rotation = Quaternion.Lerp(Quaternion.Euler(-90f, 0f, 0f), transform.rotation * Quaternion.FromToRotation(instantiatedPrefab.transform.up, hit.normal), rotateTowardsNormal);
            //instantiatedPrefab.transform.rotation = Quaternion.Lerp(transform.rotation, transform.rotation * Quaternion.FromToRotation(instantiatedPrefab.transform.up, hit.normal), rotateTowardsNormal);
            instantiatedPrefab.transform.localScale = new Vector3(
                Random.Range(minScale.x, maxScale.x),
                Random.Range(minScale.y, maxScale.y),
                Random.Range(minScale.z, maxScale.z)
                );
        }
    }

    public void DrawInEditor()
    {
        this.Generate();
    }

    public void Clear()
    {
        while (transform.childCount != 0)
        {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }
    }
}
