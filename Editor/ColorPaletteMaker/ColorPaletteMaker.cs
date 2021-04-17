using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;


public class ColorPaletteMaker : EditorWindow
{
    [MenuItem("Window/UI Toolkit/ColorPaletteMaker")]
    public static void ShowExample()
    {
        ColorPaletteMaker wnd = GetWindow<ColorPaletteMaker>();
        wnd.titleContent = new GUIContent("ColorPaletteMaker");
    }

    public void CreateGUI()
    {
        var root = rootVisualElement;

        // Import UXML
        var visualTree = AssetDatabase
            .LoadAssetAtPath<VisualTreeAsset>("Assets/_Boilerplate/Editor/ColorPaletteMaker/ColorPaletteMaker.uxml");
        
        root.Add(visualTree.Instantiate());

        // A stylesheet can be added to a VisualElement.
        // The style will be applied to the VisualElement and all of its children.
        var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/_Boilerplate/Editor/ColorPaletteMaker/ColorPaletteMaker.uss");
        VisualElement labelWithStyle = new Label("Hello World! With Style");
        labelWithStyle.styleSheets.Add(styleSheet);
        root.Add(labelWithStyle);
    }
}