using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace meditatii.Models
{
    public abstract class ViewModelBase
    {
        public string CssExtra { get; set; }
    }

    public class HomeViewModel : ViewModelBase
    {
    }
}
