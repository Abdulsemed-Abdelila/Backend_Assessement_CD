using SparkTank.Application.Persistence.Contracts.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkTank.Infrastructure.services;

public class StringValidator : IStringValidator
{
    public string GetLastIndexValue(string value)
    {
        string[] arr = value.Split("/");
        string[] lastIndex = arr[arr.Length - 1].Split(".");
        return lastIndex[0];
    }

    public string TrimFromEnd(string value, string Identifier)
    {
        var index = value.LastIndexOf(Identifier);
        var stringBeforeIdentifier = value.Substring(0, index+1);
        return stringBeforeIdentifier;

    }
    public string TrimFromStart(string value, string Identifier)
    {
        var index = value.IndexOf(Identifier);
        var stringAfterIdentifier = value.Substring(index);
        return stringAfterIdentifier;
    }


}
