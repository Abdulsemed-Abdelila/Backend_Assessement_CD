using SparkTank.Application.Persistence.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkTank.Infrastructure.services;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime CreateTime => DateTime.Now;
}
