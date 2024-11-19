using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Domain.Interfaces
{
    public interface INotificationService
    {
        Task SendNotificationAsync(string title, string message, string token);

    }
}
