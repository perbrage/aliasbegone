using System;
using System.Collections.Generic;
using Brage.AliasBeGone.Infrastructure;
using Brage.AliasBeGone.UnitTest.InfrastructureTest.Support;
using FakeItEasy;
using NUnit.Framework;

namespace Brage.AliasBeGone.UnitTest.InfrastructureTest
{
    [TestFixture]
    public class SnippetsManagerTest
    {
        [Test]
        public void IsInstalled_NoSnippetExists_ReturnsFalse()
        {
            //Arrange
            var resourceReader = A.Fake<IResourceReader>();
            var configuration = A.Fake<IConfiguration>();
            A.CallTo(() => configuration.GetSnippets()).Returns(new List<String>
                                                                    {
                                                                        "Delegate.snippet",
                                                                        "Object.snippet"
                                                                    });
            var snippetManager = new SnippetsManagerTestHarness(configuration, resourceReader);

            //Act
            var result = snippetManager.IsInstalled;

            //Assert
            Assert.IsFalse(result);  
        }

        [Test]
        public void IsInstalled_OnlyOneSnippetExists_ReturnsFalse()
        {
            //Arrange
            var resourceReader = A.Fake<IResourceReader>();
            var configuration = A.Fake<IConfiguration>();
            A.CallTo(() => configuration.GetSnippets()).Returns(new List<String>
                                                                    {
                                                                        "Boolean.snippet",
                                                                        "Int32.snippet"
                                                                    });
            var snippetManager = new SnippetsManagerTestHarness(configuration, resourceReader);

            //Act
            var result = snippetManager.IsInstalled;

            //Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void IsInstalled_AllSnippetsExists_ReturnsTrue()
        {
            //Arrange
            var resourceReader = A.Fake<IResourceReader>();
            var configuration = A.Fake<IConfiguration>();
            A.CallTo(() => configuration.GetSnippets()).Returns(new List<String>
                                                                    {
                                                                        "Boolean.snippet",
                                                                        "String.snippet"
                                                                    });
            var snippetManager = new SnippetsManagerTestHarness(configuration, resourceReader);

            //Act
            var result = snippetManager.IsInstalled;

            //Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void Install_SnippetIsNotInstalled_CallsCreateSnippet()
        {
            //Arrange
            var resourceReader = A.Fake<IResourceReader>();
            var configuration = A.Fake<IConfiguration>();
            A.CallTo(() => resourceReader.GetResourceContent(A<String>.Ignored)).Returns("ResourceContent");
            A.CallTo(() => configuration.GetSnippets()).Returns(new List<String>
                                                                    {
                                                                        "Delegate.snippet"
                                                                    });
            var snippetManager = new SnippetsManagerTestHarness(configuration, resourceReader);
            
            //Act
            snippetManager.Install();

            //Assert
            Assert.IsTrue(snippetManager.CreateSnippetCalled);
        }

        [Test]
        public void Install_SnippetIsInstalled_DoesNotCallCreateSnippet()
        {
            //Arrange
            var resourceReader = A.Fake<IResourceReader>();
            var configuration = A.Fake<IConfiguration>();
            A.CallTo(() => resourceReader.GetResourceContent(A<String>.Ignored)).Returns("ResourceContent");
            A.CallTo(() => configuration.GetSnippets()).Returns(new List<String>
                                                                    {
                                                                        "Boolean.snippet"
                                                                    });
            var snippetManager = new SnippetsManagerTestHarness(configuration, resourceReader);

            //Act
            snippetManager.Install();

            //Assert
            Assert.IsFalse(snippetManager.CreateSnippetCalled);
        }


        [Test]
        public void Uninstall_SnippetIsInstalled_CallsDeleteSnippet()
        {
            //Arrange
            var resourceReader = A.Fake<IResourceReader>();
            var configuration = A.Fake<IConfiguration>();
            A.CallTo(() => resourceReader.GetResourceContent(A<String>.Ignored)).Returns("ResourceContent");
            A.CallTo(() => configuration.GetSnippets()).Returns(new List<String>
                                                                    {
                                                                        "Boolean.snippet"
                                                                    });
            var snippetManager = new SnippetsManagerTestHarness(configuration, resourceReader);

            //Act
            snippetManager.Uninstall();

            //Assert
            Assert.IsTrue(snippetManager.DeleteSnippetCalled);
        }

        [Test]
        public void Uninstall_SnippetIsNotInstalled_DoesNotCallDeleteSnippet()
        {
            //Arrange
            var resourceReader = A.Fake<IResourceReader>();
            var configuration = A.Fake<IConfiguration>();
            A.CallTo(() => resourceReader.GetResourceContent(A<String>.Ignored)).Returns("ResourceContent");
            A.CallTo(() => configuration.GetSnippets()).Returns(new List<String>
                                                                    {
                                                                        "Delegate.snippet"
                                                                    });
            var snippetManager = new SnippetsManagerTestHarness(configuration, resourceReader);

            //Act
            snippetManager.Uninstall();

            //Assert
            Assert.IsFalse(snippetManager.DeleteSnippetCalled);
        }

    }
}
