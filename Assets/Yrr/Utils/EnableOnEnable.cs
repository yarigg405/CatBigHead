using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class EnableOnEnable : MonoBehaviour
{
    [SerializeField] List<GameObject> enableOnEnable;
    [SerializeField] float enableDelay = 0;


    private void OnEnable()
    {
        StartCoroutine(DelayedEnable());
    }

    private IEnumerator DelayedEnable()
    {
        yield return new WaitForSeconds(enableDelay);
        foreach (var go in enableOnEnable)
        {
            go.SetActive(true);
        }
    }
}
