using UnityEngine;
using System.Collections;

public class SlamOnButton : MonoBehaviour {
    private int switchState = 0;
    void FixedUpdate()
    {
        switch (switchState)
        {
            case 0:
                {
                    if (transform.localScale.x > 0.9f)
                        transform.localScale -= Vector3.one * Time.deltaTime * 0.2f;
                    else
                        switchState = 1;
                    break;
                }
            case 1:
                {
                    if (transform.localScale.x < 1.0f)
                        transform.localScale += Vector3.one * Time.deltaTime * 0.2f;
                    else
                        switchState = 0;
                    break;
                }
        }
    }
}
