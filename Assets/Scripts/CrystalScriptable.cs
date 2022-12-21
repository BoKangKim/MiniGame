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

    public int GetLv() { return lv; }
    public int GetStage() { return stage; }
    public int GetHP() { return stoneHP; }
    public int GetTimer() { return timer; }
    public int GetGoldEarned() { return goldEarned; }
}
