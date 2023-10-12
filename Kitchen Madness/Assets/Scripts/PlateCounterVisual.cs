using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCounterVisual : MonoBehaviour
{
    [SerializeField] private PlatesCounter platesCounter;
    [SerializeField] private Transform counterTopPoint;
    [SerializeField] private Transform plateVisual;

    private List<GameObject> plateVisualGameObjectList;

    private void Awake()
    {
        plateVisualGameObjectList = new List<GameObject>(); // list to hold number of plates on the counter
    }

    private void Start()
    {
        platesCounter.OnPlateSpawn += PlatesCounter_OnPlateSpawn;  // event to spawn plates.
        platesCounter.OnPlateRemoval += PlatesCounter_OnPlateRemoval; // event to remmove plates
    }

    private void PlatesCounter_OnPlateRemoval(object sender, System.EventArgs e)
    {
        GameObject plateRemoved = plateVisualGameObjectList[plateVisualGameObjectList.Count - 1];

        plateVisualGameObjectList.Remove(plateRemoved);

        Destroy(plateRemoved);
    }

    private void PlatesCounter_OnPlateSpawn(object sender, System.EventArgs e)
    {
        Transform platesVisualTransform = Instantiate(plateVisual, counterTopPoint);

        float Yoffset = 0.1f;

        platesVisualTransform.localPosition = new Vector3(0f, plateVisualGameObjectList.Count * Yoffset, 0f); // shifting position of plates upwards.

        plateVisualGameObjectList.Add(platesVisualTransform.gameObject);


    }
}
