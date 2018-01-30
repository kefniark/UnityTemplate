using System;

namespace Utils.EventManager.Tools
{
	public static class PatternMatch
	{
		public static bool IsMatch(string mask, string full) => IsRegexMatch(full, mask);

		#region Regexp 

		private const char Any = '?';
		private const char Wild = '*';

		private const char BracketStart = '[';
		private const char BracketEnd = ']';
		private const char Hyphen = '-';

		private const int Numeric09 = 1;
		private const int Numeric19 = 2;
		private const int Upper = 4;
		private const int Lower = 8;

		private static bool IsRegexMatch(string input, string pattern)
		{
			// 同一文字列ならtrue
			if (pattern == input)
			{
				return true;
			}

			// フォーマットがなければアウト
			if (pattern == string.Empty)
			{
				return false;
			}

			// テキストが空の時はワイルドだけ認める
			if (input == string.Empty)
			{
				return IsTailAllWild(pattern, 0);
			}

			// フォーマットがワイルドならガバガバ
			if (IsTailAllWild(pattern, 0))
			{
				return true;
			}

			var patternIndex = 0;
			var inputIndex = 0;
			var filter = 0;

			while (patternIndex < pattern.Length)
			{
				// [x-y]のパターンかもしれない
				if (patternIndex + 4 < pattern.Length && pattern[patternIndex + 2] == Hyphen)
				{
					// フィルタが既にあるか、[で始まっているならそいつはフィルタである
					if (filter != 0 || pattern[patternIndex] == BracketStart)
					{
						// 最低でも5つ先までないと文法的にアウト
						if (patternIndex + 5 >= pattern.Length)
						{
							throw new IndexOutOfRangeException();
						}

						// フィルタのパターンを減らして楽をする
						if (pattern[patternIndex + 1] == '0' && pattern[patternIndex + 3] == '9')
						{
							filter |= Numeric09;
						}
						else if (pattern[patternIndex + 1] == '1' && pattern[patternIndex + 3] == '9')
						{
							filter |= Numeric19;
						}
						else if (pattern[patternIndex + 1] == 'A' && pattern[patternIndex + 3] == 'Z')
						{
							filter |= Upper;
						}
						else if (pattern[patternIndex + 1] == 'a' && pattern[patternIndex + 3] == 'z')
						{
							filter |= Lower;
						}
						else
						{
							throw new NotImplementedException();
						}


						// ]で閉じられてる
						if (pattern[patternIndex + 4] == BracketEnd)
						{
							patternIndex += 5;
						}

						// [a-bc-d]みたいな感じ
						else if (patternIndex + 7 < pattern.Length && pattern[patternIndex + 5] == Hyphen)
						{
							patternIndex += 3;
						}
						else
						{
							throw new NotImplementedException();
						}


						continue;
					}
				}

				// 同じ文字 or 単一文字
				if (pattern[patternIndex] == input[inputIndex] || pattern[patternIndex] == Any)
				{
					if (filter != 0 && !Filter(input[inputIndex], filter))
					{
						return false;
					}

					++patternIndex;
					++inputIndex;

					if (inputIndex == input.Length)
					{
						return IsTailAllWild(pattern, patternIndex);
					}

					filter = 0;
					continue;
				}

				// ワイルドカード
				if (pattern[patternIndex] == Wild)
				{
					// 末尾までワイルドだけ
					if (IsTailAllWild(pattern, patternIndex))
					{
						// フィルタがないならtrue
						if (filter == 0)
						{
							return true;
						}

						// フィルタがあるならfalseなものがないか検索
						for (int i = inputIndex; i < input.Length; ++i)
						{
							if (!Filter(input[i], filter))
							{
								return false;
							}
						}

						return true;
					}

					// ワイルド以外に語尾がある時
					if (patternIndex < pattern.Length - 1)
					{
						// 移動させたテキスト量
						var shifted = 0;

						while (inputIndex + shifted < input.Length)
						{
							// フィルタを見るもじゃ
							if (filter != 0 && !Filter(input[inputIndex + shifted], filter))
							{
								return false;
							}

							// ワイルドの次の文字と一致する文字がある（or続いている）ときの処理
							if (pattern[patternIndex + 1] == input[inputIndex + shifted]
								|| pattern[patternIndex + 1] == Any)
							{
								++shifted;

								// 最後まで同じ文字じゃったな…
								if (inputIndex + shifted == input.Length)
								{
									return true;
								}

								// 違う文字がきたらbreakなのじゃ
								if (pattern[patternIndex + 1] != input[inputIndex + shifted] && pattern[patternIndex + 1] != Any)
								{
									++patternIndex;
									inputIndex += shifted - 1;
									break;
								}

								// 同じ文字がまだまだ続く
								if (inputIndex + shifted < input.Length)
								{
									continue;
								}
							}

							// ワイルドの次の文字と違う文字の場合は素直にその次へ
							++shifted;

							// 最後まで違う文字じゃったな…
							if (inputIndex + shifted == input.Length)
							{
								return false;
							}
						}

						filter = 0;
						continue;
					}
				}

				return false;
			}

			return false;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="format"></param>
		/// <param name="startIndex"></param>
		/// <returns></returns>
		private static bool IsTailAllWild(string format, int startIndex)
		{
			for (int i = startIndex; i < format.Length; ++i)
			{
				if (format[i] != Wild)
				{
					return false;
				}
			}

			return true;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="input"></param>
		/// <param name="filter"></param>
		/// <returns></returns>
		private static bool Filter(char input, int filter)
		{
			var result = (filter & Numeric09) > 0 && (input >= '0' && input <= '9');

			if ((filter & Numeric19) > 0)
			{
				result = result || (input >= '1' && input <= '9');
			}

			if ((filter & Upper) > 0)
			{
				result = result || (input >= 'A' && input <= 'Z');
			}

			if ((filter & Lower) > 0)
			{
				result = result || (input >= 'a' && input <= 'z');
			}

			return result;
		}

		#endregion
	}
}
