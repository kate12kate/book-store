using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EBook.Service.Interface
{
    public interface IBackgroundEmailSender
    {
        Task DoWork();
    }
}