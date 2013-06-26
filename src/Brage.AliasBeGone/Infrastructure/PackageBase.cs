using System;
using Microsoft.VisualStudio.Editor;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.TextManager.Interop;

namespace Brage.AliasBeGone.Infrastructure
{
    internal abstract class PackageBase : Package
    {
        private const Int32 MUST_HAVE_FOCUS = 1;

        protected IVsUserData GetUserData()
        {
            var textManager = GetService<SVsTextManager, IVsTextManager>();
            IVsTextView activeView;
            textManager.GetActiveView(MUST_HAVE_FOCUS, null, out activeView);
            
            return activeView as IVsUserData;
        }

        protected ITextView GetTextView(IVsUserData userData)
        {
            var guidViewHost = DefGuidList.guidIWpfTextViewHost;

            Object holder;
            userData.GetData(ref guidViewHost, out holder);

            return ((IWpfTextViewHost)holder).TextView;
        }

        protected TService GetService<TService>()
        {
            return (TService)GetService(typeof(TService));
        }

        protected TCast GetService<TService, TCast>()
        {
            return (TCast)GetService(typeof(TService));
        }
    }
}
