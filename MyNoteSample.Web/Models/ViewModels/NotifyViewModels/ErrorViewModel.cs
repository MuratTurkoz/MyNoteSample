using MyNoteSample.Entities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyNoteSample.Web.Models.ViewModels.NotifyViewModels
{
    public class ErrorViewModel : NotifyViewModelBase<ErrorMessageObj>
    {
        public ErrorViewModel()
        {
            Title = "Dikkat!";
            RedirectingTimeOut = 3000;
        }
    }
}