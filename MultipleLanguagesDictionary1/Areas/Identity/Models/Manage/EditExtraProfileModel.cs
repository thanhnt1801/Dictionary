using System;
using System.ComponentModel.DataAnnotations;

namespace App.Areas.Identity.Models.ManageViewModels
{
  public class EditExtraProfileModel
  {
      [Display(Name = "Tên tài khoản")]
      public string UserName { get; set; }

      [Display(Name = "Địa chỉ email")]
      public string UserEmail { get; set; }
      [Display(Name = "Số điện thoại")]
      public string PhoneNumber { get; set; }
  }
}