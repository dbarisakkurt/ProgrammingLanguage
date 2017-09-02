using NUnit.Framework;
using ProgrammingLanguage.Interpreter;
using ProgrammingLanguage.LexicalAnalysis;
using ProgrammingLanguage.SyntaxAnalysis;
using System;
using System.IO;

namespace ProgrammingLanguageTest.ExampleProgramTest
{
    [TestFixture]
    public class CompleteProgramTest
    {
        //###################################################################################
        #region Setup/TearDown

        [TearDown]
        public void TearDown()
        {
            StreamWriter sw = new StreamWriter(Console.OpenStandardOutput());
            sw.AutoFlush = true;
            Console.SetOut(sw);
        }

        #endregion

        [Test]
        public void UniversityGradeCalculation_Interpret_Successful()
        {
            string program = @"
var not = 65;
eğer(not > 89)
{
	yazdır ""AA"";
}
değilse 
{
    eğer(not > 84)
    {
	    yazdır ""BA"";
    }
    değilse 
    {
        eğer(not > 79)
        {
	        yazdır ""BB"";
        }
        değilse 
        {
            eğer(not > 74)
            {
	            yazdır ""CB"";
            }
            değilse 
            {
                eğer(not > 64)
                {
	                yazdır ""CC"";
                }
                değilse 
                {
                    eğer(not > 57)
                    {
	                    yazdır ""DC"";
                    }
                    değilse 
                    {
                        eğer(not > 39)
                        {
	                        yazdır ""DD"";
                        }
                        değilse
                        {
                            yazdır ""FF"";
                        }
                    }
                }
            }
        }
    }
}
";
            RunWriter(program, "CC");

        }

        [Test]
        public void CelsiusToFahrenheit_Interpret_Successful()
        {
            string program = @"
değişken celcius = 30;
değişken fahrenheit = celcius * 18 / 10 + 32;
yazdır fahrenheit;
";

            RunWriter(program, "86");
        }

        [Test]
        public void FahrenheitToCelsius_Interpret_Successful()
        {
            string program = @"
değişken fahrenheit = 86;
değişken selsius = (fahrenheit - 32) * 5 / 9;
yazdır selsius;
";

            RunWriter(program, "30");
        }

        [Test]
        public void CelsiusToKelvin_Interpret_Successful()
        {
            string program = @"
değişken selsius = 30;
değişken kelvin = selsius + 273;
yazdır kelvin;
";

            RunWriter(program, "303");
        }

        [Test]
        public void KelvinToCelsius_Interpret_Successful()
        {
            string program = @"
değişken kelvin = 30;
değişken selsius = kelvin - 273;
yazdır selsius;
";

            RunWriter(program, "-243");
        }

        [Test]
        public void Factorial_Interpret_Successful()
        {
            string program = @"
değişken a = 5;
değişken c = a+1;
değişken b = 1;
değişken faktöriyel = 1;
oldukça ( c > b)
{
    faktöriyel = faktöriyel * b;
    b = b + 1;
}
yazdır faktöriyel;
";
            RunWriter(program, "120");
        }

        [Test]
        public void RectanglePerimeter_Interpret_Successful()
        {
            string program = @"
değişken boy = 20
değişken en = 35

değişken sonat = (boy + en) * 2;

yazdır sonat;
";
            RunWriter(program, "110");
        }

        [Test]
        public void CirclePerimeter_Interpret_Successful()
        {
            string program = @"
değişken r = 40
değişken pi = 3

değişken daire_çevresi = 2 * pi * r;

yazdır daire_çevresi;
";
            RunWriter(program, "240");
        }

        [Test]
        public void MaximumOf2Numbers_Interpret_Successful()
        {
            string program = @"
değişken num1 = 12;
değişken num2 = 10;

değişken en_büyük = 0;

eğer(num1 >= num2)
{
	en_büyük = num1;
}
değilse 
{
    en_büyük = num2;
}

yazdır en_büyük;
";

            RunWriter(program, "12");
        }

        [Test]
        public void MaximumOf3Numbers_Interpret_Successful()
        {
            string program = @"
değişken num1 = 12;
değişken num2 = 10;
değişken num3 = 15;

değişken no = 0;

eğer(num1 > no)
{
	no = num1;
}

eğer(num2 > no)
{
	no = num2;
}

eğer(num3 > no)
{
	no = num3;
}

yazdır no;
";

            RunWriter(program, "15");
        }

        [Test]
        public void MathOperation_Interpret_Successful()
        {
            string program = @"
değişken kilo = 100;  # gram cinsinden
değişken boy = 2;  # cm cinsinden

değişken BMI = kilo / (boy * boy);

eğer(BMI < 1)
{
  yazdır ""a"";
}
değilse 
{
    eğer(BMI >= 1 ve BMI< 5)
    {
      yazdır ""b"";
    }
    değilse 
    {
        eğer(BMI >= 5 ve BMI< 10)
        {
          yazdır ""c"";
        }
        değilse 
        {
            eğer(BMI >= 10 ve BMI< 15)
            {
              yazdır ""d"";
            }
            değilse
            {
              yazdır ""e"";
            }
        }
    }
}
";

            RunWriter(program, "e");
        }

        [Test]
        public void AverageOf5Numbers_Interpret_Successful()
        {
            string program = @"
değişken para1 = 5;
değişken para2 = 7;
değişken para3 = 12;

değişken ort = (para1 + para2 + para3) / 3;

yazdır ort;
";

            RunWriter(program, "8");
        }

        private void RunWriter(string program, string result)
        {
            var out1 = Console.Out;
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                Interpret(program);
                Assert.AreEqual(result, sw.ToString().Trim());
            }
            Console.SetOut(out1);
        }

        private void Interpret(string program)
        {
            Lexer lexer = new Lexer(program);
            lexer.Lex();

            Parser parser = new Parser(lexer.TokenList);
            parser.ParseProgram();



            Evaluator eval = new Evaluator();
            eval.Evaluate(parser.ProgramNode);
        }
    }
}
