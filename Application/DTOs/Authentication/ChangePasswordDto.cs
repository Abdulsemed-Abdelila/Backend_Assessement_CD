using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SparkTank.Domain.Entities.Common;

namespace SparkTank.Application.DTOs.Authentication
{

    public class ChangePasswordDto
    {
        public string NewPassword { get; set; }
        public string OldPassword { get; set; }
    }
}
