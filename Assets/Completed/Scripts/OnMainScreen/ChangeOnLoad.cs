using UnityEngine;
using System.Collections;

public class ChangeOnLoad : MonoBehaviour {

    public AudioClip level1Music;
    private AudioSource source1;

	// Use this for initialization
	void Start () {
        source1 = GetComponent<AudioSource>();
	}

    void OnLevelWasLoaded(int level)
    {
        switch (level)
        {
            case 1:
                {
                    source1.clip = level1Music;
                    source1.Play();
                    break;
                }
        }
    }
}
