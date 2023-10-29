using System;
using System.Collections.Generic;
using System.Linq;
using Constants;
using Core.WindowSystem.Blockers;
using Core.WindowSystem.Layers;
using Core.WindowSystem.MVP;
using GP.Framework.WindowSystem;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Layer = Core.WindowSystem.Layers.Layer;
using Object = UnityEngine.Object;

namespace Core.WindowSystem
{
    public class WindowManager : IWindowManager, IWindowRegistration, IDisposable
    {
        public event Action<IPresenter> WindowOpenedEvent;
        public event Action<IPresenter> WindowClosedEvent;
        public event Action<IPresenter> ActiveWindowChangedEvent;
        public event Action AllWindowsClosedEvent;

        private readonly Dictionary<Type, Action<Transform, Action<IWindowView, IWindowModel>>> _viewModelCreators;
        private readonly Dictionary<Layer, IWindowOpenInfo> _lastActiveWindows;

        private readonly Dictionary<Type, Layer> _presentersLayers = new();
        private readonly Dictionary<IWindowData, IWindowOpenInfo> _windowsData = new();
        private readonly Dictionary<IPresenter, IWindowOpenInfo> _windowsOpenData = new();
        
        private readonly List<Layer> _windowLayers = new List<Layer>();

        private readonly IBlocker _backgroundBlocker;
        private readonly IWindowManagerFactory _windowManagerFactory;
        
        private Canvas _mainCanvas;

        public Canvas MainCanvas
        {
            get
            {
                if (_mainCanvas == null)
                {
                    _mainCanvas = _windowManagerFactory.GetMainCanvas();
                }

                return _mainCanvas;
            }
        }

        public WindowManager(IWindowManagerFactory windowManagerFactory, IWindowSystemSettings windowSystemSettings)
        {
            _mainCanvas = windowManagerFactory.GetMainCanvas();
            _backgroundBlocker = windowManagerFactory.GetBackgroundBlocker();
            _backgroundBlocker.SetActive(false);
            _windowManagerFactory = windowManagerFactory;
            _viewModelCreators = new Dictionary<Type, Action<Transform, Action<IWindowView, IWindowModel>>>();
            _lastActiveWindows = new Dictionary<Layer, IWindowOpenInfo>();

            List<ViewLayerSettings> layersSettings = windowSystemSettings.LayersSettings;
            int currentLayerOrder = 0;

            foreach (var layerSettings in layersSettings)
            {
                var layer = new Layer(layerSettings);

                _windowLayers.Add(layer);

                layer.Order = currentLayerOrder++;
            }
            
            //
            // foreach (var settings in layersSettings)
            // {
            //     var layer = new Layer(settings);
            //     _presentersLayers.Add(layer.GetType(), layer);
            // }
        }

        public void Dispose()
        {
            if (_mainCanvas != null)
            {
                Object.Destroy(_mainCanvas.gameObject);
            }
        }

        public void Close(IPresenter windowPresenter, bool executeImmediately = false)
        {
            if (windowPresenter.State == ViewState.Close)
            {
                return;
            }
            if (_windowsOpenData.TryGetValue(windowPresenter, out IWindowOpenInfo windowOpenInfo))
            {
                Debug.Log($"Try Close ({windowPresenter.GetType()}) with Data {windowOpenInfo.Data}");
            }
            
            if (_windowsData.TryGetValue(windowOpenInfo.Data, out var info))
            {
                var window = info.Presenter;
                if (window != null && _windowsOpenData.ContainsKey(window))
                {
                    _windowsOpenData.Remove(info.Presenter);
                    var manipulation = (IWindowManipulation)window;
                    manipulation.Close();
                }

                _windowsData.Remove(windowOpenInfo.Data);
            }
        }

        public void ClearCanvas()
        {
            foreach (var openWindow in _windowsOpenData)
            {
                openWindow.Value.WindowManipulation.Close();
            }
            AllWindowsClosedEvent?.Invoke();
        }

        public IWindowOpenInfo GetActiveWindow(Layer layer)
        {
            _lastActiveWindows.TryGetValue(layer, out IWindowOpenInfo windowOpenInfo);
            return windowOpenInfo;
        }

        public IWindowOpenInfo GetActiveWindow(string layerName)
        {
            foreach (var layer in _lastActiveWindows)
            {
                if (layerName == layer.Key.Name)
                {
                    return layer.Value;
                }
            }

            return null;
        }

        public bool IsOpened<TPresenter>() where TPresenter : class, IPresenter, new()
        {
            var controllerType = typeof(TPresenter);
            foreach (var windowOpenPair in _windowsOpenData)
            {
                if (windowOpenPair.Key.GetType() == controllerType && windowOpenPair.Key.IsOpen)
                {
                    return true;
                }
            }

            return false;
        }

        public void Register<T>(Action<Transform, Action<IWindowView, IWindowModel>> createMethod)
            where T : IPresenter
        {
            var type = typeof(T);

            var layerAttribute =
                (ViewLayerAttribute) Attribute.GetCustomAttribute(type, typeof(ViewLayerAttribute));
            Debug.Log($"Register ({type}) on layer ({layerAttribute?.Name})");

            if (layerAttribute != null)
            {
                Layer layer = _windowLayers.FirstOrDefault(x => x.Name == layerAttribute.Name);
                _presentersLayers[type] = layer;
            }

            _viewModelCreators[type] = createMethod;
        }

        public void SetCanvasConfig(WindowSystemConfig config)
        {
            CanvasScaler canvasScaler = MainCanvas.GetComponent<CanvasScaler>();
            canvasScaler.matchWidthOrHeight = config.CanvasScaler;
            canvasScaler.screenMatchMode = config.ScreenMatchMode;
            if (config.ReferenceResolution.HasValue)
            {
                canvasScaler.referenceResolution = config.ReferenceResolution.Value;
            }

            Camera camera = _mainCanvas.worldCamera;
            camera.depth = config.Depth;

            _backgroundBlocker.ApplyConfig(config.BlockerConfig);
        }

        public Layer GetLayer(string layerName)
        {
            foreach (var layer in _windowLayers)
            {
                if (layer.Name == layerName)
                {
                    return layer;
                }
            }

            return null;
        }

        internal string GetPresenterName(IWindowData data)
        {
            if (_windowsData.TryGetValue(data, out var info))
            {
                return info.Presenter?.GetType().Name;
            }

            return null;
        }

        public TPresenter Open<TPresenter>(IWindowParameters parameters, bool enableBlocker)
            where TPresenter : class, IPresenter
        {
            _presentersLayers.TryGetValue(typeof(TPresenter), out Layer layer);

            if (layer != null)
            {
                IWindowOpenInfo windowOpenInfo = CreateWindow<TPresenter>(parameters, enableBlocker);

                _lastActiveWindows.TryGetValue(windowOpenInfo.Data.Layer, out IWindowOpenInfo lastActiveWindow);
                
                if (windowOpenInfo.Data.Layer == layer && lastActiveWindow == null)
                {
                    _lastActiveWindows.Add(windowOpenInfo.Data.Layer, windowOpenInfo);
                }
                Debug.Log($"Try Open<{typeof(TPresenter)}> with Data {windowOpenInfo.Data}");

                return (TPresenter)windowOpenInfo.Presenter;
            }

            Debug.Log($"Couldn't find the layer for presenter ({typeof(TPresenter)})");
                
            return null;
        }

        private IWindowOpenInfo CreateWindow<TPresenter>(IWindowParameters parameters, bool enableBlocker)
            where TPresenter : class, IPresenter
        {
            var (view, model) = CreateViewModel<TPresenter>(MainCanvas.transform);

            IPresenter window = _windowManagerFactory.CreateWindowPresenter<TPresenter>(view, model, parameters);
            
            _presentersLayers.TryGetValue(typeof(TPresenter), out Layer layer);

            var openInfo = new WindowOpenInfo(layer)
            {
                Presenter = window,
                Parameters = parameters,
                EnableBlocker = enableBlocker
            };
            
            window.Layer = layer;
            
            Debug.Log($"Created presenter {typeof(TPresenter)} on layer: {window.Layer}");

            _windowsData[openInfo.Data] = openInfo;
            _windowsOpenData[window] = openInfo;

            return openInfo;
        }

        private (IWindowView, IWindowModel) CreateViewModel<TPresenter>(Transform parent)
        {
            IWindowView windowView = null;
            IWindowModel windowModel = null;
            _viewModelCreators[typeof(TPresenter)].Invoke(parent, (view, model) =>
            {
                if (view == null)
                {
                    Debug.LogError($"Couldn't create View: {typeof(TPresenter)}");
                    return;
                }

                if (model == null)
                {
                    Debug.LogError($"Couldn't create Model: {typeof(TPresenter)}");
                }

                windowView = view;
                windowModel = model;
                
                if (windowModel?.State.Value == ViewState.None)
                {
                    view.Disable();
                }
            });

            return (windowView, windowModel);
        }

        private void OnActiveWindowChanged(Layer layer)
        {
            ActiveWindowChangedEvent?.Invoke(GetActiveWindow(layer)?.Presenter);
        }

        private void ActiveBlocker(IWindowOpenInfo info)
        {
            string layer = info?.Presenter.Layer.Name ?? WindowsLayer.Default;

            if (info == null || layer == WindowsLayer.Screen || layer == WindowsLayer.Default || !info.EnableBlocker)
            {
                try
                {
                    _backgroundBlocker.SetActive(false);
                }
                catch (Exception exception)
                {
                    Debug.LogError($"Window Manager: Something went wrong with window system: {exception}");
                }

                return;
            }

            _backgroundBlocker.SetActive(true, layer);
        }
    }
}