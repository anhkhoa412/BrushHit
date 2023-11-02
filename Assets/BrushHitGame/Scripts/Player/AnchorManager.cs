using UnityEngine;

public class AnchorManager : MonoBehaviour
{
    public Transform centerAnchor;
    public Transform targetAnchor1;
    public Transform targetAnchor2;
    public float orbitSpeed = 20f;
    public Transform currentAnchor;

    [SerializeField] private bool isEnemy = false;
    [SerializeField] private float timeBetweenChange = 2.0f; // Set a default value
    private float nextTimeChange;

    private void Awake()
    {
        currentAnchor = centerAnchor;
        nextTimeChange = Time.time + Random.Range(1.0f, timeBetweenChange);
    }

    private void Update()
    {
        // Calculate the position to move the sphere around the current target sphere
        transform.RotateAround(currentAnchor.position, Vector3.up, orbitSpeed * Time.deltaTime);

        if (!isEnemy && Input.GetMouseButtonDown(0) && !GameManager.Instance.IsPointerOverUIElement() && IsValidMove())
        {
            ChangeAnchor();
        }

        if (isEnemy && Time.time > nextTimeChange && IsValidMove())
        {
            ChangeAnchor();
            timeBetweenChange = Random.Range(1.0f, 5.0f);
            nextTimeChange = Time.time + timeBetweenChange;
        }
    }

    public void ChangeAnchor()
    {
        currentAnchor = (currentAnchor == centerAnchor) ? targetAnchor1 : targetAnchor2;
    }

    public bool IsValidMove()
    {
        Ray ray1 = CreateRay(targetAnchor1);
        Ray ray2 = CreateRay(targetAnchor2);

        if (Physics.Raycast(ray1, out var hit1, 30.0f) && Physics.Raycast(ray2, out var hit2, 30.0f) && GameManager.Instance.isStart)
        {
            if (hit1.collider.CompareTag("Plane") || hit1.collider.CompareTag("Rubber"))
            {
                return true;
            }
        }
        else if (!isEnemy)
        {
            GameManager.Instance.GameOver();
        }

        return false;
    }

    private Ray CreateRay(Transform anchor)
    {
        return new Ray(anchor.position + new Vector3(0, 0f, 0), anchor.TransformDirection(Vector3.down));
    }
}
