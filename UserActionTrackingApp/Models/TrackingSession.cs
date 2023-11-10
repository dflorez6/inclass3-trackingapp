/*  TrackingSession.cs
    In Class 1

    Revision History
    David Florez ID: 8820815, 2023.11.10: Created
*/
namespace UserActionTrackingApp.Models
{
    public class TrackingSession
    {
        //====================
        // Props
        //====================
        private ISession _session { get; set; }
        private const string SessionKey = "Kanut"; // Defines a Key
        public static Dictionary<string, int> SessionPageCounter { get; set; } = new Dictionary<string, int>();


        //====================
        // Constructor
        //====================
        public TrackingSession(ISession sess) => _session = sess;

        //====================
        // Methods
        //====================
        // GetTrackingCount
        // Description: Returns the value of SessionCounter stored in the session
        // Parameters: None
        // Returns: Int
        public int GetSessionCounter()
        {
            return _session.GetObject<Int32>(SessionKey);
        }

        // IncreaseTrackingCount
        // Description: Increments the counter by 1 per visit && stores it in the session
        // Parameters: None
        // Returns: Void
        public void IncreaseSessionCounter(string controllerName, string actionName)
        {
            // Initializing Page Object. Will be stored in the Dictionary
            Page page = new Page($"{controllerName}-{actionName}");

            // Checks if a value inside of the Dictionary exists based on the key
            if (SessionPageCounter.ContainsKey(page.PageKey))
                // If it exists, update PageCounter
                SessionPageCounter[page.PageKey]++;
            else
                // If it doesn't exist, add Key, Value to the Dictionary
                SessionPageCounter.Add(page.PageKey, page.PageCounter + 1);

            // Store Dictionary into the session
            _session.SetObject(SessionKey, SessionPageCounter);
        }

        public int ReturnSessionPageCount(string dictionaryKey) {            
            int pageCounter = 0;

            // Based on the key (Controller-Action), returns the corresponding counter for the specific page
            if (SessionPageCounter.ContainsKey(dictionaryKey))
                pageCounter = SessionPageCounter[dictionaryKey];

            return pageCounter;
        }

        // IncreaseTrackingCount
        // Description: Returns SessionPageCounter Dictionary for consumption in the Views
        // Parameters: None
        // Returns: Dictionary
        public Dictionary<string, int> ShowDictionary()
        {
            return SessionPageCounter;
        }

    }
}
