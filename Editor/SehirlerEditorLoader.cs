using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class SehirlerEditorLoader : EditorWindow
{
    [MenuItem("Tools/Sehirleri Yükle Panelde Göster")]
    public static void SehirleriYukleMenu()
    {
        // Sahnedeki ilk Canvas içindeki ilk Panel'i bul
        Canvas canvas = GameObject.FindObjectOfType<Canvas>();
        if (canvas == null)
        {
            EditorUtility.DisplayDialog("Canvas Bulunamadý", "Sahnede bir Canvas bulunamadý.", "Tamam");
            return;
        }
        Transform panel = canvas.transform.Find("Panel");
        if (panel == null)
        {
            EditorUtility.DisplayDialog("Panel Bulunamadý", "Canvas altýnda 'Panel' adýnda bir GameObject bulunamadý.", "Tamam");
            return;
        }

        // Paneli temizle
        for (int i = panel.childCount - 1; i >= 0; i--)
        {
            GameObject.DestroyImmediate(panel.GetChild(i).gameObject);
        }

        // 81 PNG'yi sýrayla yükle ve Image oluþtur
        for (int i = 1; i <= 81; i++)
        {
            string spritePath = $"Sehirler/{i}";
            Sprite sprite = Resources.Load<Sprite>(spritePath);
            if (sprite == null)
            {
                Debug.LogWarning($"{spritePath}.png bulunamadý veya Resources'a eklenmedi.");
                continue;
            }

            // Yeni GameObject oluþtur
            GameObject imgObj = new GameObject($"SehirImage_{i}", typeof(RectTransform), typeof(CanvasRenderer), typeof(Image));
            imgObj.transform.SetParent(panel, false);

            // Image componentini ayarla
            Image img = imgObj.GetComponent<Image>();
            img.sprite = sprite;

            // Boyutlarý sprite'ýn 10'da biri yap
            RectTransform rt = imgObj.GetComponent<RectTransform>();
            rt.sizeDelta = new Vector2(sprite.rect.width / 10f, sprite.rect.height / 10f);
        }
    }

    [MenuItem("Tools/Paneli Temizle")] // Yeni MenuItem
    public static void PaneliTemizleMenu()
    {
        Canvas canvas = GameObject.FindObjectOfType<Canvas>();
        if (canvas == null)
        {
            EditorUtility.DisplayDialog("Canvas Bulunamadý", "Sahnede bir Canvas bulunamadý.", "Tamam");
            return;
        }
        Transform panel = canvas.transform.Find("Panel");
        if (panel == null)
        {
            EditorUtility.DisplayDialog("Panel Bulunamadý", "Canvas altýnda 'Panel' adýnda bir GameObject bulunamadý.", "Tamam");
            return;
        }
        for (int i = panel.childCount - 1; i >= 0; i--)
        {
            GameObject.DestroyImmediate(panel.GetChild(i).gameObject);
        }
    }
}
