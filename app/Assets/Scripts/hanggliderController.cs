using UnityEngine;
using System.Collections;

// Colorado River - Grand Canyon (Video to Mesh)(https://sketchfab.com/models/c29a84f14a3f45cdaf1166cd15756a6e) by dtmcnamara(https://sketchfab.com/dtmcnamara) is licensed under CC Attribution(http://creativecommons.org/licenses/by/4.0/)

public class hanggliderController : MonoBehaviour {
	
	float velocity = 0.15f;
	float gliderAngle = 0.0f;

	protected OVRCameraRig CameraRig = null;

	GameObject glider = null;

	// Use this for initialization
	void Start () {

		// We use OVRCameraRig to set rotations to cameras,
		// and to be influenced by rotation
		OVRCameraRig[] CameraRigs = gameObject.GetComponentsInChildren<OVRCameraRig>();
		
		if(CameraRigs.Length == 0)
			Debug.LogWarning("OVRPlayerController: No OVRCameraRig attached.");
		else if (CameraRigs.Length > 1)
			Debug.LogWarning("OVRPlayerController: More then 1 OVRCameraRig attached.");
		else
			CameraRig = CameraRigs[0];

		// We use the glider tag as a way to find our glider

		glider = GameObject.FindWithTag("Glider");

		gliderAngle = glider.transform.eulerAngles [1] * Mathf.PI / 180.0f;

	}
	
	// Update is called once per frame
	void Update () {

		//----------------------------------------------------------------------
		// set the rotation of the head, based on the tilt
		//----------------------------------------------------------------------
		var hmdTransform = CameraRig.centerEyeAnchor.transform;

		var hmd_rot = hmdTransform.rotation.eulerAngles;

		var camera_rot = transform.eulerAngles;

		if (hmd_rot [2] > 180) {
			hmd_rot [2] = hmd_rot [2] - 360;
		}

		camera_rot [1] -= hmd_rot[2] / 2000 / Mathf.PI * 180;

		gliderAngle -= hmd_rot [2] / 2000;

		transform.eulerAngles = camera_rot;

		//----------------------------------------------------------------------
		// set the rotation of the hangglider
		//----------------------------------------------------------------------
		Vector3 glider_rot = glider.transform.eulerAngles;
		
		glider_rot [1] = gliderAngle / Mathf.PI * 180 ;
		glider_rot [2] = hmd_rot [2] / 2.0f;
		
		glider.transform.eulerAngles = glider_rot;

		//---------------------------------------------------------------------
		// move the glider
		//---------------------------------------------------------------------
		Vector3 pos = transform.position;

		pos [0] += velocity * Mathf.Sin (gliderAngle);
		pos [1] -= velocity / 10.0f;
		pos [2] += velocity * Mathf.Cos (gliderAngle);

		transform.position = pos;
	}
}
