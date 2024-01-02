using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceControlPoints : MonoBehaviour
{
    int counter = 1;
    [SerializeField] GameObject pointPrefab;
    [SerializeField] Transform splineSegment;
    public List<GameObject> controlPoints; //Reference: https://hub.packtpub.com/arrays-lists-dictionaries-unity-3d-game-development/
    [SerializeField] private LineRenderer controlPolyLine;


    private void createPoint()
    {
        Vector2 lMousePosition = Input.mousePosition;
        var myMouseStartWorldPosition = Camera.main.ScreenToWorldPoint(lMousePosition);
        myMouseStartWorldPosition.z = 0;
        GameObject x = Instantiate(pointPrefab, myMouseStartWorldPosition, Quaternion.Euler(0, 0, 0), splineSegment);
        x.name = "P" + counter++;
        controlPoints.Add(x);
        controlPolyLine.positionCount = controlPoints.Count;
    }
    // Start is called before the first frame update
    void Start()
    {
        controlPoints = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (controlPoints.Count > 1)
        {
            for (int i = 0; i < controlPoints.Count ; i++)
            {
                controlPolyLine.SetPosition(i, controlPoints[i].transform.position);
                Debug.Log("I is: " + i);
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            createPoint();

        }
    }
}
