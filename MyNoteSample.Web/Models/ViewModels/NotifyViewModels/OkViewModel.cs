using MyNoteSample.Entities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyNoteSample.Web.Models.ViewModels.NotifyViewModels
{
    public class OkViewModel : NotifyViewModelBase<string>
    {
        public OkViewModel()
        {
            Title = "İşlem Başarılı..";
            RedirectingTimeOut = 3000;
        }
    }
}