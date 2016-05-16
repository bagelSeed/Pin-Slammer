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

    [HideInInspector]public Rigidbody2D rb2d;		//Store a reference to the Rigidbody2D component required to use 2D Physics.

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
