using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ColorSwitchGame;

public class Point : ColorSwitch
{
    public void OnTriggerEnter2D(Collider2D collider)
    {
        GameManager.OnCoinChange();
        this.gameObject.SetActive(false);
    }
}