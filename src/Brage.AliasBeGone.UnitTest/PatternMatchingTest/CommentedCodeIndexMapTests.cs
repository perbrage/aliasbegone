using Brage.AliasBeGone.PatternMatching;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brage.AliasBeGone.UnitTest.PatternMatchingTest
{
	[TestFixture]
	public class CommentedCodeIndexMapTests
	{
		[Test]
		public void should_process_slashslash_comments_as_single_line()
		{
			var s1 = @"
using System;

namespace Brage.AliasBeGone
{
	";
			var s2 = @"//this is the class that does everything.";

			var s3 = @"

	public class Bob
	{
		
	}
}
";
			var s4 = "//and then a little bit more";

			var indexMap = CommentedCodeIndexMap.CreateMap(s1 + s2 + s3 + s4);

			var culmativeIndex = 0;
			for (int i = 0; i < s1.Length; i++)
			{
				Assert.False(indexMap.IsIndexCommentedCode(i));
			}
			culmativeIndex += s1.Length;

			for (int i = 0; i < s2.Length; i++)
			{
				Assert.True(indexMap.IsIndexCommentedCode(i + culmativeIndex));
			}
			culmativeIndex += s2.Length;

			for (int i = 1; i < s3.Length; i++)
			{
				Assert.False(indexMap.IsIndexCommentedCode(i + culmativeIndex));
			}
			culmativeIndex += s3.Length;

			for (int i = 0; i < s4.Length; i++)
			{
				Assert.True(indexMap.IsIndexCommentedCode(i + culmativeIndex));
			}
			culmativeIndex += s4.Length;

		}

		[Test]
		public void should_process_multiline_comments_as_single_line()
		{
			var s1 = @"
using System;

namespace Brage.AliasBeGone
{
	";
			var s2 = @"/*
Some comments make this more exciting
//this is the class that does everything.
*/
";

			var s3 = @"

	public class Bob
	{
		
	}
}
";
			var s4 = "/*Should be a compiler error I think.";

			var indexMap = CommentedCodeIndexMap.CreateMap(s1 + s2 + s3 + s4);

			var culmativeIndex = 0;
			for (int i = 0; i < s1.Length; i++)
			{
				Assert.False(indexMap.IsIndexCommentedCode(i));
			}
			culmativeIndex = s1.Length;

			for (int i = 0; i < s2.Length - 2; i++)
			{
				Assert.True(indexMap.IsIndexCommentedCode(i + culmativeIndex));
			}
			culmativeIndex += s2.Length - 2;

			for (int i = 1; i < s3.Length; i++)
			{
				Assert.False(indexMap.IsIndexCommentedCode(i + culmativeIndex));
			}
			culmativeIndex += s3.Length;

			for (int i = 2; i < s4.Length; i++)
			{
				Assert.True(indexMap.IsIndexCommentedCode(i + culmativeIndex));
			}
			culmativeIndex += s4.Length;
		}
	}
}
