﻿using NUnit.Framework;
using ProgrammingLanguage.LexicalAnalysis;

namespace ProgrammingLanguageTest.LexerTest
{
    [TestFixture]
    public class Lexer_AndOrTest
    {
        //###################################################################################
        #region Tests

        [TestCase("ve veya")]
        [TestCase("veya ve")]
        public void CreateLexer_AndOrParse_GetTokens(string input)
        {
            Lexer lexer = new Lexer(input);
            lexer.Lex();

            Assert.AreEqual(3, lexer.TokenList.Count);
        }

        #endregion
    }
}
