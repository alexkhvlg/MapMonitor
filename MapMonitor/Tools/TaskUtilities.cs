using System;
using System.ServiceModel.Dispatcher;
using System.Threading.Tasks;

namespace MapMonitor.Tools
{
    public static class TaskUtilities
    {
#pragma warning disable RECS0165 // Asynchronous methods should return a TaskInfo instead of void
        public static async void FireAndForgetSafeAsync(this Task task, IErrorHandler handler = null)
#pragma warning restore RECS0165 // Asynchronous methods should return a TaskInfo instead of void
        {
            try
            {
                await task;
            }
            catch (Exception ex)
            {
                handler?.HandleError(ex);
            }
        }
    }
}