using System;
using System.Collections.Generic;
using System.IO;
using Game.UIManage;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ComponentAutoBindTool
{
    // 脚本生成的目录，根据自身要求更改
    private const string scriptsPath = "Assets/Scripts/UIManage/AutoBind/";

    // C#文件模板，根据自身要求更改
    private const string Template =
        "namespace Game.UIManage\n" +
        "{\n" +
        "    public partial class ##NAME## : UIFormLogic\n" +
        "    {\n" +
        "//##INSERTPROPERTY##\n" +
        "        protected override void AutoBind()\n" +
        "        {\n" +
        "//##INSERTFUNCTION##\n" +
        "        }\n" +
        "    }\n" +
        "}\n";

    [MenuItem("CONTEXT/Component/AutoBind", false, 0)]
    public static void Bind(MenuCommand command)
    {
        var component = (Component)command.context;


        Type managerType = typeof(UIFormLogic); // UIFormLogic:UI管理的父类，根据自身要求更改
        var manager = component.transform.GetComponentInParent(managerType);
        if (manager == null)
        {
            Debug.LogError("Not Find UIManager");
            return;
        }
        var content = manager.transform.Find("Content");// 根节点，根据自身要求更改


        AutoBind(component, content, manager.GetType().Name);
    }

    // 根据不同的类型为属性添加不同的后缀，根据自身要求更改
    private static Dictionary<Type, string> TypeSuffix = new Dictionary<Type, string>()
    {
        { typeof(Image),"_Img"},
        { typeof(Button),"_Btn"},
        { typeof(Toggle),"_Tog"},
        { typeof(TMPro.TextMeshProUGUI),"_Txt"},
        { typeof(Text),"_Txt"},
        { typeof(TMPro.TMP_InputField),"_IF"},
        { typeof(InputField),"_IF"},
        { typeof(Slider),"_SL"},
    };

    private static void AutoBind(Component component, Transform root, string className)
    {
        string path = scriptsPath + className + ".AutoBind.cs";

        string Text = File.Exists(path) ? File.ReadAllText(path) : Template.Replace("##NAME##", className);

        string name = component.name;
        if (TypeSuffix.TryGetValue(component.GetType(), out string suffix))
        {
            name += suffix;
        }
        string property = $"        private {component.GetType().FullName} {name};\n//##INSERTPROPERTY##\n";
        Text = Text.Replace("//##INSERTPROPERTY##\n", property);

        var str = GetPath(component.transform, root, Text, out var title);
        string function = $"            {name} = {title}.Find(\"{str}\").GetComponent<{component.GetType().FullName}>();\n//##INSERTFUNCTION##\n";
        Text = Text.Replace("//##INSERTFUNCTION##\n", function);

        StreamWriter sw = new StreamWriter(path);
        sw.Write(Text);
        sw.Dispose();
        sw.Close();
    }

    private static string GetPath(Transform selection, Transform parent, string text, out string title)
    {
        string result = selection.name;
        for (int i = 0; i < 100; i++)
        {
            selection = selection.parent;
            if (selection == null)
            {
                title = string.Empty;
                return string.Empty;
            }
            if (selection == parent)
            {
                title = parent.name;
                return result;
            }
            if(text.Contains($" {selection.name};"))
            {
                title = $"{selection.name}.transform";
                return result;
            }
            foreach (var item in TypeSuffix)
            {
                string name = $" {selection.name}{item.Value};";
                if (text.Contains(name))
                {
                    title = $"{selection.name}{item.Value}.transform";
                    return result;
                }
            }

            result = selection.name + "/" + result;
        }
        title = parent.name;
        return result;
    }
}
