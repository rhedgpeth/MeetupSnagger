using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MeetupSnagger.Models;

namespace MeetupSnagger.Services
{
    public class MeetupService : BaseHttpService
    {
        string ApiKey { get; set; }

        public MeetupService(string apiKey) : base($"https://api.meetup.com/")
        {
            ApiKey = apiKey;
        }

        public Task<CategoryResponse> GetCategories() => GetAsync<CategoryResponse>($"2/categories?{GetApiAuthenticationString()}");

        public Task<Group[]> FindGroups(Dictionary<string, string> parameters = null)
        {
            if (parameters != null)
            {
                return GetAsync<Group[]>($"find/groups?{GetQueryString(parameters)}");
            }

            return GetAsync<Group[]>($"find/groups");
        }

        string GetApiAuthenticationString() => $"key={ApiKey}&sign=true";

        string GetQueryString(Dictionary<string, string> parameters)
        {
            var query = new StringBuilder(GetApiAuthenticationString());

            foreach(var key in parameters.Keys)
            {
                query.Append($"&{key}={parameters[key]}");
            }

            return query.ToString();
        }
    }
}
