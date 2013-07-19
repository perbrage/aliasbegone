### Alias Be Gone
A C# alias to .NET CLR type replacer extension for VS2010, VS2012 and VS2013 Preview that provides a keyboard shortcut to quickly replace all aliases to its CLR equivalents in current active document. Alias Be Gone also provides optional snippets that can be installed to provide development using aliases, that quickly converts into CLR type when you double-tap Tab like any other snippet. 

### Target audience
Are you a C# developer who is also a CLR purist that would never use aliases like short, int or long in your code? Do you have a war in your shop where other developers replace your beautiful code with those pesky aliases colored like keywords? Then this extension is for you!

Alias Be gone is your extension to quickly replace all C# aliases to corresponding CLR type and put you one step ahead in the fight against C# aliases. 

### Installation
* Install Alias Be Gone extension through Visual Studios Extension Manager like any other Visual Studio extension. If you have something else bound to Ctrl-K, Ctrl-J you may need to bind the extension to another shortcut.
* Optional! If you want to install the bundled snippets you can pull down the Edit menu after restarting Visual Studio and choose Install Snippets right under Alias Be Gone menu command.

### Usage

Alias Be Gone provides a menu command with shortcut (Ctrl+K, Ctrl+J) to quickly replace all aliases. Simply open up a C# code file and use Alias Be Gone shortcut and all your aliases will be converted into CLR types. You can also select one or more code files directly in the Solution Explorer and have several code files converted at the same time.

If you installed the bundled snippets you can develop using the alias names and then press tab twice after like any other snippet. For example, type bool and then press tab twice to instantly replace it to Boolean

### History

* Version 0.8
    * Fixed an issue concerning people using the Keep Tabs option in Visual Studio
    * Added support for Visual Studio 12 (Currently Visual Studio 2013 Preview)

* Version 0.7
    * Now ignores matches in comments. (Thanks to Bill Robertson)
    * Added support to selected one or more code files directly in the solution explorer and have them processed with same shortcut / menuitem as usual

* Version 0.6
    * Fixed a minor issue
    * Updated information about supporting VS2012

* Version 0.5
    * Additional search patterns added

* Version 0.4
    * VS11 support
    * Added missing search patterns

* Version 0.3
    * First public release!
    

### Links

[Alias Be Gone on Visual Studio Gallery](http://visualstudiogallery.msdn.microsoft.com/f4f18ca8-187b-4f3d-9a1e-eeb8330bb1f7)
