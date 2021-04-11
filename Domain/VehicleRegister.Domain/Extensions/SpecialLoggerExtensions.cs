using System;
using System.Runtime.CompilerServices;
using VehicleRegister.Domain.Interfaces.Logger.Interface;

namespace VehicleRegister.Domain.Extensions
{
    public abstract class SpecialLoggerExtensions
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
        public void ErrorLog(Exception ex, string name) => _logger.LogError($"{Environment.NewLine} Class: {this.GetType().Name} {Environment.NewLine} Method: {name} {Environment.NewLine} Error:  resulted in: {ex.Message}.");
        public void LogGettingInfo(string name) => _logger.LogInfo($"{Environment.NewLine} Class: {this.GetType().Name} {Environment.NewLine} Method: {name}{Environment.NewLine} Info: Successfully fetched from database");
        public void LogSuccessInfo(string name) => _logger.LogInfo($"{Environment.NewLine} Class: {this.GetType().Name} {Environment.NewLine} Method: {name}{Environment.NewLine} Info: was successfull");



        /// <summary>
        /// Logging for Services
        /// </summary>

        public void LogInfo(string name, string info) => _logger.LogInfo($"{Environment.NewLine} Class: {this.GetType().Name} {Environment.NewLine} Method: {name}{Environment.NewLine} Info: {info}");
        public void LogError(string name, string error) => _logger.LogError($"{Environment.NewLine}Class: {this.GetType().Name} {Environment.NewLine} Method: {name} {Environment.NewLine} Error: {error}");
    }
}