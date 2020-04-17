using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HDFC.Web.ViewModels.Masters.Currencies
{
    public class CurrencyEditViewModel : CurrencyCreateViewModel
    {
        [Required]
        public string Uid { get; set; }
    }
}
