using UnityEngine;
using System.Collections;

public class TinPinObjectController : MonoBehaviour {

    public enum STATUS
    {
        ATTACKING,
        PATROLLING,
        HAULTED,
        ESCAPING
    };

    public float initialScale;
    public float shrinkScale;
    public float shrinkSpeed;

    public float friction;

}
