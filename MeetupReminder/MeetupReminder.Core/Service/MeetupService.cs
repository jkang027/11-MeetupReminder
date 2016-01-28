using CSharp.Meetup.Connect;
using MeetupReminder.Core.Domain;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Spring.Social.OAuth1;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetupReminder.Core.Service
{
    public class MeetupService
    {
        private const string MeetupApiKey = "phu5pogo05samhquo4ni2qk84l";
        private const string MeetupSecretKey = "9uvqfg5ep1kcbtf9e5p71a46qq";

        private static async Task<OAuthToken> authenticate()
        {
            var meetupServiceProvider = new MeetupServiceProvider(MeetupApiKey, MeetupSecretKey);

            var oauthToken = await meetupServiceProvider.OAuthOperations.FetchRequestTokenAsync("oob", null);

            var authenticateUrl = meetupServiceProvider.OAuthOperations.BuildAuthorizeUrl(oauthToken.Value, null);

            Process.Start(authenticateUrl);

            Console.WriteLine("Enter the pin from meetup.com");
            string pin = Console.ReadLine();

            var requestToken = new AuthorizedRequestToken(oauthToken, pin);
            var oauthAccessToken = meetupServiceProvider.OAuthOperations.ExchangeForAccessTokenAsync(requestToken, null).Result;

            return oauthAccessToken;
        }

        public static async Task<MeetupSearchResult> GetMeetupsFor(string meetupGroupName)
        {
            var token = await authenticate();

            var meetupServiceProvider = new MeetupServiceProvider(MeetupApiKey, MeetupSecretKey);

            var meetup = meetupServiceProvider.GetApi(token.Value, token.Secret);

            string json = await meetup.RestOperations.GetForObjectAsync<string>($"https://api.meetup.com/2/events?group_urlname={meetupGroupName}");

            var oEvents = JsonConvert.DeserializeObject<MeetupSearchResult>(json);

            return oEvents;
        }
    }
}
