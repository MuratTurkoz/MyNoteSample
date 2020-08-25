using MyNoteSample.Entities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyNoteSample.Web.Models.ViewModels.NotifyViewModels
{
    public class WarningViewModel : NotifyViewModelBase<string>
    {
        public WarningViewModel()
        {
            Title = "Uyarı!";
            RedirectingTimeOut = 3000;
        }
    }
}