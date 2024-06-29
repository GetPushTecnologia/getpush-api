using System;

namespace GetPush_Api.Domain.Services
{
    public interface ILogService
    {
        void Debug(string message);
        void ErrorException(string message, Exception ex);
        void Info(string message);
        void Warn(string message);
    }
}
