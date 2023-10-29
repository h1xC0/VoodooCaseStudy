using Constants;
using Core.WindowSystem.Blockers;
using Core.WindowSystem.Extensions;
using Core.WindowSystem.MVP;
using Core.WindowSystem.Settings;
using Factories;
using GP.Framework.WindowSystem;
using Services.ResourceProvider;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Core.WindowSystem
{
    public class WindowManagerFactory : AbstractFactory, IWindowManagerFactory
    {
        private const string WindowsManagerPath = "Prefabs/Windows/MainCanvas";
        private const string BackgroundBlockerPath = "Prefabs/Windows/BackgroundBlocker";
        private const string ForegroundBlockerPath = "Prefabs/Windows/ForegroundBlocker";
        
        private Canvas _mainCanvas;
        private RectTransform _mainCanvasRt;
        private Blocker _backgroundBlocker;
        private GameObject _foregroundBlocker;

        public WindowManagerFactory(DiContainer diContainer) : base(diContainer)
        {
        }

        public Canvas GetMainCanvas()
        {
            if (_mainCanvas == null)
            {
                _mainCanvas = CreateObject<Canvas>(WindowsManagerPath);
            }

            return _mainCanvas;
        }

        public IBlocker GetBackgroundBlocker()
        {
            if (_backgroundBlocker == null)
            {
                _backgroundBlocker = CreateObject<Blocker>(BackgroundBlockerPath, GetMainCanvas().transform);
            }

            return _backgroundBlocker;
        }

        public GameObject GetForegroundBlocker()
        {
            if (_foregroundBlocker == null)
            {
                var blockerCanvas = CreateObject<Canvas>(ForegroundBlockerPath, GetMainCanvas().transform);
                blockerCanvas.sortingLayerName = "Foreground";
                _foregroundBlocker = blockerCanvas.gameObject;
            }

            return _foregroundBlocker;
        }

        public TPresenter CreateWindowPresenter<TPresenter>(IWindowView windowView, IWindowModel windowModel, IWindowParameters parameters)
            where TPresenter : class, IPresenter
        {
            var presenter = DiContainer.Instantiate<TPresenter>(new object[]{windowView, windowModel});
            return presenter;
        }

        public Button CreateOutsideAreaButton(IWindowView windowView)
        {
            var outsideAreaGo = new GameObject("OutsideAreaButton");

            var mainCanvas = GetMainCanvas();
            if (!_mainCanvasRt)
            {
                _mainCanvasRt = mainCanvas.GetComponent<RectTransform>();
            }

            outsideAreaGo.transform.SetParent(mainCanvas.transform, false);
            var rt = outsideAreaGo.AddComponent<RectTransform>();

            // fit to canvas size
            rt.anchoredPosition = Vector2.zero;
            rt.anchorMin = new Vector2(0.5f, 0.5f);
            rt.anchorMax = new Vector2(0.5f, 0.5f);
            var mainCanvasSizeDelta = _mainCanvasRt.sizeDelta;
            rt.sizeDelta = new Vector2(mainCanvasSizeDelta.x, mainCanvasSizeDelta.y);
            outsideAreaGo.transform.SetParent(((MonoBehaviour)windowView).transform, false);
            outsideAreaGo.transform.SetSiblingIndex(0);

            outsideAreaGo.AddComponent<CanvasRenderer>();
            outsideAreaGo.AddComponent<NonDrawingGraphic>();
            var outsideAreaButton = outsideAreaGo.AddComponent<Button>();
            outsideAreaButton.transition = Selectable.Transition.None;

            return outsideAreaButton;
        }
    }
}