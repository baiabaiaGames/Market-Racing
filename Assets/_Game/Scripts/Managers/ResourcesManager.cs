using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Single Instances/ Resources Manager")]
public class ResourcesManager : ScriptableObject {

    public SingleUse[] all_items;
    Dictionary<string, int> i_dict = new Dictionary<string, int> ();

    public void Init () {
        InitItemsDictionary ();
    }

    void InitItemsDictionary () {
        i_dict.Clear ();
        for (int i = 0; i < all_items.Length; i++) {
            if (i_dict.ContainsKey (all_items[i].name)) {
                Debug.Log (all_items[i].name + " is a duplicate");
            } else {
                i_dict.Add (all_items[i].name, i);
            }
        }
    }

    public SingleUse GetItem (string id) {
        SingleUse retVal = null;
        int index = -1;

        if (i_dict.TryGetValue (id, out index))
            retVal = all_items[index];

        return retVal;
    }

}