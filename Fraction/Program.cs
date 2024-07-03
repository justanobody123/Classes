using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Fraction
{
	internal class Program
	{
		class Fraction
		{
			private int integer;
			public int Integer
			{
				get { return integer; }
				set { integer = value; }
			}
			private int numerator;
			public int Numerator
			{
				get { return numerator; }
				set { numerator = value; }
			}
			private int denominator;
			public int Denominator
			{
				get { return denominator; }
				set { if (denominator != 0) denominator = value; }
			}
			public void Print()
			{
				if (numerator == 0)
				{
					Console.WriteLine(integer);
				}
				else
				{
					if (integer != 0)
					{
						Console.Write($"{integer}({numerator}/{denominator})");
					}
					else
					{
						Console.Write($"({numerator}/{denominator})");
					}
				}
            }
			public double FractionToDouble()
			{
				return (integer * denominator + numerator) / (double)denominator;
			}
			public static Fraction operator + (Fraction a, Fraction b)
			{
				//Переводим дробь в приемлемый для сложения вид
				a.numerator = a.integer * a.denominator + a.numerator;
				a.integer = 0;
				b.numerator = b.integer * b.denominator + b.numerator;
				b.integer = 0;
				//Общий знаменатель
				int newDenominator = a.denominator * b.denominator;
				//Сумма числителей
				int newNumerator = a.numerator * b.denominator + b.numerator * a.denominator;
				//Выделяем из неправильной дроби целую часть
				int newInteger = newNumerator / newDenominator;
				newNumerator = newNumerator % newDenominator;
				Fraction newFraction = new Fraction(newInteger, newNumerator, newDenominator);
				//Упрощаем дробь
				newFraction.SimplifyFraction();
				return newFraction;
			}
			public static double operator + (Fraction a, double b)
			{
				return a.FractionToDouble() + b;
			}
			public static double operator +(double b, Fraction a)
			{
				return b + a.FractionToDouble();
			}
			public static Fraction operator - (Fraction a, Fraction b)
			{
				//Переводим дробь в приемлемый для вычитания вид
				a.numerator = a.integer * a.denominator + a.numerator;
				a.integer = 0;
				b.numerator = b.integer * b.denominator + b.numerator;
				b.integer = 0;
				//Общий знаменатель
				int newDenominator = a.denominator * b.denominator;
				//Разность числителей
				int newNumerator = a.numerator * b.denominator - b.numerator * a.denominator;
				//Выделяем из неправильной дроби целую часть
				int newInteger = newNumerator / newDenominator;
				newNumerator = newNumerator % newDenominator;
				Fraction newFraction = new Fraction(newInteger, newNumerator, newDenominator);
				//Упрощаем дробь
				newFraction.SimplifyFraction();
				return newFraction;
			}
			public static double operator - (Fraction a, double b)
			{
				return a.FractionToDouble() - b;
			}
			public static double operator - (double b, Fraction a)
			{
				return b - a.FractionToDouble();
			}
			public static Fraction operator * (Fraction a, Fraction b)
			{
				//Переводим дробь в приемлемый для умножения вид
				a.numerator = a.integer * a.denominator + a.numerator;
				a.integer = 0;
				b.numerator = b.integer * b.denominator + b.numerator;
				b.integer = 0;
				//Перемножаем между собой числители и знаменатели
				int newNumerator = a.numerator * b.numerator;
				int newDenominator = b.denominator * a.denominator;
				//Выделяем из неправильной дроби целую часть
				int newInteger = newNumerator / newDenominator;
				newNumerator = newNumerator % newDenominator;
				Fraction newFraction = new Fraction(newInteger, newNumerator, newDenominator);
				//Упрощаем дробь
				newFraction.SimplifyFraction();
				return newFraction;
			}
			public static double operator * (Fraction a, double b)
			{
				return a.FractionToDouble() * b;
			}
			public static double operator * (double b, Fraction a)
			{
				return b * a.FractionToDouble();
			}
			public static Fraction operator / (Fraction a, Fraction b)
			{
				//Переводим дробь в приемлемый для деления вид
				a.numerator = a.integer * a.denominator + a.numerator;
				a.integer = 0;
				b.numerator = b.integer * b.denominator + b.numerator;
				b.integer = 0;
				//Свап
				(b.numerator, b.denominator) = (b.denominator, b.numerator);
				return a * b;
			}
			public static double operator /(Fraction a, double b)
			{
				return a.FractionToDouble() / b;
			}
			public static double operator /(double b, Fraction a)
			{
				return b / a.FractionToDouble();
			}
			public static bool operator > (Fraction a, Fraction b)
			{
				return a.FractionToDouble() > b.FractionToDouble();
			}
			public static bool operator < (Fraction a, Fraction b)
			{
				return a.FractionToDouble() < b.FractionToDouble();
			}
			public static bool operator >(Fraction a, double b)
			{
				return a.FractionToDouble() > b;
			}
			public static bool operator <(Fraction a, double b)
			{
				return a.FractionToDouble() < b;
			}
			public static bool operator >(double a, Fraction b)
			{
				return a > b.FractionToDouble();
			}
			public static bool operator <(double a, Fraction b)
			{
				return a < b.FractionToDouble();
			}
			public void SimplifyFraction()
			{
				int gcd = GreatestCommonDivisor(this.numerator, this.denominator);
				while (gcd != 1)
				{
					this.numerator /= gcd;
					this.denominator /= gcd;
					gcd = GreatestCommonDivisor(this.numerator, this.denominator);
				}		
			}
			public Fraction(int integer, int numerator, int denominator)
            {
                this.integer = integer;
				this.numerator = numerator;
				this.denominator = denominator;
            }
        }
		static void Main(string[] args)
		{
			Fraction twoAndAHalf = new Fraction(2, 1, 2);
			Fraction threeQuarters = new Fraction(0, 3, 4);
			//Сложение двух дробей: 2(1/2) + (3/4)
			Fraction sum = twoAndAHalf + threeQuarters;
			Console.Write("Сложение двух дробей: ");
			sum.Print(); //Ожидаемый результат: 3(1/4)
			Console.WriteLine();
			//Вычитание двух дробей: 2(1/2) - (3/4)
			Fraction difference = twoAndAHalf - threeQuarters;
			Console.Write("Вычитание двух дробей: ");
			difference.Print(); //Ожидаемый результат: 1(3/4)
			Console.WriteLine();
			//Умножение двух дробей: 2(1/2) * (3/4)
			Fraction product = twoAndAHalf * threeQuarters;
			Console.Write("Умножение двух дробей: ");
			product.Print(); //Ожидаемый результат: 1(7/8)
			Console.WriteLine();
			//Деление одной дроби на другую: 2(1/2) / (3/4)
			Fraction quotient = twoAndAHalf / threeQuarters;
			Console.Write("Деление одной дроби на другую: ");
			quotient.Print(); //Ожидаемый результат: 3(1/3)
			Console.WriteLine();
			//Преобразование дроби в double и сложение с числом типа double
			double doubleResult = twoAndAHalf.FractionToDouble() + 2.5;
			Console.WriteLine($"Преобразование дроби в double и сложение с числом типа double: {doubleResult}");
			//Сравнение дробей: 2(1/2) > (3/4)
			bool greaterThan = twoAndAHalf > threeQuarters;
			Console.WriteLine($"Сравнение дробей: 2(1/2) > (3/4)? Ответ: {greaterThan}");
			//Сравнение дробей: 2(1/2) < (3/4)
			bool lessThan = twoAndAHalf < threeQuarters;
			Console.WriteLine($"Сравнение дробей: 2(1/2) < (3/4)? Ответ: {lessThan}");
		}
		static int GreatestCommonDivisor(int a, int b)
		{
			while (b != 0)
			{
				int temp = b;
				b = a % b;
				a = temp;
			}
			return a;
		}
	}
}
