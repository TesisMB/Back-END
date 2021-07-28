using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts.Interfaces
{
    public interface IEmailRepository
    {
        void Send(string to, string subject, string html, string from = null);
    }
}
