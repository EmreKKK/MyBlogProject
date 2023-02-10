using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBlogProject.UI.ViewModels
{
    public class OkViewModel : NotifyViewModelBase<string>
    {
        public OkViewModel()
        {
            Title = "Transaction is Successful!";
        }
    }
}