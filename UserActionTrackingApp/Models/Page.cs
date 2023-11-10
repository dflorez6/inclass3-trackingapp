/*  Page.cs
    In Class 1

    Revision History
    David Florez ID: 8820815, 2023.11.10: Created
*/
namespace UserActionTrackingApp.Models
{
    public class Page
    {
        //====================
        // Props
        //====================
        public string PageKey { get; set; }
        public int PageCounter { get; set; }

        //====================
        // Constructors
        //====================
        // Default
        public Page()
        {
            PageKey = string.Empty;
            PageCounter = 0;
        }

        // Non-Default
        public Page(string controllerAction)
        {
            PageKey = controllerAction;
            PageCounter = 0;
        }
    }
}
