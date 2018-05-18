using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenuAttribute (fileName = "Menu", menuName = "Menu")]
public class StandarMenu : ScriptableObject {

	public Sprite backGround;
	public MenuData[] menuData;
}

[System.Serializable]
public class MenuData {
	public string textEnglish;
	public string textSpanish;
}