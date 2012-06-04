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
    [InstalledProductRegistration("#110", "#112", "0.5", IconResourceID = 400)]
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [ProvideAutoLoad("{f1536ef8-92ec-443c-9ed7-fdadf150da82}")]
    [Guid(Constants.ALIAS_BE_GONE_PACKAGE_ID_STRING)]
    internal sealed class AliasBeGonePackage : PackageBase
    {
        private const String USING_SYSTEM = "using System;";
        private const String SNIPPETS_NAMESPACE = "Brage.AliasBeGone.Snippets";
        private const String SNIPPETS_INSTALLED_MESSAGE = "Snippets are now installed!";
        private const String SNIPPETS_UNINSTALLED_MESSAGE = "Snippets are now uninstalled!";
        private const String SNIPPETS_INSTALL_QUESTION_TITLE = "Install Alias Be Gone Snippets?";
        private const String SNIPPETS_INSTALL_QUESTION_DESCRIPTION = "You are about to install Alias Be Gone snippets in your personal snippets folder! " +
                                                                     "These snippets are optional functionality and are not required for Alias Be Gone to function " +
                                                                     "properly! \n\nCaution! These snippets will NOT be removed when you uninstall Alias Be Gone extension. " +
                                                                     "You can however do it manually or use the menu option provided to uninstall these snippets before you " +
                                                                     "uninstall Alias Be Gone";

        private const String ERROR_ENCOUNTERED = "An error occured: ";
        
        private readonly PatternMatcher _patternMatcher;
        private readonly SnippetsManager _snippetsManager;

        private OleMenuCommand _aliasBeGoneConvertMenuItem;
        private OleMenuCommand _aliasBeGoneInstallSnippetsMenuItem;
        private OleMenuCommand _aliasBeGoneUninstallSnippetsMenuItem;

        private Boolean _snippetsInstalled;

        public AliasBeGonePackage()
        {
            var configuration = new Configuration();
            var resourceManager = new ResourceReader(SNIPPETS_NAMESPACE);
            _patternMatcher = new PatternMatcher(configuration);
            _snippetsManager = new SnippetsManager(configuration, resourceManager);

            _snippetsInstalled = _snippetsManager.IsInstalled;
        }

        protected override void Initialize()
        {
            base.Initialize();

            var menuCommandService = GetService<IMenuCommandService>();

            if (menuCommandService == null)
                return;

            _aliasBeGoneConvertMenuItem = BuildMenuItem(menuCommandService, 
                                                        Constants.ALIAS_BE_GONE_CONVERT_COMMAND, 
                                                        OnExecuteConvert, 
                                                        OnBeforeQueryStatusConvert);

            _aliasBeGoneInstallSnippetsMenuItem = BuildMenuItem(menuCommandService, 
                                                                Constants.ALIAS_BE_GONE_INSTALL_SNIPPETS_COMMAND, 
                                                                OnExecuteInstallSnippets, 
                                                                OnBeforeQueryStatusInstallSnippets);

            _aliasBeGoneUninstallSnippetsMenuItem = BuildMenuItem(menuCommandService, 
                                                                  Constants.ALIAS_BE_GONE_UNINSTALL_SNIPPETS_COMMAND, 
                                                                  OnExecuteUninstallSnippets, 
                                                                  OnBeforeQueryStatusUninstallSnippets);

        }

        private OleMenuCommand BuildMenuItem(IMenuCommandService menuCommandService, Int32 commandId, EventHandler onExecute, EventHandler onBeforeQueryStatus)
        {
            var command = new CommandID(new Guid(Constants.ALIAS_BE_GONE_COMMANDSET_ID_STRING), commandId);
            var menuItem = new OleMenuCommand(onExecute, command);

            menuItem.BeforeQueryStatus += onBeforeQueryStatus;
            menuCommandService.AddCommand(menuItem);

            return menuItem;
        }

        private void OnBeforeQueryStatusConvert(Object sender, EventArgs e)
        {
            var dte = GetService<SDTE, DTE2>();
            _aliasBeGoneConvertMenuItem.Enabled = dte.ActiveWindow.Caption.EndsWith(".cs");
        }

        private void OnBeforeQueryStatusInstallSnippets(Object sender, EventArgs e)
        {
            _aliasBeGoneInstallSnippetsMenuItem.Visible = !_snippetsInstalled;
        }

        private void OnBeforeQueryStatusUninstallSnippets(Object sender, EventArgs e)
        {
            _aliasBeGoneUninstallSnippetsMenuItem.Visible = _snippetsInstalled;
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
            var messageBoxResult = MessageBox.Show(SNIPPETS_INSTALL_QUESTION_DESCRIPTION, 
                                                   SNIPPETS_INSTALL_QUESTION_TITLE, 
                                                   MessageBoxButton.OKCancel);

            if (messageBoxResult == MessageBoxResult.Cancel)
                return;

            try
            {
                _snippetsManager.Install();
                _snippetsInstalled = true;

                MessageBox.Show(SNIPPETS_INSTALLED_MESSAGE);
            }
            catch (Exception exception)
            {
                MessageBox.Show(ERROR_ENCOUNTERED + exception.Message);
            }
        }

        private void OnExecuteUninstallSnippets(Object sender, EventArgs e)
        {
            try
            {
                _snippetsManager.Uninstall();
                _snippetsInstalled = false;

                MessageBox.Show(SNIPPETS_UNINSTALLED_MESSAGE);
            }
            catch (Exception exception)
            {
                MessageBox.Show(ERROR_ENCOUNTERED + exception.Message);
            }
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
