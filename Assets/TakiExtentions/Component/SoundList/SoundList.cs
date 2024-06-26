using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Taki.SoundManager
{
    [CreateAssetMenu(menuName = "CreateScriptableObject/SoundList",fileName = "SoundList")]
    public partial class SoundList : ScriptableObject
    {
        
        [Header("音声を入れる場所")]
        [Header("音声を入れるとコードが動的生成されますが、コンパイルはされないので後で手動コンパイルの必要があります")]
        [SerializeField] private List<AudioInformation> audios;

#if UNITY_EDITOR
        /// <summary>
        /// サウンド用のパーシャルを動的生成する。UnityEditor専用
        /// </summary>
        [ContextMenu("UpdateSoundListScripts")]
        public void UpdateSoundListScripts()
        {
            Debug.Log("実行しました。");

            var mono = MonoScript.FromScriptableObject(this);
            string path = AssetDatabase.GetAssetPath( mono );
            string directoryName = Path.GetDirectoryName(path); 
            string classNamePath = Path.GetFileNameWithoutExtension(path);
            string combinePath = Path.Combine(directoryName, classNamePath + "Dynamic.cs");

            // とくていのファイルを上書きする。
            using (StreamWriter writer = new StreamWriter(combinePath, false, Encoding.Unicode))
            {
                writer.Write(CreateScripts());
                Debug.Log(CreateScripts());
                Debug.Log("音声の一覧スクリプトを更新しました");

            } 
        }
        
        public string CreateScripts()
        {
            StringBuilder sb = new StringBuilder();
            string soundKindEnumName ="SoundKind";
            string soundSwitchVariableName = "kind";

            sb.AppendLine("// このファイルは自動生成されたものです");
            sb.AppendLine("using UnityEngine;");
            sb.AppendLine($"namespace {GetType().Namespace}");
            sb.AppendLine("{");
            sb.AppendLine($"    public partial class {GetType().Name} : ScriptableObject");
            sb.AppendLine("    {");
            sb.AppendLine($"        public {nameof(AudioInformation)} GetSoundClip({soundKindEnumName} {soundSwitchVariableName})");
            sb.AppendLine("        {");
            sb.AppendLine($"            switch({soundSwitchVariableName})");
            sb.AppendLine("            {");

            for (int i = 0; i < audios.Count; i++)
            {
                sb.AppendLine($"            case {soundKindEnumName}.{audios[i].audio.name}:");
                sb.AppendLine($"                return {nameof(audios)}[{i}];");
            }
            sb.AppendLine("            default:");
            sb.AppendLine("                return default;");
            sb.AppendLine("            }");
            sb.AppendLine("        }");
            sb.AppendLine("     }");
            sb.AppendLine($"    public enum {soundKindEnumName}");
            sb.AppendLine("    {");
            for (int i = 0; i < audios.Count; i++)
            {
                sb.AppendLine($"        {audios[i].audio.name},");
            }
            sb.AppendLine("    }");
            sb.AppendLine("}");

            return sb.ToString();
        }

        // 音声ファイルに更新が入ったらファイルを更新する
        private void OnValidate()
        {
            UpdateSoundListScripts();
        }
#endif
    }
    
    
}
