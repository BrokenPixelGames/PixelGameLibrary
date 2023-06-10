using System.IO;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Runtime.Setting
{
    
    public static class GameSettings
    {
        private const string SettingPath = "Resources/Data";
        private static List<GameSettingBase> _settings = new();
        
        public static T Get<T>() where T : GameSettingBase
        {
            string filename = typeof(T).ToString().Split('.').LastOrDefault();
            string p = Path.Combine(SettingPath, filename);

            T setting = (T)_settings.FirstOrDefault(x => x.GetType() == typeof(T));
            if (setting == null)
            {
                setting = Resources.Load<T>(p);
                if (setting == null)
                {
                    setting = ScriptableObject.CreateInstance<T>();
#if UNITY_EDITOR
                    string d = Path.Combine(Application.dataPath, SettingPath);
                    if (!Directory.Exists(d))
                    {
                        Directory.CreateDirectory(d);
                    }
                    
                    AssetDatabase.CreateAsset(setting, $"Assets/{p}.asset");
                    AssetDatabase.SaveAssets();
                    AssetDatabase.Refresh();
#endif
                }
                
                _settings.Add(setting);
            }

            return setting;
        }
    }
}