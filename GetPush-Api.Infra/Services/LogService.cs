using GetPush_Api.Domain.Services;
using GetPush_Api.Infra.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetPush_Api.Infra.Services
{
    public class LogService : ILogService
    {
        public void Debug(string message)
        {
            Log.Instance.Debug(message);
        }

        public void ErrorException(string message, Exception ex)
        {
            Log.Instance.Error(ex, message);
        }

        public void Info(string message)
        {
            Log.Instance.Info(message);
        }

        public void Warn(string message)
        {
            Log.Instance.Warn(message);
        }
    }
}
