using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBlogProject.UI.ViewModels
{
    public class WarningViewModel : NotifyViewModelBase<string>
    {
        public WarningViewModel()
        {
            Title = "Warning!";
        }
    }
}