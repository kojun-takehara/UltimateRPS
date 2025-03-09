using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
 
public class SelectChildren
{
  // 選択した要素の直接の子要素全てを選択
  [MenuItem("MyTools/Select Direct Children")]
  public static void SelectDirectChildren()
  {
    var newSelection = new List<Object>();
    foreach (Transform t in Selection.transforms)
    {
      for (int i = 0; i < t.childCount; i++)
      {
        newSelection.Add(t.GetChild(i).gameObject);
      }
    }
    Selection.objects = newSelection.ToArray();
  }
}