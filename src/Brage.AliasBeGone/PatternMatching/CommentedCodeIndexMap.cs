using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brage.AliasBeGone.PatternMatching
{
	internal class CommentedCodeIndexMap
	{
		/// <summary>
		/// Contains the begin and end index ranges for all comments.
		/// </summary>
		private List<Tuple<Int32, Int32>> _listOfIndexes = new List<Tuple<int, int>>();

		/// <summary>
		/// The text to search that this index map was created with.
		/// </summary>
		public String TextToSearch { get; private set; }

		/// <summary>
		/// Will determine if the current index is inside commented code.
		/// </summary>
		/// <param name="currentIndex">The current index to check to see if inside commented code.</param>
		public bool IsIndexCommentedCode(int currentIndex)
		{
			return _listOfIndexes.Any(x => x.Item1 <= currentIndex && x.Item2 >= currentIndex);
		}

		/// <summary>
		/// Will create a index map based on the textToSearch parameter.
		/// </summary>
		/// <param name="textToSearch">The source code to create an index map against for source code comments.</param>
		/// <returns></returns>
		public static CommentedCodeIndexMap CreateMap(String textToSearch)
		{
			var indexMap = new CommentedCodeIndexMap()
			{
				TextToSearch = textToSearch,
			};

			var currentIndex = 0;
			var startIndexOfComment = 0;
			var endIndexOfComment = 0;

			while (currentIndex < textToSearch.Length - 2) //will be checking next character as well.
			{
				var characters = textToSearch.Substring(currentIndex, 2);
				switch (characters)
				{
					default:
						//no comment, check the next character
						currentIndex++;
						continue;
					case "//": //single line comment
						endIndexOfComment = textToSearch.IndexOf("\n", currentIndex) - 1;
						break;
					case "/*":
						endIndexOfComment = textToSearch.IndexOf("*/", currentIndex) + 2;
						break;
				}

				startIndexOfComment = currentIndex;

				//if the last line of the file or strange overflow
				if (endIndexOfComment < 0 || endIndexOfComment < startIndexOfComment)
					endIndexOfComment = textToSearch.Length - 1;

				indexMap._listOfIndexes.Add(new Tuple<int, int>(startIndexOfComment, endIndexOfComment));

				currentIndex = endIndexOfComment;
			}

			return indexMap;
		}
	}
}