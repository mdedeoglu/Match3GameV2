using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Drop/DropType")]
public class DropTypeSO : ScriptableObject
{
    public string dropName;
    public GameObject dropPrefab;

}
