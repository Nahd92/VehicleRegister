using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace VehicleRegister.Domain.Interfaces.Extensions.Interface
{
    public interface ISpecialLoggerExtension
    {
        string GetActualAsyncMethodName([CallerMemberName] string name = null) => name;

         void ErrorLog(string className, Exception ex, string name);
         void LogGettingInfo(string className, string name);
         void LogSuccessInfo(string className, string name);
         void LogInfo(string className, string name, string info);
         void LogError(string className, Exception ex, string name);
    }
}
