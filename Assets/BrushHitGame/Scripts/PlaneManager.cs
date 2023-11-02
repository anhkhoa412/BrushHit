using System.Collections.Generic;
using UnityEngine;

public class PlaneManager : MonoBehaviour
{
    public GameObject[] planes;

    // This function collects the brushes associated with each plane.
    private void Update()
    {
        if (CheckWin() && GameManager.Instance.isNormalMode)
        {
            GameManager.Instance.Win();
        }

        if (EnemyCheckWin() && !GameManager.Instance.isNormalMode)
        {
            GameManager.Instance.Win();
        }
    }

    private bool CheckWin()
    {

        if (GameManager.Instance.isNormalMode)
        {
            foreach (GameObject plane in planes)
            {
                RubberSpawn rubberSpawn = plane.GetComponent<RubberSpawn>();
                if (rubberSpawn != null && !rubberSpawn.isFinish)
                {
                    return false;
                }
            }
        }
        return true;
    }

    private bool EnemyCheckWin()
    {
        if (!GameManager.Instance.isNormalMode)
        {
            foreach (GameObject plane in planes)
            {
                RubberSpawn rubberSpawn = plane.GetComponent<RubberSpawn>();
                if (rubberSpawn.isFinish)
                {
                    if (GameManager.Instance.playerColorCount > GameManager.Instance.enemyColorCount)
                    {
                        return true;
                    } else if (GameManager.Instance.playerColorCount < GameManager.Instance.enemyColorCount)
                    {
                        GameManager.Instance.GameOver();
                    }
                    }


                }
            }
            return false;

        }
    }
