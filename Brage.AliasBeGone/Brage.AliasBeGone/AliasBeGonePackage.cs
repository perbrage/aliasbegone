using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.ComponentModel.Design;
using System.Windows;
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
        private OleMenuCommand _aliasBeGoneConvertMenuItem;
        private OleMenuCommand _aliasBeGoneInstallSnippetsMenuItem;

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

            BuildAliasBeGoneConvertCommand(menuCommandService);
            BuildAliasBeGoneInstallSnippetsCommand(menuCommandService);
        }

        private void BuildAliasBeGoneConvertCommand(IMenuCommandService menuCommandService)
        {
            var aliasBeGoneConvertCommand = new CommandID(new Guid(Constants.ALIAS_BE_GONE_COMMANDSET_ID_STRING), Constants.ALIAS_BE_GONE_CONVERT_COMMAND);

            _aliasBeGoneConvertMenuItem = new OleMenuCommand(OnExecuteConvert, aliasBeGoneConvertCommand);
            _aliasBeGoneConvertMenuItem.BeforeQueryStatus += OnBeforeQueryStatus;

            menuCommandService.AddCommand(_aliasBeGoneConvertMenuItem); 
        }

        private void BuildAliasBeGoneInstallSnippetsCommand(IMenuCommandService menuCommandService)
        {
            var aliasBeGoneInstallSnippetsCommand = new CommandID(new Guid(Constants.ALIAS_BE_GONE_COMMANDSET_ID_STRING), Constants.ALIAS_BE_GONE_INSTALL_SNIPPETS_COMMAND);
            _aliasBeGoneInstallSnippetsMenuItem = new OleMenuCommand(OnExecuteInstallSnippets, aliasBeGoneInstallSnippetsCommand);
            menuCommandService.AddCommand(_aliasBeGoneInstallSnippetsMenuItem);
        }


        private void OnBeforeQueryStatus(Object sender, EventArgs e)
        {
            var dte = GetService<SDTE, DTE2>();
            _aliasBeGoneConvertMenuItem.Enabled = dte.ActiveWindow.Caption.EndsWith(".cs");
        }

        private void OnExecuteConvert(Object sender, EventArgs e)
        {
            var userData = GetUserData();

            if (userData == null)
                return;

            var textView = GetTextView(userData);
            var text = textView.TextBuffer.CurrentSnapshot.GetText();

            Apply(_patternMatcher.Search(text), textView);
        }

        private void OnExecuteInstallSnippets(Object sender, EventArgs e)
        {
            var messageBoxResult = MessageBox.Show("You are about to install Alias Be Gone snippets in your personal snippets folder. These snippets are optional functionality and Alias Be Gone will work without them! Caution! These snippets will NOT be uninstalled if you remove Alias Be Gone extension. ", "Install Alias Be Gone Snippets?", MessageBoxButton.OKCancel);

            if (messageBoxResult == MessageBoxResult.Cancel)
                return;

            MessageBox.Show("Installed");
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
