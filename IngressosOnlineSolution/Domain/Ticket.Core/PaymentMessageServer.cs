﻿using System;
using System.Net.Mail;

namespace Ticket.Core
{
    internal class PaymentMessageServer : IObserver<PaymentMessage>
    {
        private SmtpClient _smtp;

        private SmtpClient Smtp => _smtp ?? (_smtp = new SmtpClient());

        public void OnCompleted()
        {
        }

        public void OnError(Exception erro)
        {
            erro.LogAndThrow();
        }

        public async void OnNext(PaymentMessage message)
        {
            if (message.Email == null) return;
            var mail = new MailMessage("gag@gustavoamerico.net", message.Email) { Body = message.ToString() };
            await Smtp.SendMailAsync(mail);
        }
    }
}