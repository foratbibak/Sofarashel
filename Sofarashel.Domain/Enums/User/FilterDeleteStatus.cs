using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace User_Login.Enum.User
{
    public enum FilterDeleteStatus
    {
        [Display(Name ="حذف شده ها")] NotDeleted,
        [Display(Name ="همه")]All,
        [Display(Name ="حذف شده ها")] Deleted
    }
}
