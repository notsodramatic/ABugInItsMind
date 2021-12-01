using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Memo Text", menuName ="Memo/New Memo Text")]
public class MemoTextSO : ScriptableObject
{
    public string[] memoTextLines;
}
