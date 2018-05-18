using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LenguageManager : MonoBehaviour {
    [SerializeField] private StandarMenu menu;
    [SerializeField] private TextMenu[] textMenu;
    [SerializeField] private Dropdown lenguage;

    [SerializeField] private Image backGround;

    [System.Serializable]
    public class TextMenu {
        public string name;
        public TMPro.TextMeshProUGUI text;
    }

    private void Awake () {
        backGround.sprite = menu.backGround;
        for (int i = 0; i < menu.menuData.Length; i++) {
            if (menu.menuData[i].textEnglish != null)
                textMenu[i].text.SetText (menu.menuData[i].textEnglish);
        }
    }

    public void Lenguage () {

        switch (lenguage.value) {
            case 0:
                for (int i = 0; i < menu.menuData.Length; i++) {
                    if (menu.menuData[i].textEnglish != null)
                        textMenu[i].text.SetText (menu.menuData[i].textEnglish);
                }
                Debug.Log ("Ingles");
                break;

            case 1:
                for (int i = 0; i < menu.menuData.Length; i++) {
                    if (menu.menuData[i].textSpanish != null)
                        textMenu[i].text.SetText (menu.menuData[i].textSpanish);
                }
                Debug.Log ("Español");
                break;
        }
    }
}