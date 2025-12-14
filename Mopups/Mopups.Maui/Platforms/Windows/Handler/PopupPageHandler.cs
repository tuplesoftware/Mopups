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

            PlatformView.SizeChanged += OnPlatformViewSizeChanged;
        }

        protected override ContentPanel CreatePlatformView()
        {
            return new PopupPageRenderer();
        }

        protected override void DisconnectHandler(ContentPanel platformView)
        {
            platformView.SizeChanged -= OnPlatformViewSizeChanged;

            if (platformView is PopupPageRenderer popupPageRenderer)
                popupPageRenderer.Handler = null;

            base.DisconnectHandler(platformView);
        }

        private void OnPlatformViewSizeChanged(object sender, Microsoft.UI.Xaml.SizeChangedEventArgs e)
        {
            VirtualView.ComputeDesiredSize(e.NewSize.Width, e.NewSize.Height);
        }
    }
}
