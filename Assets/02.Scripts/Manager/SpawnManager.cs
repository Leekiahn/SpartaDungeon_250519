using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private void Start()
    {
        GameManager.Instance.MakeManager();
    }
}
