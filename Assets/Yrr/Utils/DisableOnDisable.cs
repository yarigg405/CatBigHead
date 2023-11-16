using System.Collections.Generic;
using UnityEngine;

public class DisableOnDisable : MonoBehaviour
{
    [SerializeField] List<GameObject> disableOnDisable;


    private void OnDisable()
    {
        foreach (var go in disableOnDisable)
        {
            if (go)
                go.SetActive(false);
        }
    }
}
