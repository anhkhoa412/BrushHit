using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Rubber : MonoBehaviour
{
    private Rigidbody rb;
    
    private bool isColliding = false; // Flag to track collision with the player

    public Material pinkMaterial; // Assign the pink material in the Inspector
    public Material greenMaterial;
    public bool isChangePlayerColor;
    public bool isChangeEnemyColor;

    [SerializeField] private GameObject changeColorEffect;
    [SerializeField] private GameObject enemyChangeColorEffect;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (!isColliding)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);

        } 
    }
    private void FixedUpdate()
    {
        rb.AddForce(Vector3.down * 1000f);
    }

    void LateUpdate()
    {
        if (isColliding)
        {
            // Limit the brush's rotation to -50 to 50 degrees
            Vector3 currentRotation = transform.rotation.eulerAngles;
            currentRotation.x = Mathf.Clamp(currentRotation.x, -40f, 40f);
            currentRotation.z = Mathf.Clamp(currentRotation.z, -40f, 40f);
            transform.rotation = Quaternion.Euler(currentRotation);
        }
    }


    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && GameManager.Instance.isStart)
        {
            isColliding = true;
            if (!isChangePlayerColor || isChangeEnemyColor)
            {
                ChangeMaterialColor(pinkMaterial);

                GameObject effect1 = ObjectPooling.Instance.GetPooledObject("PlayerChangeColorEffect");

                if (effect1 != null)
                {
                    effect1.transform.position = transform.position;
                    effect1.transform.rotation = transform.rotation;
                    effect1.SetActive(true);
                    ObjectPooling.Instance.ReturnToPool(effect1, 0.2f);
                }
               
                isChangePlayerColor = true;
                
                isChangeEnemyColor = false;
            }
        }

        if (collision.gameObject.CompareTag("Enemy") && GameManager.Instance.isStart)
        {
            if (!isChangeEnemyColor || isChangePlayerColor)
            {
                ChangeMaterialColor(greenMaterial);
                GameObject effect2 =  ObjectPooling.Instance.GetPooledObject("EnemyChangeColorEffect");
                
                if (effect2 != null)
                {
                    effect2.transform.position = transform.position;
                    effect2.transform.rotation = transform.rotation;
                    effect2.SetActive(true);
                    ObjectPooling.Instance.ReturnToPool(effect2, 0.2f);
                }
                
                isChangeEnemyColor = true;
                isChangePlayerColor = false;
             
            }
        }

    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Enemy"))
        {
            isColliding = false;

        }
    }

    void ChangeMaterialColor(Material newMaterial)
    {
        GetComponent<Renderer>().material = newMaterial;
       
        if (newMaterial == pinkMaterial)
        {
            GameManager.Instance.playerScore++;
            GameManager.Instance.playerColorCount++;
          
            if (!GameManager.Instance.isMute)
                GameManager.Instance.hitSound.Play();
            
        }

        if (newMaterial == greenMaterial)
        {
            
            GameManager.Instance.enemyColorCount++;
        }


    }

        
        
}
