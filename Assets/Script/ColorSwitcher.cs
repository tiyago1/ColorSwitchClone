using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ColorSwitchGame;

public class ColorSwitcher : ColorSwitch
{
    public void OnTriggerEnter2D(Collider2D collider)
    {
        GameManager.OnColorChange(ColorType.MaxColorTypeCount);
        this.gameObject.SetActive(false);
    }
}