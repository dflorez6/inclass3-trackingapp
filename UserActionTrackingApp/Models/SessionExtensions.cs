/*  SessionExtensions.cs
    In Class 1

    Revision History
    David Florez ID: 8820815, 2023.11.10: Created
*/
using System.Text.Json; // Must be included to work with JSON

namespace UserActionTrackingApp.Models
{
    // Generic Class that extends Session Class
    public static class SessionExtensions
    {
        // Sets an object in JSON
        public static void SetObject<T>(this ISession session,
                                string key, T value)
        {
            session.SetString(key, JsonSerializer.Serialize(value));
        }

        // Gets / Reads an object in JSON
        public static T? GetObject<T>(this ISession session,
                                      string key)
        {
            var json = session.GetString(key);
            if (string.IsNullOrEmpty(json))
            {
                return default(T);
            }
            else
            {
                return JsonSerializer.Deserialize<T>(json);
            }
        }
    }
}
