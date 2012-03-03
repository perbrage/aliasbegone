using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.ComponentModel.Design;
using Brage.AliasBeGone.Infrastructure;
using Brage.AliasBeGone.PatternMatching;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.Text.Editor;
using Constants = Brage.AliasBeGone.Infrastructure.Constants;

namespace Brage.AliasBeGone
{
    [PackageRegistration(UseManagedResourcesOnly = true)]
    [InstalledProductRegistration("#110", "#112", "0.1", IconResourceID = 400)]
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [ProvideAutoLoad("{f1536ef8-92ec-443c-9ed7-fdadf150da82}")]
    [Guid(Constants.ALIAS_BE_GONE_PACKAGE_ID_STRING)]
    internal sealed class AliasBeGonePackage : PackageBase
    {
        private const String USING_SYSTEM = "using System;";
        private readonly PatternMatcher _patternMatcher;
        private OleMenuCommand _aliasBeGoneMenuItem;

        public AliasBeGonePackage()
        {
            _patternMatcher = new PatternMatcher(new Configuration());
        }

        protected override void Initialize()
        {
            base.Initialize();

            var menuCommandService = GetService<IMenuCommandService>();

            if (menuCommandService == null)
                return;

            BuildAliasBeGoneCommand(menuCommandService);
        }

        private void BuildAliasBeGoneCommand(IMenuCommandService menuCommandService)
        {
            var aliasBeGoneCommand = new CommandID(new Guid(Constants.ALIAS_BE_GONE_COMMANDSET_ID_STRING), Constants.ALIAS_BE_GONE_COMMAND);

            _aliasBeGoneMenuItem = new OleMenuCommand(OnExecute, aliasBeGoneCommand);
            _aliasBeGoneMenuItem.BeforeQueryStatus += OnBeforeQueryStatus;

            menuCommandService.AddCommand(_aliasBeGoneMenuItem); 
        }

        private void OnBeforeQueryStatus(Object sender, EventArgs e)
        {
            var dte = GetService<SDTE, DTE2>();
            _aliasBeGoneMenuItem.Enabled = dte.ActiveWindow.Caption.EndsWith(".cs");
        }

        private void OnExecute(Object sender, EventArgs e)
        {
            var userData = GetUserData();

            if (userData == null)
                return;

            var textView = GetTextView(userData);
            var text = textView.TextBuffer.CurrentSnapshot.GetText();

            Apply(_patternMatcher.Search(text), textView);
        }

        private void Apply(IEnumerable<PatternHit> patternHits, ITextView textView)
        {
            var text = textView.TextBuffer.CurrentSnapshot.GetText();
            var textEdit = textView.TextBuffer.CreateEdit();

            if (!text.Contains(USING_SYSTEM))
                textEdit.Insert(0, USING_SYSTEM + Environment.NewLine);

            foreach (var patternHit in patternHits)
                textEdit.Replace(patternHit.StartPosition, patternHit.CharsToReplace, patternHit.ReplaceWith);

            textEdit.Apply();
        }
    }
}
