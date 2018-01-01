using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ColorSwitchGame;
using System;

public class Shape : ColorSwitch 
{
    #region Fields

    public ColorType Color;
    private ShapeController mController;

    #endregion // Fields

    #region Unity Methods

    public void Start()
    {

    }

    public void OnTriggerCollider2D(Collider2D collider)
    {

    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        mController.OnCollisionDetect();
    }

    #endregion // Unity Methods
    
    #region Public Methods
    
    public void Initialize(ShapeController controller, ColorType color)
    {
        Color = color;
        mController = controller;
        SetColor(Color);
    }

    public void SetTrigger(bool value)
    {
        if (this.GetComponent<PolygonCollider2D>() != null)
            this.GetComponent<PolygonCollider2D>().isTrigger = value;
        else if(this.GetComponent<BoxCollider2D>() != null)
            this.GetComponent<BoxCollider2D>().isTrigger = value;

    }

    #endregion // Public Methods
    
    #region Private Nethods
    
    private void SetColor(ColorType type)
    {
        UnityEngine.Color color = new UnityEngine.Color();

        switch (type)
        {
            case ColorType.Yellow:
                color = new UnityEngine.Color(0.897f, 0.848f, 0.185f, 1.0f);
                break;
            case ColorType.Purple:
                color = new UnityEngine.Color(0.574f, 0.103f, 0.779f, 1.0f);
                break;
            case ColorType.Pink:
                color = new UnityEngine.Color(0.991f, 0.338f, 1.0f, 1.0f);
                break;
            case ColorType.Cyan:
                color = new UnityEngine.Color(0.0f, 0.710f, 1.0f, 1.0f);
                break;
        }

        this.GetComponent<SpriteRenderer>().color = color;
    }

    #endregion // Private Methods
}