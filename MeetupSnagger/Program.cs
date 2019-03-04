using System;
using System.Collections.Generic;
using System.Linq;
using MeetupSnagger.Models;
using MeetupSnagger.Services;

namespace MeetupSnagger
{
    class Program
    {
        // This key has been invalidated - you'll need to swap out with your own
        readonly static MeetupService meetupService = new MeetupService("64e3b3b757c5e8607f4d565942e43");

        static void Main(string[] args)
        {
            /*
            args = new string[7];          
            args[0] = "groups";
            args[1] = "-c";
            args[2] = "34";
            args[3] = "-z";
            args[4] = "60605";
            args[5] = "-s";
            args[6] = "android";
            */

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

                    GetGroups(cid, zip, search);
                }
            }

            Console.ReadKey();
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

        static async void GetGroups(string categoryId, string zipcode, string searchTerm)
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
        }
    }
}
