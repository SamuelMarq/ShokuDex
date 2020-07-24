using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Recodme.ShokuDex.Data.UserInfo
{
    public class User : IdentityUser<int>
    {
        [ForeignKey("Profiles")]
        public Guid ProfileId { get; set; }
        public virtual Profiles Profiles { get; set; }
    }
}