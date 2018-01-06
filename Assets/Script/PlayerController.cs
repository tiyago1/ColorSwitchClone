using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ColorSwitchGame;

public class PlayerController : ColorSwitch
{
    #region Constants

    private const float POWER = 210;

    #endregion // Constants

    #region Fields

    private Rigidbody2D mRigibody;
    public ColorType Color;
    private Material PlayerMaterial;

    #endregion // Fields

    #region Unity Methods

    public void Start()
    {
        Initialize();
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mRigibody.velocity = Vector2.zero;
            mRigibody.AddForce(Vector2.up * POWER);
        }
    }

    #endregion // Unity Methods

    #region Public Methods

    public void Initialize()
    {
        mRigibody = this.GetComponent<Rigidbody2D>();
        PlayerMaterial = this.GetComponent<Material>();
        GameManager.ColorChange += GameManager_ColorChange;
    }

    #endregion // Public Methods

    #region Private Nethods
    private void GameManager_ColorChange(ColorType color)
    {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        Color mColor = new Color();

        switch (color)
        {
            case ColorType.Yellow:
                mColor = new Color(0.897f, 0.848f, 0.185f, 1.0f);
                break;
            case ColorType.Purple:
                mColor = new Color(0.574f, 0.103f, 0.779f, 1.0f);
                break;
            case ColorType.Pink:
                mColor = new Color(0.991f, 0.338f, 1.0f, 1.0f);
                break;
            case ColorType.Cyan:
                mColor = new Color(0.0f, 0.710f, 1.0f, 1.0f);
                break;
        }

        renderer.color = mColor;
    }

    #endregion // Private Methods
}