using UnityEngine;
using sgffu.EventMessage;
using sgffu.Characters;
using sgffu.Characters.Input;
using UniRx;

/// <summary>
/// The camera added this script will follow the specified object.
/// The camera can be moved by left mouse drag and mouse wheel.
/// </summary>
[DisallowMultipleComponent]
public class FollowingCamera : MonoBehaviour, IControlCharacterAxis
{
    public GameObject target; // an object to follow
    public Vector3 offset = new Vector3(0f, 2.5f, -2f); // offset form the target object

    PlayerInputControlAxis input_controller;

    //[SerializeField] private float distance = 4.0f; // distance from following object
    [SerializeField] private float distance = 2.0f; // distance from following object
    [SerializeField] private float polarAngle = 45.0f; // angle with y-axis
    [SerializeField] private float azimuthalAngle = 45.0f; // angle with x-axis

    [SerializeField] private float minDistance = 1.0f;
    [SerializeField] private float maxDistance = 30.0f;
    [SerializeField] private float minPolarAngle = 5.0f;
    [SerializeField] private float maxPolarAngle = 75.0f;
    [SerializeField] private float mouseXSensitivity = 5.0f;
    [SerializeField] private float mouseYSensitivity = 5.0f;
    [SerializeField] private float scrollSensitivity = 5.0f;

    private float mouse_x = 0f;
    private float mouse_y = 0f;
    private float mouse_wheel = 0f;


    void Start()
    {
        input_controller = new PlayerInputControlAxis(this);

        MessageBroker.Default.Receive<TerrainCreated>().Subscribe(x => {
            this.target = GameObject.FindGameObjectWithTag("Player");

            if (this.target == null) {
                throw new System.Exception("FollowingCamera::Start(): Player GameObject is not found.");
            }

            this.transform.position = this.target.transform.position + new Vector3(0f, 0f, 0f);
            
            var lookAtPos = this.target.transform.position + this.offset;
            this.updatePosition(lookAtPos);
            transform.LookAt(lookAtPos);

            this.enabled = true;
        });

        this.enabled = false;

    }

    void LateUpdate()
    {
        //if (Input.GetMouseButton(0)) {
            //this.updateAngle(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        //}

        //this.updateDistance(Input.GetAxis("Mouse ScrollWheel"));

        this.updateAngle(this.mouse_x, this.mouse_y);
        this.updateDistance(this.mouse_wheel);

        var lookAtPos = this.target.transform.position + this.offset;
        this.updatePosition(lookAtPos);
        transform.LookAt(lookAtPos);
    }

    void updateAngle(float x, float y)
    {
        x = this.azimuthalAngle - x * this.mouseXSensitivity;
        this.azimuthalAngle = Mathf.Repeat(x, 360);

        y = this.polarAngle + y * this.mouseYSensitivity;
        this.polarAngle = Mathf.Clamp(y, this.minPolarAngle, this.maxPolarAngle);
    }

    void updateDistance(float scroll)
    {
        scroll = this.distance - scroll * this.scrollSensitivity;
        this.distance = Mathf.Clamp(scroll, this.minDistance, this.maxDistance);
    }

    void updatePosition(Vector3 lookAtPos)
    {
        var da = this.azimuthalAngle * Mathf.Deg2Rad;
        var dp = this.polarAngle * Mathf.Deg2Rad;
        transform.position = new Vector3(
            lookAtPos.x + this.distance * Mathf.Sin(dp) * Mathf.Cos(da),
            lookAtPos.y + this.distance * Mathf.Cos(dp),
            lookAtPos.z + this.distance * Mathf.Sin(dp) * Mathf.Sin(da));
    }

    public void OnAxis(string axis_name, float value)
    {
        switch(axis_name) {
            case sgffu.Input.Mouse.X: this.mouse_x = value; break;
            case sgffu.Input.Mouse.Y: this.mouse_y = value; break;
            case sgffu.Input.Mouse.Wheel: this.mouse_wheel = value; break;
        }
    }
}