using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyNoteSample.Entities.ValueObject
{
    public class LoginViewModel
    {
        [DisplayName("Kullancı Adı"), Required(ErrorMessage = "{0} Alan boş geçilemez."), StringLength(10, ErrorMessage = "{0} max. {1} karakter olmalı")]
        public string UserName { get; set; }
        [DisplayName("Şifre"), Required(ErrorMessage = "{0} Alan boş geçilemez."), DataType(DataType.Password), StringLength(10, ErrorMessage = "{0} max. {1} karakter olmalı")]
        public string Password { get; set; }
    }
}