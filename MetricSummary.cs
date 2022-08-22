using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

namespace GlobalEconomies
{
    public class MetricSummary

    {
        public static int StartYear;
        public static int EndYear;
        public MetricSummary(int start, int end)
        {
            StartYear = start;
            EndYear = end;
            showMenu();
        }

        public static string trimString(string toTrim)
        {
            if (toTrim.Length > 10)
            {
                return toTrim.Substring(0, 10);
            }
            else
            {
                while (toTrim.Length < 10)
                {
                    toTrim += " ";
                }
                return toTrim;
            }
        }

        public static void showMenu()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("global_economies.xml");
            XmlNode allLabels = xmlDoc.SelectSingleNode("//global_economies/labels");

            List<string> allLabel = new List<string>();

            foreach (XmlNode label in allLabels.ChildNodes)
            {
                foreach(XmlAttribute maybe in label.Attributes)
                {
                    if (maybe.Value.Equals("Year"))
                    {

                    }
                    else
                    {
                        Console.WriteLine(String.Format("{0}: {1}",allLabel.Count+1,maybe.Value));
                        allLabel.Add(maybe.Value);
                    }
                }
            }

            Console.Write("\n Enter a metric #: ");
            String userInput = Console.ReadLine();

            if (Int32.TryParse(userInput, out int metricNumber))
            {
                if (metricNumber > 0 && metricNumber <= allLabel.Count)
                {
                    String metric = allLabel[metricNumber - 1];

                    Console.WriteLine(metric + " by Region\n");
                    List<List<XmlAttribute>> allinfo = new List<List<XmlAttribute>>();


                    XmlNodeList allRegions = xmlDoc.SelectNodes("//global_economies/region");


                    foreach (XmlNode region in allRegions)
                    {
                        List<XmlAttribute> thisRegion = new List<XmlAttribute>();         
                        foreach (XmlNode allInfo in region.SelectNodes("year"))
                        {
                            var year = allInfo.Attributes["yid"];                            
                                if (Int32.TryParse(year.Value, out int regionYear))
                                {
                                    if (regionYear >= StartYear && regionYear <= EndYear)
                                    {
                                        switch (metricNumber)
                                        {
                                            case 1:
                                                XmlNode inflation = allInfo.SelectSingleNode("inflation");
                                                thisRegion.Add(inflation.Attributes["consumer_prices_percent"]);
                                                break;
                                            case 2:
                                                thisRegion.Add(allInfo.SelectSingleNode("inflation").Attributes["gdp_deflator_percent"]);
                                                break;
                                            case 3:
                                                thisRegion.Add(allInfo.SelectSingleNode("interest_rates").Attributes["real"]);
                                                break;
                                            case 4:
                                                thisRegion.Add(allInfo.SelectSingleNode("interest_rates").Attributes["lending"]);
                                                break;
                                            case 5:
                                                thisRegion.Add(allInfo.SelectSingleNode("interest_rates").Attributes["deposit"]);
                                                break;
                                            case 6:
                                                thisRegion.Add(allInfo.SelectSingleNode("unemployment_rates").Attributes["national_estimate"]);
                                                break;
                                            case 7:
                                                thisRegion.Add(allInfo.SelectSingleNode("unemployment_rates").Attributes["modeled_ILO_estimate"]);
                                                break;
                                        }

                                    }
                                }
                            
                        }
                        allinfo.Add(thisRegion);
                       
                    }

                    String header = "\t \t \t Region";
                    for (int start = StartYear; start <= EndYear; start++)
                    {
                        header += String.Format("\t{0}", start);
                    }
                    Console.WriteLine(header);


                    for (int count = 0; count < allRegions.Count; count++)
                    {
                        String output = "";
                        output += trimString(allRegions[count].Attributes["rname"].Value) + "\t \t \t";
                        foreach (XmlAttribute p in allinfo[count])
                        {
                            if (p != null)
                            {
                                output += String.Format("{0} \t", p.Value);
                            }
                            else
                            {
                                output += "_ \t";
                            }
                        }
                        Console.WriteLine(output);
                    }
                }
            }

        }
    }
}
