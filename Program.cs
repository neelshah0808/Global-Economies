using System;

namespace GlobalEconomies
{
    class Program
    {
        public static int StartYear = 2017;
        public static int EndYear = 2021;

        static void Main(string[] args)
        {
            showMenu();
        }

        public static void showMenu()
        {
            Console.WriteLine("World Economic Data");
            Console.WriteLine("===== ======== ==== \n\n");

            Console.WriteLine(String.Format("'Y' to adjust the range of years (currently {0} to {1})", StartYear, EndYear));
            Console.WriteLine("'R' to print a regional summary");
            Console.WriteLine("'M' to print a specific metric for all regions");
            Console.WriteLine("'X' to exit the program");

            Console.Write(String.Format("Your selection: "));
            String userInput = Console.ReadLine();


            switch (userInput.ToLower()) {
                case "y":
                    bool valid = false;
                    int startYear = StartYear;
                    while (valid == false)
                    {
                        Console.Write("Starting year (1970 to 2021): ");
                        string StartYr = Console.ReadLine();

                        if (Int32.TryParse(StartYr, out int start))
                        {
                            if (start > 1970 && start < 2021)
                            {
                                valid = true;
                                startYear = start;
                            }
                            else
                            {
                                Console.WriteLine("ERROR: Start year must be an interger from 1970 to 2021");
                                valid = false;
                            }
                        }
                        else
                        {
                            Console.WriteLine("ERROR: Start year must be an valid interger");
                            valid = false;
                        }

                    }
                                       

                    valid = false;
                    int endYear = EndYear;
                    while (valid == false)
                    {
                        Console.Write($"Ending Year ({startYear} to {startYear + 5}): ");
                        string endYr = Console.ReadLine();

                        if (Int32.TryParse(endYr, out int end))
                        {
                            if (end > (startYear + 4) || end < startYear)
                            {
                                Console.WriteLine(String.Format("ERROR: End Year must be an interger between {0} and {1}", startYear, startYear+4));
                                valid = false;                                
                            }
                            else
                            {
                                valid = true;
                                endYear = end;
                            }
                        }
                        else
                        {
                            Console.WriteLine("ERROR: Start year must be an valid interger");
                            valid = false;
                        }
                    }

                    StartYear = startYear;
                    EndYear = endYear;

                    Console.WriteLine("\n\n");
                    showMenu();

                    break;
                case "r":
                    RegionalSummary regSumm = new RegionalSummary(StartYear, EndYear);
                    showMenu();
                    break;
                case "m":
                    MetricSummary metSumm = new MetricSummary(StartYear, EndYear);
                    showMenu();
                    break;
                case "x":
                    System.Environment.Exit(1);
                    break;
                default:
                    Console.WriteLine("Invalid Input. Please try again. \n\n");
                    showMenu();
                    break;
            }

        }
    }
}
