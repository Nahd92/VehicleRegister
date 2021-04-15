using System;
using System.Runtime.CompilerServices;
using VehicleRegister.Domain.Interfaces.Extensions.Interface;
using VehicleRegister.Domain.Interfaces.Logger.Interface;

namespace VehicleRegister.Domain.Extensions
{
    public class SpecialLoggerExtensions : ISpecialLoggerExtension
    {
        private ILoggerManager _logger;

        public SpecialLoggerExtensions(ILoggerManager logger)
        {
            _logger = logger;
        }
        public static string GetActualAsyncMethodName([CallerMemberName] string name = null) => name;

        /// <summary>
        ///  Loggin for database
        /// </summary>
        public void ErrorLog(string className, Exception ex, string name) => 
            _logger.LogError($"{Environment.NewLine} Class: {className} {Environment.NewLine} Method: {name} {Environment.NewLine} Error:  resulted in: {ex.Message}.");
        public void LogGettingInfo(string className, string name) => _logger.LogInfo($"{Environment.NewLine} Class: {className} {Environment.NewLine} Method: {name}{Environment.NewLine} Info: Successfully fetched from database");
        public void LogSuccessInfo(string className, string name) => _logger.LogInfo($"{Environment.NewLine} Class: {className} {Environment.NewLine} Method: {name}{Environment.NewLine} Info: was successfull");



        /// <summary>
        /// Logging for Services
        /// </summary>

        public void LogInfo(string className, string name, string info) => _logger.LogInfo($"{Environment.NewLine} Class: {className} {Environment.NewLine} Method: {name}{Environment.NewLine} Info: {info}");
        public void LogError(string className, Exception ex, string name) => _logger.LogError($"{Environment.NewLine}Class: {className} {Environment.NewLine} Method: {name} {Environment.NewLine} Error: {ex}");
    }
}