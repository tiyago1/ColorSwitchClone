using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ColorSwitchGame;

public class PlayerController : ColorSwitch
{
    #region Fields

    private Rigidbody2D mRigibody;
    public float mPower;

    private Transform CameraTransform;

    public float mLastPositionY;
    public float mCurrentPositionY = -1;

    private Vector3 mVelocity = Vector3.zero;

    private float t;
    public bool mIsStart;

    private Vector3 targetPosition;

    private float mDefaultTargetPositionY = 2.0f;

    public ColorType Color;

    private Material PlayerMaterial;

    #endregion // Fields

    #region Unity Methods

    public void Start()
    {
        Initialize();
        //    mRigibody.velocity = Vector2.zero;
        //    mRigibody.AddForce(Vector2.up * mPower);
        //    mLastPositionY = mCurrentPositionY;
        //    mCurrentPositionY = this.transform.position.y;
        //    IsFalling = true;
    }

    private bool IsFalling;

    public void Update()
    {
        if (mIsStart)
        {
            t += 0.05f * Time.deltaTime; // 0.1
            Debug.Log(Vector3.Angle(targetPosition, CameraTransform.position));
            if (Vector3.Angle(targetPosition, CameraTransform.position) >= 5f)
            {
                CameraTransform.position = Vector3.MoveTowards(CameraTransform.position, targetPosition, t);
            }

            if (t < 1)
            {
                //CameraTransform.position = Vector3.MoveTowards(CameraTransform.position, targetPosition, 100000000f);
                //CameraTransform.position = new Vector3(0, Mathf.Lerp(CameraTransform.position.y, targetPosition.y, t), -10);
            }
            else
            {
                mIsStart = false;
                t = 0;
            }
        }

        if (IsFalling)
        {
            if (mLastPositionY + 2 <= mCurrentPositionY)
            {
                mLastPositionY = mCurrentPositionY;
                mCurrentPositionY = this.transform.position.y;
                if ((int)mCurrentPositionY - (int)mLastPositionY >= 1.0f)
                {
                    targetPosition = CameraTransform.position + (Vector3.up * mDefaultTargetPositionY);
                    t = 0;
                    mIsStart = true;
                }
            }
            else
            {
                Vector3 viewPortPoint = Camera.main.WorldToViewportPoint(this.transform.position);

                if (viewPortPoint.y >= 0.43f)
                {
                    targetPosition = CameraTransform.position + (Vector3.up * mDefaultTargetPositionY);
                    mIsStart = true;
                    t = 0;
                    IsFalling = false;
                }
                // when it begins to fall
                // Error("MLAS:" + mLastPositionY + " cur  " + mCurrentPositionY);
            }
        }


        if (Input.GetMouseButtonDown(0))
        {
            mRigibody.velocity = Vector2.zero;
            mRigibody.AddForce(Vector2.up * mPower);
            mLastPositionY = mCurrentPositionY;
            mCurrentPositionY = this.transform.position.y;
            IsFalling = true;


        }
    }


    //if (viewPortPoint.y >= 0.5)
    //{

    //    float t = Time.deltaTime;
    //    targetPosition = CameraTransform.position + (Vector3.up * 1.5f);
    //    t = 0;
    //    mIsStart = true;


    //}

    #endregion // Unity Methods

    #region Public Methods

    public void Initialize()
    {
        mRigibody = this.GetComponent<Rigidbody2D>();
        CameraTransform = Camera.main.gameObject.transform;
        PlayerMaterial = this.GetComponent<Material>();
        GameManager.ColorChange += GameManager_ColorChange;

    }

    private void GameManager_ColorChange(ColorType color)
    {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        UnityEngine.Color mColor = new UnityEngine.Color();

        switch (color)
        {
            case ColorType.Yellow:
                mColor = new UnityEngine.Color(0.897f, 0.848f, 0.185f, 1.0f);
                break;
            case ColorType.Purple:
                mColor = new UnityEngine.Color(0.574f, 0.103f, 0.779f, 1.0f);
                break;
            case ColorType.Pink:
                mColor = new UnityEngine.Color(0.991f, 0.338f, 1.0f, 1.0f);
                break;
            case ColorType.Cyan:
                mColor = new UnityEngine.Color(0.0f, 0.710f, 1.0f, 1.0f);
                break;
        }

        renderer.color = mColor;
    }

    #endregion // Public Methods

    #region Private Nethods

    #endregion // Private Methods
}