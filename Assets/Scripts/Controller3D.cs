using UnityEngine;
using System.Collections;

public class Controller3D : MonoBehaviour {

    [SerializeField, Range(0, 50)]
    float maxWalk;

    [SerializeField, Range(0, 360)]
    float maxRotation;

    [SerializeField]
    float walkForce;

    [SerializeField]
    float rotationForce;

    [SerializeField]
    float smallInputThreshold = 0.03f;

    [SerializeField, Range(0, 1)]
    float velocityDecay = 0.1f;

    [SerializeField, Range(1, 10)]
    float velocityDecay2 = 5f;

    Rigidbody rb;

    bool _allowWalk = true;
    [SerializeField]

    bool applyRotationToTransform = true;

    public bool allowWalk
    {
        get
        {
            return _allowWalk;
        }

        set
        {
            _allowWalk = value;
            if (!value)
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
        }
    }
    void Start()
    {
        rb = GetComponentInChildren<Rigidbody>();
    }

	void Update () {
	    if (_allowWalk)
        {
            var walkForceMoment = Input.GetAxis("Vertical") * walkForce * Time.deltaTime;
            var rotationForceMoment = Input.GetAxis("Horizontal") * rotationForce * Time.deltaTime;
            var walking = Mathf.Abs(walkForceMoment) > smallInputThreshold;
            var rotating = Mathf.Abs(rotationForceMoment) > smallInputThreshold;

            if (walking) {
                rb.AddForce(walkForceMoment * rb.transform.forward);
                rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxWalk);
            } else
            {
                rb.velocity = rb.velocity * velocityDecay / velocityDecay2;
            }

            if (rotating)
            {
                if (applyRotationToTransform)
                {
                    rb.transform.Rotate(Vector3.up, rotationForceMoment,Space.Self);
                }
                else
                {
                    rb.velocity = rb.velocity * velocityDecay;
                    rb.AddTorque(rotationForceMoment * rb.transform.up);
                    rb.angularVelocity = Vector3.ClampMagnitude(rb.angularVelocity, maxRotation);
                }
            }
            else
            {
                if (!applyRotationToTransform)
                    rb.angularVelocity = Vector3.zero;
            }
        }
	}

}
