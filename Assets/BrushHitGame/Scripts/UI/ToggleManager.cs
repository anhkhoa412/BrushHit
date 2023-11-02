using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleManager : MonoBehaviour
{
    public GameObject highlight;

    private void Start()
    {
        highlight.SetActive(false);
    }
}
