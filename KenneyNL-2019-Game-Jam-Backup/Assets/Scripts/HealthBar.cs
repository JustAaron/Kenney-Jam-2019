using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private Transform bar;

    // Start is called before the first frame update
    void Awake()
    {
        bar = transform.Find("Bar");
    }

    public void setSize(float sizeNormalized)
    {
        bar.localScale = new Vector3(sizeNormalized, 1f, 0f);
    }

    private void LateUpdate()
    {
        transform.rotation = Quaternion.identity;
        transform.position = transform.parent.position + (Vector3.up);
    }
}
