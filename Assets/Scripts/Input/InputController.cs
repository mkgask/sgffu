using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using OwrBase.Input;
using OwrBase.EventMessage;

public class InputController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		OwrBase.Input.Service.init();
	}
	
	// Update is called once per frame
	void Update () {
		OwrBase.Input.Service.inputCheck();
	}
}
