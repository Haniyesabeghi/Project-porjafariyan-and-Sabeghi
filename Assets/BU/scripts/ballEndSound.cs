﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballEndSound : MonoBehaviour {

	public AudioSource ballEndAudio;



	void OnTriggerEnter(Collider collider){
		if (collider.gameObject.name == "Ball") {
			ballEndAudio.Play ();

		}
	}

}
