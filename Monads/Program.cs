using System;

namespace Monads
{
    class Program
    {
        static void Main(string[] args)
        {
            TestMaybe();
            TestEither();
            TestResult();
            Console.ReadKey();
        }

        public static void TestMaybe()
        {
            Console.WriteLine();
            Console.WriteLine("Testing 'Maybe'");
            // implicit conversion
            Maybe<int> five = 5;
            Maybe<int> six = 6;
            Console.WriteLine("5 + 6 is {0}",
                from x in five
                from y in six
                select x + y);

            // explict conversion
            Console.WriteLine("Nothing + 6 is {0}",
                from x in Maybe<int>.Nothing
                from y in (Maybe<int>)6
                select x + y);

            // constructor method
            Console.WriteLine("5 + Nothing is {0}",
                from x in new Maybe<int>(5)
                from y in Maybe<int>.Nothing
                select x + y);
        }

        public static void TestEither()
        {
            Console.WriteLine();
            Console.WriteLine("Testing 'Either':");
            Console.WriteLine("5 + 6 + 7 is {0}",
                from x in Either<DateTime, int>.FromRight(5)
                from y in Either<DateTime, int>.FromRight(6)
                from z in Either<DateTime, int>.FromRight(7)
                select x + y + z);

            Console.WriteLine("Left-DateTime + 6 + 7 is {0}",
                from x in Either<DateTime, int>.FromLeft(DateTime.Now)
                from y in Either<DateTime, int>.FromRight(6)
                from z in Either<DateTime, int>.FromRight(7)
                select x + y + z);

            Console.WriteLine("5 + Left-DateTime + 7 is {0}",
                from x in Either<DateTime, int>.FromRight(5)
                from y in Either<DateTime, int>.FromLeft(DateTime.Now)
                from z in Either<DateTime, int>.FromRight(7)
                select x + y + z);

            Console.WriteLine("5 + 6 + Left-DateTime is {0}",
                from x in Either<DateTime, int>.FromRight(5)
                from y in Either<DateTime, int>.FromRight(6)
                from z in Either<DateTime, int>.FromLeft(DateTime.Now)
                select x + y + z);
        }

        public static void TestResult()
        {
            Console.WriteLine();
            Console.WriteLine("Testing 'Result', an 'Either' subclass:");

            Console.WriteLine("5 * 6 * 7 is {0}",
                from x in new Result<int>(5)
                from y in new Result<int>(6)
                from z in new Result<int>(7)
                select x * y * z);

            Console.WriteLine("Error + 6 + 7 is {0}",
                from x in Result<int>.FromError("Error")
                from y in new Result<int>(6)
                from z in new Result<int>(7)
                select x + y + z);

            Console.WriteLine("5 + Error + 7 is {0}",
                from x in new Result<int>(5)
                from y in Result<int>.FromError("Error")
                from z in new Result<int>(7)
                select x + y + z);

            Console.WriteLine("5 + 6 + Error is {0}",
                from x in new Result<int>(5)
                from y in new Result<int>(6)
                from z in Result<int>.FromError("Error")
                select x + y + z);
        }
    }
}
