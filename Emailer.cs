using System;
using System.Net;
using System.Net.Mail;
using EVEMon.Common;
using System.Text;

namespace EVEMon
{
    internal class Emailer
    {
        private Emailer()
        {
        }

        private string m_server;
        private ICredentialsByHost m_serverCredentials;
        private bool m_serverRequiresSsl = false;
        private string m_fromAddr;
        private string m_toAddr;
        private string m_subject;
        private string m_body;
        private int m_portNumber;

        public static bool SendTestMail(Settings settings)
        {
            return SendMail(settings,
                            "EVE Character Monitor Test Mail",
                            "This is a test email sent by EVE Character Monitor");
        }

        public static bool SendAlertMail(Settings settings, string skillName, string charName)
        {
            StringBuilder messageText = charName + " has finished training " + skillName + "\r\n\r\nNext skills listed in plans:\r\n\r\n";
            foreach (string planName in settings.GetPlansForCharacter(charName))
            {
                Plan p = settings.GetPlanByName(charName, planName);
                if (p.Entries.Count > 0)
                {
                    int i = 0;
                    while (i < p.Entries.Count && p.Entries[i].Skill.Known)
                    {
                        if (!p.Entries[i].Skill.Known)
                        {
                            messageText.Append(planName + ":\r\n\t" + p.Entries[i].SkillName + " " + p.Entries[i].Level + "\r\n\r\n");
                            i = p.Entries.Count;
                        }
                        i++;
                    }
                }
            }
            return SendMail(settings, charName + " skill " + skillName + " complete", messageText.ToString());
        }

        private static bool SendMail(Settings settings, string subject, string body)
        {
            Emailer m = new Emailer();
            m.m_server = settings.EmailServer;
            m.m_fromAddr = settings.EmailFromAddress;
            m.m_toAddr = settings.EmailToAddress;
            m.m_subject = subject;
            m.m_body = body;
            m.m_portNumber = settings.PortNumber;
            if (settings.EmailAuthRequired)
            {
                m.m_serverCredentials = new NetworkCredential(settings.EmailAuthUsername, settings.EmailAuthPassword);
            }
            m.m_serverRequiresSsl = settings.EmailServerRequiresSsl;
            if (settings.PortNumber > 0)
                return m.Send(settings.PortNumber);
            else
                return m.Send();
        }

        private bool Send()
        {
            try
            {
                MailMessage msg = new MailMessage();
                msg.From = new MailAddress(m_fromAddr);
                msg.To.Add(m_toAddr);
                msg.Subject = m_subject;
                msg.Body = m_body;
                SmtpClient cli = new SmtpClient(m_server);
                if (m_serverCredentials != null)
                {
                    cli.Credentials = m_serverCredentials;
                }
                cli.EnableSsl = m_serverRequiresSsl;
                cli.Send(msg);
                return true;
            }
            catch (Exception e)
            {
                ExceptionHandler.LogException(e, true);
                return false;
            }
        }

        private bool Send(int PortNumber)
        {
            try
            {
                MailMessage msg = new MailMessage();
                msg.From = new MailAddress(m_fromAddr);
                msg.To.Add(m_toAddr);
                msg.Subject = m_subject;
                msg.Body = m_body;
                SmtpClient client = new SmtpClient(m_server, m_portNumber);

                if (m_serverCredentials != null)
                {
                    client.Credentials = m_serverCredentials;
                }
                client.EnableSsl = m_serverRequiresSsl;
                client.Send(msg);
                return true;
            }
            catch (Exception e)
            {
                ExceptionHandler.LogException(e, true);
                return false;
            }
        }
    }
}
