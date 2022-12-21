using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new PickData", menuName = "ScriptableObjects/PickData")]
public class PickScriptable : ScriptableObject
{
    [SerializeField] private string objName;
    [SerializeField] private int touchDamage;
    [SerializeField] private int gold;
}
