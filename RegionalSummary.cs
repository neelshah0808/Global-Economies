using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace GlobalEconomies
{
    public class RegionalSummary
    {
        public static int StartYear;
        public static int EndYear;
        public RegionalSummary(int start, int end)
        {
            StartYear = start;
            EndYear = end;
            showMenu();

        }

        public static void showMenu()
        {
            Console.WriteLine("Select a region by number as shown below...");

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("global_economies.xml");
            XmlNodeList allRegions = xmlDoc.SelectNodes("//global_economies/region");

            List<string> allRegion = new List<string>();

            foreach (XmlNode region in allRegions)
            {
                var regionName = region.Attributes["rname"];
                if (regionName != null)
                {
                    Console.WriteLine(String.Format("{0}: {1} ",allRegion.Count + 1,regionName.Value));
                    allRegion.Add(regionName.Value);
                }
            }

            Console.Write("Enter a region: ");
            String userInput = Console.ReadLine();

            if (Int32.TryParse(userInput, out int regionNumber))
            {
                if (regionNumber <= allRegion.Count && regionNumber > 0)
                {
                    Console.WriteLine("\nEconomic information for "+ allRegion[regionNumber-1] + "\n \n");
                    XmlNode regionSelect = allRegions[regionNumber - 1];
                    XmlNodeList allYears = regionSelect.SelectNodes("year");

                    List<XmlAttribute> CPI = new List<XmlAttribute>();
                    List<XmlAttribute> GDP = new List<XmlAttribute>();
                    List<XmlAttribute> real = new List<XmlAttribute>();
                    List<XmlAttribute> lending = new List<XmlAttribute>();
                    List<XmlAttribute> deposit = new List<XmlAttribute>();
                    List<XmlAttribute> NTL = new List<XmlAttribute>();
                    List<XmlAttribute> IPO = new List<XmlAttribute>();

                    


                    foreach (XmlNode info in allYears)
                    {
                        var year = info.Attributes["yid"];
                        if (Int32.TryParse(year.Value, out int regionYear))
                        {                            
                            if (regionYear >= StartYear && regionYear <= EndYear)
                            {
                                XmlNode inflation = info.SelectSingleNode("inflation");
                                XmlNode interest = info.SelectSingleNode("interest_rates");
                                XmlNode unemployment = info.SelectSingleNode("unemployment_rates");

                                CPI.Add(inflation.Attributes["consumer_prices_percent"]);
                                GDP.Add(inflation.Attributes["gdp_deflator_percent"]);
                                real.Add(interest.Attributes["real"]);
                                lending.Add(interest.Attributes["lending"]);
                                deposit.Add(interest.Attributes["deposit"]);
                                NTL.Add(unemployment.Attributes["national_estimate"]);
                                IPO.Add(unemployment.Attributes["modeled_ILO_estimate"]);
                            }
                        }
                        
                    }

                    List<List<XmlAttribute>> allInfo = new List<List<XmlAttribute>>();
                    allInfo.Add(CPI);
                    allInfo.Add(GDP);
                    allInfo.Add(real);
                    allInfo.Add(lending);
                    allInfo.Add(deposit);
                    allInfo.Add(NTL);
                    allInfo.Add(IPO);

                    String header = "\t Economic Metric";
                    for(int start = StartYear; start<=EndYear; start++)
                    {
                        header += String.Format("\t{0}", start);
                    }

                    Console.WriteLine(header);

                    int count = 1;
                    foreach (List<XmlAttribute> list in allInfo)
                    {
                        String output = " ";
                        switch (count)
                        {
                            case 1:
                                output+="\t   Inflation CPI";
                                break;
                            case 2:
                                output+="\t   Inflation GDP";
                                break;
                            case 3:
                                output+="\t  Real Interest %";
                                break;
                            case 4:
                                output += "\tLending Interest %";
                                break;
                            case 5:
                                output += "\tDeposit Interest %";
                                break;
                            case 6:
                                output += "\tUnemployment NTL %";
                                break;
                            case 7:
                                output += "\tUnemployment IPO %";
                                break;

                        }
                        foreach(XmlAttribute p in list)
                        {
                            if (p != null)
                            {
                                output += String.Format("\t{0}", p.Value);
                            }
                            else
                            {
                                output += "\t _";
                            }
                        }
                        count += 1;
                        Console.WriteLine(output);
                    }
                }
                else
                {
                    Console.WriteLine("ERROR: There's no such region \n \n ");
                    showMenu();
                }
            }
            else
            {
                Console.WriteLine("ERROR: Region must be a number");
                showMenu();
            }
        }

        
    }
    
}
