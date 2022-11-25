using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Email
{
    public interface IEmailService
    {
        void SendEmailToUser(String userEmail, String subject, String header, String body);

    }
}
