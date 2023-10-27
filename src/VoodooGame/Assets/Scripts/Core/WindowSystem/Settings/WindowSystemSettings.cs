using System.Collections.Generic;
using Core.WindowSystem.Layers;
using GP.Framework.WindowSystem;
using UnityEditor;
using UnityEngine;

namespace Core.WindowSystem.Settings
{
    [CreateAssetMenu(fileName = "WindowSystemSettings", menuName = "Configurations/Window System/Settings")]
    public class WindowSystemSettings : ScriptableObject, IWindowSystemSettings
    {
        public const string Path = "Assets/Framework/Resources/Settings/GP Window System/WindowSystemSettings.asset";

        [SerializeField] 
        private List<ViewLayerSettings> _layersSettings;

        public List<ViewLayerSettings> LayersSettings => _layersSettings;

        private static WindowSystemSettings GetSettings()
        {
            var settings = Resources.Load<WindowSystemSettings>("Settings/GP Window System/WindowSystemSettings");
            if (settings != null)
            {
                return settings;
            }

            return null;
        }

#if UNITY_EDITOR
        public static SerializedObject GetSerializedSettings()
        {
            var settings = GetSettings();
            if (settings == null)
            {
                return null;
            }

            return new SerializedObject(settings);
        }
#endif
    }
}