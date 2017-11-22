using Microsoft.AspNet.Identity;
using SendGrid;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace WebAppProject.Services
{
    /*
     * CLASSE ANALISADA DO EXEMPLO:
     * https://docs.microsoft.com/en-us/aspnet/identity/overview/features-api/account-confirmation-and-password-recovery-with-aspnet-identity
     * E AJUSTADA DE ACORDO:
     * http://bitoftech.net/2015/02/03/asp-net-identity-2-accounts-confirmation-password-user-policy-configuration/
     * 
     * SERVE PARA EXECUTAR O SERVIÇO DE E-MAIL
     */

    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Conecte o seu serviço de email aqui para enviar um email
            return ConfigSendGridasync(message);
        }

        public Task ConfigSendGridasync(IdentityMessage message)
        {
            var myMessage = new SendGridMessage();
            myMessage.AddTo(message.Destination);
            myMessage.From = new MailAddress(
                                ConfigurationManager.AppSettings["mailAccount"],
                                ConfigurationManager.AppSettings["userName"]);
            myMessage.Subject = message.Subject;
            myMessage.Text = message.Body;
            myMessage.Html = message.Body;

            var credentials = new NetworkCredential(
                       ConfigurationManager.AppSettings["mailAccount"],
                       ConfigurationManager.AppSettings["mailPassword"]
                       );

            // Create a Web transport for sending email.
            var transportWeb = new Web(credentials);

            // Send the email.
            if (transportWeb != null)
            {
                return transportWeb.DeliverAsync(myMessage);
            }
            else
            {
                return Task.FromResult(0);
            }
        }
    }
}