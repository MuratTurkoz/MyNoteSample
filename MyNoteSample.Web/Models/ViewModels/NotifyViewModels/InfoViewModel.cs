using MyNoteSample.Entities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyNoteSample.Web.Models.ViewModels.NotifyViewModels
{
    public class InfoViewModel : NotifyViewModelBase<string>
    {
        public InfoViewModel()
        {
            Title = "Bilgilendirme..";
            RedirectingTimeOut = 3000;
        }
    }
}