using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyNoteSample.Web.Models.ViewModels.NotifyViewModels
{
    public class NotifyViewModelBase<T>
    {
        public List<T> Items { get; set; }
        public string Header { get; set; }
        public string Title { get; set; }
        public Boolean IsRedirecting { get; set; }
        public string RedirectingUrl { get; set; }
        public int RedirectingTimeOut { get; set; }
        public NotifyViewModelBase()
        {
            Header = "Yönlendiriliyorsunuz..";
            Title = "Geçersiz İşlem";
            IsRedirecting = true;
            RedirectingTimeOut = 3000;
            RedirectingUrl = "/Home/Index";
            Items = new List<T>();
        }
    }
}

