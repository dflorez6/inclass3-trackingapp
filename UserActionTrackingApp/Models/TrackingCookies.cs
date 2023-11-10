/*  TrackingCookies.cs
    In Class 1

    Revision History
    David Florez ID: 8820815, 2023.11.10: Created
*/
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Text.Json; // Must be included to work with JSON

namespace UserActionTrackingApp.Models
{
    public class TrackingCookies
    {
        //====================
        // Props
        //====================
        private const string CookiesKey = "Kanut";
        private IRequestCookieCollection requestCookies { get; set; } = null!;
        private IResponseCookies responseCookies { get; set; } = null!;
        public static Dictionary<string, int> CookiesPageCounter { get; set; } = new Dictionary<string, int>();

        //====================
        // Constructors
        //====================
        public TrackingCookies(IRequestCookieCollection cookies)
        {
            requestCookies = cookies;
        }

        public TrackingCookies(IResponseCookies cookies)
        {
            responseCookies = cookies;
        }

        //====================
        // Methods
        //====================
        // GetTrackingCount
        // Description: Returns the value of CookieCounter stored in the session
        // Parameters: None
        // Returns: Int
        public int GetCookiesCounter(string dictionaryKey)
        {
            Dictionary<string, int> CookiesPageCounterDictionary;
            int pageCounter = 1;

            // Checks if there are cookies that match the key && assigns it to a variable
            string cookieString = requestCookies[CookiesKey] ?? String.Empty;

            // Checks if cookieString is not empty (there are cookies)
            if (cookieString != string.Empty)
            {
                // Deserialize JSON string
                CookiesPageCounterDictionary = JsonConvert.DeserializeObject<Dictionary<string, int>>(cookieString);

                // Only returns the value that matches the dictionaryKey
                if (CookiesPageCounter.ContainsKey(dictionaryKey))
                    pageCounter = CookiesPageCounterDictionary[dictionaryKey];
            }

            return pageCounter;
        }

        // IncreaseTrackingCount
        // Description: Increments the counter by 1 per visit && stores it in the cookies
        // Parameters: None
        // Returns: Void
        public void IncreaseCookiesCounter(string controllerName, string actionName)
        {
            // Cookies options
            CookieOptions options = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(90) // Will expire in 90 days
            };

            // Initializing Page Object
            Page page = new Page($"{controllerName}-{actionName}");

            // Checks if a value inside of the Dictionary exists based on the key            
            if (CookiesPageCounter.ContainsKey(page.PageKey))
                // If it exists, update PageCounter
                CookiesPageCounter[page.PageKey]++;
            else
                // If it doesn't exist, add Key, Value to the Dictionary
                CookiesPageCounter.Add(page.PageKey, page.PageCounter + 1);

            // Serialize the dictionary into a string
            var dictionaryString = JsonConvert.SerializeObject(CookiesPageCounter);

            // Console.WriteLine("Cookies");
            // Console.WriteLine("dictionaryString " + dictionaryString);

            RemoveCookies();     // delete old cookie first

            // Append to Cookies
            responseCookies.Append(CookiesKey, dictionaryString, options); // Must use Response.Cookies in the Controller to set Cookies
        }

        public void RemoveCookies()
        {
            responseCookies.Delete(CookiesKey);
        }

        /*
        //
        public string[] GetMyTeamsIds()
        {
            string cookie = requestCookies["Kanut"] ?? String.Empty; // checks if there are cookies that match the key

            if (string.IsNullOrEmpty(cookie))
                return Array.Empty<string>();   // empty string array
            else
                return cookie.Split(Delimiter);
        }

        public void RemoveMyTeamIds()
        {
            responseCookies.Delete(TeamsKey);
        }
        */

    }
}
