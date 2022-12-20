using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new CrystalData", menuName = "ScriptableObjects/CrystalData")]
public class CrystalScriptable : ScriptableObject
{
    [SerializeField] private int lv;
    [SerializeField] private int stage;
    [SerializeField] private int stoneHP;
    [SerializeField] private int timer;
    [SerializeField] private int goldEarned;
}
