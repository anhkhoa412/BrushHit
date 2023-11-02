using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineManager : MonoBehaviour
{
    private LineRenderer lr;
    private BoxCollider box;
    [SerializeField] private Transform[] points;

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
        box = GetComponent<BoxCollider>(); 
    }

    public void SetUpLine(Transform[] points)
    {
        lr.positionCount = points.Length;
        this.points = points;

        UpdateBoxColliderSize(); 
    }

    // Function to update the BoxCollider size based on the line's length.
    private void UpdateBoxColliderSize()
    {
        Vector3 lineStart = points[0].position;
        Vector3 lineEnd = points[points.Length - 1].position;
        float lineLength = Vector3.Distance(lineStart, lineEnd);

        // Adjust the BoxCollider's size.
        box.size = new Vector3(lr.startWidth, lr.startWidth, lineLength);
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < points.Length; i++)
        {
            lr.SetPosition(i, points[i].position);
            transform.position = lr.transform.position;
        }
    }
}
