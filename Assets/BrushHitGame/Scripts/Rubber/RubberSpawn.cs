using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubberSpawn : MonoBehaviour
{
    public GameObject brushPrefab; // Assign the rubber prefab in the Inspector
    public float spacing = 1.0f; // Spacing between rubber
    public bool isFinish = false;
    [SerializeField] private List<GameObject> rubbers = new List<GameObject>(); // Store references to the instantiated rubber
    public int playerColorCount = 0;
    public int enemyColorCount = 0;
    void Start()
    {
        // Calculate the size of the plane
        Renderer planeRenderer = GetComponent<Renderer>();
        Vector3 planeSize = planeRenderer.bounds.size;

        // Calculate the number of rows and columns based on the plane size and spacing
        int rows = Mathf.FloorToInt(planeSize.z / spacing);
        int cols = Mathf.FloorToInt(planeSize.x / spacing);

        // Calculate the starting position
        Vector3 startPosition = transform.position - new Vector3((cols - 1) * spacing / 2, 0, (rows - 1) * spacing / 2);

        // Instantiate rubber in a grid and store references in the list
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                Vector3 capsulePosition = startPosition + new Vector3(j * spacing, transform.position.y + 0.18f, i * spacing);
                GameObject newBrush = Instantiate(brushPrefab, capsulePosition, Quaternion.identity);
                newBrush.transform.parent = transform;
                rubbers.Add(newBrush); // Add the reference to the list
            }
        }
    }

    private void Update()
    {
        if (AreAllBrushesChangedColor())
        {
            isFinish = true;    
        }
        
              
                
            
        
    }
    private bool AreAllBrushesChangedColor()
    {
        foreach (GameObject rubber in rubbers)
        {
            Rubber rubberS = rubber.GetComponent<Rubber>();
            if (rubberS != null && !rubberS.isChangePlayerColor && !rubberS.isChangeEnemyColor)
            {
                return false; 
            }
        }
        return true; 
    }


    }
    
