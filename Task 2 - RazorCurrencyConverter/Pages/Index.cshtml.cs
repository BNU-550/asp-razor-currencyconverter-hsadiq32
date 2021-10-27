using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace RazorCurrencyConverter.Pages
{
    public class IndexModel : PageModel
    {
        public Dictionary<string, double> unitConversion = new Dictionary<string, double>();
        //public Dictionary<string, double> unitConversion { get; set; }
        [BindProperty]
        public double Currency1 { get; set; }
        [BindProperty]
        public string Currency1Name { get; set; }
        [BindProperty]
        public string Currency2Name { get; set; }
        private readonly ILogger<IndexModel> _logger;



        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            ViewData["ShowResult"] = "hide";
        }

        public void OnPost()
        {
            UnitConversionData();
            if (Currency1Name.Equals(Currency2Name) || (Currency1 < 0))
            {
                if (Currency1 < 0)
                {
                    ViewData["ErrorMessage"] = "Amount must be larger than 0!";
                }
                else
                {
                    ViewData["ErrorMessage"] = "Selected Currencies are the same!";
                }
                ViewData["ShowResult"] = "hide";
            }
            else
            {
                ViewData["ShowError"] = "hide";
                ViewData["Result"] = Converter(Currency1Name, Currency1, Currency2Name);
            }
            
        }

        public void UnitConversionData()
        {
            unitConversion.Clear();
            unitConversion.Add("GBP", 1);
            unitConversion.Add("EUR", 1.18);
            unitConversion.Add("USD", 1.36);
            unitConversion.Add("CAD", 1.70);
            unitConversion.Add("AUD", 1.85);
        }
        public string Converter(string curr1, double curr1val, string curr2)
        {
            //Convert first value to GBP
            return string.Format("{0:0.00}", (curr1val / unitConversion[curr1]) * unitConversion[curr2]);
        }

    }
}
