using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum DieStatus
{
    Ready,
    StartRoll,
    Rolling,
    Stopped
}


public class DieController : MonoBehaviour
{
    // Keep track of the current side;
    public int CurrentSide { private set; get; } = 0;

    private Vector3 startPosition;

    private Rigidbody rigidBody;

    public DieStatus DieStatus { private set; get; }

    // Assign an outward pointing vector to each side.
    private List<(int, Vector3)> sideList = new List<(int, Vector3)>
    {
        (1, Vector3.down),
        (2, Vector3.forward),
        (3, Vector3.up),
        (4, Vector3.back),
        (5, Vector3.right),
        (6, Vector3.left)
    };

    void Awake()
    {
        this.rigidBody = this.GetComponent<Rigidbody>();
    }

    void Start()
    {
        this.startPosition = this.transform.position;
        this.DieStatus = DieStatus.Ready;
    }

    void Update()
    {
        if (this.DieStatus == DieStatus.StartRoll)
        {
            this.DieStatus = DieStatus.Rolling;
        }
        else if (this.DieStatus == DieStatus.Rolling)
        {
            float maxValue = -1.0f;
            int maxSideIndex = -1;
            for (int i = 0; i < this.sideList.Count; i++)
            {
                // Go through each side and determine which side is pointing up.
                float dotValue = Vector3.Dot(Vector3.up, this.transform.TransformDirection(this.sideList[i].Item2));
                if (dotValue > maxValue)
                {
                    maxValue = dotValue;
                    maxSideIndex = i;
                }
            }
            if (maxSideIndex >= 0)
            {
                this.CurrentSide = this.sideList[maxSideIndex].Item1;
            }
            if (this.IsRolling() == false)
            {
                this.DieStatus = DieStatus.Stopped;
            }
        }
    }

    private bool IsRolling()
    {
        bool rolling = true;
        if (this.rigidBody.velocity.magnitude == 0f)
        {
            if (this.rigidBody.angularVelocity.magnitude == 0f)
            {
                rolling = false;
            }
        }
        return rolling;
    }

    public void Roll()
    {
        this.rigidBody.AddForce(Vector3.up * Random.Range(0.5f, 1.5f), ForceMode.Impulse);
        this.rigidBody.AddTorque(Vector3.left * Random.Range(1f, 50f), ForceMode.Impulse);
        this.rigidBody.AddTorque(Vector3.forward * Random.Range(1f, 50f), ForceMode.Impulse);
        this.rigidBody.AddTorque(Vector3.up * Random.Range(1f, 50f), ForceMode.Impulse);
        this.DieStatus = DieStatus.StartRoll;
    }

    public void Reset()
    {
        this.rigidBody.velocity = new Vector3(0f, 0f, 0f);
        this.rigidBody.angularVelocity = new Vector3(0f, 0f, 0f);
        this.transform.position = this.startPosition;
        this.transform.rotation = Quaternion.identity;
        this.DieStatus = DieStatus.Ready;
    }

}
