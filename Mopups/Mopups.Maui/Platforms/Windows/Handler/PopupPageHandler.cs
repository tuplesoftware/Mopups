using Microsoft.Maui.Handlers;
using Microsoft.Maui.Layouts;
using Microsoft.Maui.Platform;

namespace Mopups.Platforms.Windows
{
    public class PopupPageHandler : PageHandler
    {
        public PopupPageHandler()
        {
        }

        protected override void ConnectHandler(ContentPanel platformView)
        {
            if (platformView is PopupPageRenderer popupPageRenderer)
                popupPageRenderer.Handler = this;

            base.ConnectHandler(platformView);

            platformView.SizeChanged += OnPlatformViewSizeChanged;
            platformView.LayoutUpdated += OnPlatformViewLayoutChanged;
        }

        protected override ContentPanel CreatePlatformView()
        {
            return new PopupPageRenderer();
        }

        protected override void DisconnectHandler(ContentPanel platformView)
        {
            platformView.LayoutUpdated -= OnPlatformViewLayoutChanged;
            platformView.SizeChanged -= OnPlatformViewSizeChanged;

            if (platformView is PopupPageRenderer popupPageRenderer)
                popupPageRenderer.Handler = null;

            base.DisconnectHandler(platformView);
        }

        private void OnPlatformViewSizeChanged(object sender, Microsoft.UI.Xaml.SizeChangedEventArgs e)
        {
            VirtualView.ComputeDesiredSize(e.NewSize.Width, e.NewSize.Height);
        }

        private void OnPlatformViewLayoutChanged(object sender, object e)
        {
            // for some reason, this extra call to ComputeDesiredSize, which just does a Measure,
            // is needed to ensure all views are sync'd on size. Solves issue #84.
            VirtualView.ComputeDesiredSize(this.PlatformView.Width, this.PlatformView.Height);
        }
    }
}
