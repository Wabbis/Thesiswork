using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class FaceCamera : MonoBehaviour
{
	public Transform target;


    // Start is called before the first frame update
    void Start()
    {
		target = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(target)
		{
			// transform.LookAt(2 * transform.position - target.position);
		}

		transform.LookAt(SceneView.lastActiveSceneView.pivot);
    }
}
