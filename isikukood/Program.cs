using System.Reflection.Emit;
using System.Xml.Linq;

namespace isikukood
{
    public class Program
    {
        public static void Main()
        {
            //Console.WriteLine(new IdCode("27605030298").GetFullYear());  // 1876
            //Console.WriteLine(new IdCode("37605030299").GetFullYear());  // 1976
            //Console.WriteLine(new IdCode("50005200009").GetFullYear());  // 2000
            //Console.WriteLine(new IdCode("27605030298").GetBirthDate());  // 03.05.1876
            //Console.WriteLine(new IdCode("37605030299").GetBirthDate());  // 03.05.1976
            //Console.WriteLine(new IdCode("50005200009").GetBirthDate());  // 20.05.2000

            //Console.WriteLine(new IdCode("a").IsValid());  // False
            //Console.WriteLine(new IdCode("123").IsValid());  // False
            //Console.WriteLine(new IdCode("37605030299").IsValid());  // True
            //// 30th February
            //Console.WriteLine(new IdCode("37602300299").IsValid());  // False
            //Console.WriteLine(new IdCode("52002290299").IsValid());  // False
            //Console.WriteLine(new IdCode("50002290231").IsValid());  // True
            //Console.WriteLine(new IdCode("30002290231").IsValid());  // False

            //// control number 2nd round
            //Console.WriteLine(new IdCode("51809170123").IsValid());  // True
            //Console.WriteLine(new IdCode("39806302730").IsValid());  // True

            //// control number 3rd round
            //Console.WriteLine(new IdCode("60102031670").IsValid());  // True
            //Console.WriteLine(new IdCode("39106060750").IsValid());  // True
            
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Kirjuta oma isikukood: ");
                string isikukood=Console.ReadLine();
                IdCode id = new IdCode(isikukood);
                if (id.IsValid())
                {
                    while (true)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine();
                        Console.WriteLine("Mida te tahete:\n1) Vaata vanust\n2) Vaata sünnikuupäeva\n3) Vaata sugu\n4) Vaata sündmuskoht\n5) Uus isikukood\n6) Exit");

                        ConsoleKeyInfo vali = Console.ReadKey();
                        Console.WriteLine();
                        switch (vali.KeyChar)
                        {
                            case '1':
                                id.Age(id);
                                break;

                            case '2':
                                Console.WriteLine(id.GetBirthDate());
                                break;

                            case '3':
                                id.Gender(isikukood);
                                break;

                            case '4':
                                IdCode.BirthPlace(isikukood);
                                break;

                            case '5':
                                while (true)
                                {
                                    Console.ForegroundColor = ConsoleColor.White;
                                    Console.WriteLine("Kirjuta uus isikukood: ");
                                    isikukood = Console.ReadLine();
                                    id = new IdCode(isikukood);
                                    if (id.IsValid())
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("Vale isikukood!");
                                    }
                                    Console.WriteLine();
                                }
                                break;

                            case '7':
                                Environment.Exit(0);
                                break;

                            default:
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Vale valik!");
                                break;
                        }
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Vale isikukood!");
                }
            }
        }
    }
}