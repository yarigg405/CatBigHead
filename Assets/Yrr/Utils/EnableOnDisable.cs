using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ToolBox
{
    public class EnableOnDisable : MonoBehaviour
    {
        [SerializeField] GameObject[] enableOnDisable;

        private void OnDisable()
        {
            foreach (var go in enableOnDisable)
            {
                go.SetActive(true);
            }
        }
    }
}