using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using sgffu.Input;
using sgffu.EventMessage;

public class InputController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		sgffu.Input.Service.init();
	}
	
	// Update is called once per frame
	void Update () {
		sgffu.Input.Service.inputCheck();
	}
}
