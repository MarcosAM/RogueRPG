using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipDatabase : MonoBehaviour
{
    [SerializeField] Equip[] equips;
    //[SerializeField] int[] amount;

    static EquipDatabase instace;

    private void Awake()
    {
        instace = this;
        DontDestroyOnLoad(gameObject);
    }

    public static Equip GetEquip(int equipIndex) { return instace.equips[equipIndex]; }
    public static Equip[] GetAllEquips() { return instace.equips; }
    //public static int[] GetAmount() { return instace.amount; }
}