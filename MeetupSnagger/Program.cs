using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper;
using MeetupSnagger.Models;
using MeetupSnagger.Services;
using MoreLinq;

namespace MeetupSnagger
{
    class Program
    {
        // This key has been invalidated - you'll need to swap out with your own
        readonly static MeetupService meetupService = new MeetupService("<put your key here>");

        static void Main(string[] args)
        {
            
            args = new string[7];          
            args[0] = "groups";
            args[1] = "-c";
            args[2] = "34";
            args[3] = "-z";
            args[4] = "20001"; // "60605" = chicago; "30301" = Atlanta, "10001" = New York, "20001" = DC
            args[5] = "-s";
            args[6] = "android";

            //args = new string[1];
            //args[0] = "categories";

            if (args.Length > 0)
            {
                if (args.Contains("categories"))
                {
                    GetCategories();
                }
                else if (args.Contains("groups"))
                {
                    string cid = null;
                    string zip = null;
                    string search = null;

                    for (int i = 0; i < args.Length; i++)
                    {
                        if (args[i] == "-c")
                        {
                            cid = args[i + 1];
                        }
                        else if (args[i] == "-z")
                        {
                            zip = args[i + 1];
                        }
                        else if (args[i] == "-s")
                        {
                            search = args[i + 1];
                        }
                    }

                    var searchTerms = new List<string>
                    {
                        "android", "iOS", "apple", "google", "amazon", "aws",
                        "microsoft", "azure", "docker", "kubernetes", ".net",
                        "python", "kotlin", "GCP", "GDP", "ruby",
                        "php", "golang", "java", "cloud", "web", "tech",
                        "technology", "code", "programmer", "developer",
                        "software", "xamarin", "big data", "analytics",
                        "database", "nosql", "javascript"
                    };
                    var groups = new List<SpreadsheetGroupView>();
                    foreach (var term in searchTerms)
                    {
                        Console.WriteLine($"Searching for '{term}'...");
                        Console.WriteLine();
                        var rawGroups = GetGroups(cid, zip, term).Result;
                        rawGroups.ForEach(r => groups.Add(new SpreadsheetGroupView
                        {
                            City = r.City,
                            Name = r.Name,
                            Size = r.Members.ToString(),
                            Url = $"https://www.meetup.com/{r.UrlName}"
                        }));

                        using (var writer = new StreamWriter("output.csv"))
                        {
                            using (var csv = new CsvWriter(writer))
                            {
                                csv.WriteRecords(groups.DistinctBy(g => g.Url));
                            }
                        }

                    }
                }

                Console.ReadKey();
            }
        }

        static async void GetCategories()
        {
            var categoryResponse = await meetupService.GetCategories();

            if (categoryResponse?.Results?.Length > 0)
            {
                Console.WriteLine($"RESULT COUNT = {categoryResponse.Results.Length}\r\n");

                foreach (var category in categoryResponse.Results)
                {
                    Console.WriteLine($"Name: {category.Name}");
                    Console.WriteLine($"ID:   {category.Id}");
                    Console.WriteLine("***********************************************************************");
                }
            }
        }

        static async Task<List<Group>> GetGroups(string categoryId, string zipcode, string searchTerm)
        {
            Group[] groups;

            var parameters = new Dictionary<string, string>();

            if (!string.IsNullOrEmpty(categoryId))
            {
                parameters.Add("category", categoryId);
            }

            if (!string.IsNullOrEmpty(zipcode))
            {
                parameters.Add("zip", zipcode);
            }

            if (!string.IsNullOrEmpty(searchTerm))
            {
                parameters.Add("text", searchTerm);
            }

            if (parameters.Keys?.Count > 0)
            {
                groups = await meetupService.FindGroups(parameters);
            }
            else
            {
                groups = await meetupService.FindGroups();
            }

            int count = groups != null ? groups.Length : 0;

            Console.WriteLine($"RESULT COUNT = {count} \r\n");

            foreach (var group in groups)
            {
                Console.WriteLine($"Name:     {group.Name}");
                Console.WriteLine($"Location: {group.City}, {group.State} - {group.Country}");
                Console.WriteLine($"URL:      https://www.meetup.com/{group.UrlName}");
                Console.WriteLine($"Members:  {group.Members}");
                Console.WriteLine("***********************************************************************");
            }

            return groups.ToList();
        }
    }
}
