using EIST.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace EIST.Services
{
    public class MailerService
    {
        private bool _mailer;

        //public CompanyService CompanyService { get; }
        //public Company Company { get; }

        public MailerService()
        {
            _mailer = true;
            //CompanyService = new CompanyService();
        }

        public bool SendIssueApprovalMail(string to, string subject, string mailBody, string url, string userFullName)
        {
            var sent = false;
            if (_mailer)
            {
                var bodyString = "<tr>" +
                                     "<td style = 'background-color:#005daa;height:50px;font-size:30px;color:#fff;'>Approval For Issue</td>" +
                                "</tr>" +
                              "<tr> " +
                                    "<td style = 'text-align:left;'>" +
                                        "<div style = 'margin-left:10px;color:black;'> " +
                                                //"<div> </div> " +
                                                "<div style='margin:10px 0 20px 0'><font size = '2' ><span style='font-size:10pt'> Dear " + userFullName + ",</span></font></div>" +
                                                //"<div> </div>" +
                                                "<div><font face = 'Calibri' size = '2'><span style = 'font-size:10pt'> Issue approval has been pending at your end in EIST Software and requires your attention.</span></font></div>" +
                                                  "<div><font face = 'Calibri' size = '2'><span style = 'font-size:10pt'> Please review this issue request and choose to approve, reject or withhold it and assign developer.</span></font></div>" +
                                                   "<div><font face = 'Calibri' size = '2'><span style = 'font-size:10pt'> &nbsp;</span></font><div>" +
                                                   "<div><font size = '2' color = 'red'><span style = 'font-size:10pt'><b> N.B.This email is system generated.</b></span></font></div>" +
                                                   "<div> &nbsp;</div> " +
                                           "</div> " +
                                       "</td> " +
                                "</tr> " +
                                "<tr>" +
                                    "<td  style='height:50px;'> " +
                                             "<a href='" + url + "' style = 'text-decoration: none;margin:10px 0px 30px 0px;border-radius:4px;padding:10px 20px;border: 0;color:#fff;background-color:#005daa;'>Click For Approval</a>" +
                                    "</td>" +
                                "</tr> ";

                var message = new MailMessage();
                message.To.Add(new MailAddress(to));
                message.Subject = subject;
                // Pdf Attachment //
                //message.Attachments.Add(pdfLink);
                sent = SendMail(bodyString, message);
            }
            return sent;
        }

        public bool SendMail(string body, MailMessage message)
        {


            var content = "<html>" +
                                "<head>" +
                                "</head>" +
                                "<body>" +
                                    "<table border='0' width='100%' style='margin:auto;padding:10px;background-color: #F3F3F3;border:1px solid #0C143B;'>" +
                                        "<tr>" +
                                            "<td>" +
                                                "<table border='0' width='100%'>" +
                                                    "<tr>" +
                                                        "<td style='text-align: center;'>" +
                                                            "<h1>" +
                                                                "<img src='cid:companylogo'  width='350px' height='80px'/> " +
                                                            "</h1>" +
                                                        "</td>" +
                                                    "</tr>" +
                                                "</table>" +
                                            "</td>" +
                                        "</tr>" +
                                        "<tr>" +
                                            "<td>" +
                                                "<table border='0' cellpadding='0' cellspacing='0' style='text-align:center;width:100%;background-color: #FFFF;'>" +
                                                body
                                                + "</table>" +
                                            "</td>" +
                                        "</tr>" +
                                        "<tr>" +
                                            "<td>" +
                                                "<table border='0' width='100%' style='border-radius: 5px;text-align: center;'>" +
                                                    "<tr>" +
                                                        "<td>" +
                                                            "<div style='margin-top: 20px;color:black;'>" +

                                                                "<span style='font-size:12px;'>Developed By - "+DefaultValue.DeveloperCompanyFullName+". © "+DateTime.Now.Year+"</span>" +
                                                            "</div>" +
                                                        "</td>" +
                                                    "</tr>" +
                                                "</table>" +
                                            "</td>" +
                                        "</tr>" +
                                    "</table>" +
                                "</body>" +
                                "</html>";


            AlternateView av1 = AlternateView.CreateAlternateViewFromString(content, null, MediaTypeNames.Text.Html);
            string logoPath = "";//Server.MapPath("~/Content/images/logo.gif"); // my logo is placed in images folder
            LinkedResource logo = new LinkedResource(logoPath);
            logo.ContentId = "companylogo";
            av1.LinkedResources.Add(logo);
            message.AlternateViews.Add(av1);

            message.IsBodyHtml = true;

            using (var smtp = new SmtpClient())
            {
                smtp.EnableSsl = true;
                smtp.Send(message);
                return true;
            }

        }

        public bool SendPIRejectedMailFromRAUser(string to, string subject, string mailBody, string url, string userFullName, string remarks, Attachment pdfLink)
        {
            var sent = false;
            if (_mailer)
            {
                var bodyString = "<tr>" +
                                     "<td style = 'background-color:#005daa;height:50px;font-size:30px;color:#fff;'>PI Rejected </td>" +
                                "</tr>" +
                              "<tr> " +
                                    "<td style = 'text-align:left;'>" +
                                        "<div style = 'margin-left:10px;color:black;'> " +
                                                //"<div> </div> " +
                                                "<div style='margin:10px 0 20px 0'><font size = '2' ><span style='font-size:10pt'> Dear " + userFullName + ",</span></font></div>" +
                                                //"<div> </div>" +
                                                "<div><font face = 'Calibri' size = '2'><span style = 'font-size:10pt'> PI has been rejected by admin for " + remarks + ". </span></font></div>" +
                                                   "<div><font face = 'Calibri' size = '2'><span style = 'font-size:10pt'> &nbsp;</span></font><div>" +
                                                   "<div><font size = '2' color = 'red'><span style = 'font-size:10pt'><b> N.B.This email is system generated.</b></span></font></div>" +
                                                   "<div> &nbsp;</div> " +
                                           "</div> " +
                                       "</td> " +
                                "</tr> " +
                                "<tr>" +
                                    "<td  style='height:50px;'> " +
                                             "<a href='" + url + "' style = 'text-decoration: none;margin:10px 0px 30px 0px;border-radius:4px;padding:10px 20px;border: 0;color:#fff;background-color:#005daa;'> Click Here For Details</a>" +
                                    "</td>" +
                                "</tr> ";

                var message = new MailMessage();
                message.To.Add(new MailAddress(to));
                message.Subject = subject;
                // Pdf Attachment //
                //message.Attachments.Add(pdfLink);
                sent = SendMail(bodyString, message);
            }
            return sent;
        }

        public bool SendIssueAcceptRejectMailFromManager(string toMail, string ccMail, string subject, string mailBody, string url, int status, string userFullName, string remarks)
        {
            var sent = false;
            if (_mailer)
            {
                var bodyString = "<tr>" +
                                     "<td style = 'background-color:#005daa;height:50px;font-size:30px;color:#fff;'>PI "+ (status == (int)EnumIssueStatus.Accepted ? "Approved" : "Rejected") + " </td>" +
                                "</tr>" +
                              "<tr> " +
                                    "<td style = 'text-align:left;'>" +
                                        "<div style = 'margin-left:10px;color:black;'> " +
                                                //"<div> </div> " +
                                                "<div style='margin:10px 0 20px 0'><font size = '2' ><span style='font-size:10pt'> Dear " + "sir" + ",</span></font></div>" +
                                                //"<div> </div>" +
                                                "<div><font face = 'Calibri' size = '2'><span style = 'font-size:10pt'> PI has been "+(status==(int)EnumIssueStatus.Accepted?"approved":"rejected")+" by " +userFullName+ (status == (int)EnumIssueStatus.Accepted ? "" : " for " + remarks) + ". </span></font></div>" +
                                                   "<div><font face = 'Calibri' size = '2'><span style = 'font-size:10pt'> &nbsp;</span></font><div>" +
                                                   "<div><font size = '2' color = 'red'><span style = 'font-size:10pt'><b> N.B.This email is system generated.</b></span></font></div>" +
                                                   "<div> &nbsp;</div> " +
                                           "</div> " +
                                       "</td> " +
                                "</tr> " +
                                "<tr>" +
                                    "<td  style='height:50px;'> " +
                                             "<a href='" + url + "' style = 'text-decoration: none;margin:10px 0px 30px 0px;border-radius:4px;padding:10px 20px;border: 0;color:#fff;background-color:#005daa;'> Click Here For Details</a>" +
                                    "</td>" +
                                "</tr> ";

                var message = new MailMessage();
                var toMails = toMail.Split(',').ToList();
                var ccMails = ccMail.Split(',').ToList();
                foreach (var to in toMails)
                {
                    message.To.Add(new MailAddress(to));
                }
                foreach (var cc in ccMails)
                {
                    message.CC.Add(new MailAddress(cc));
                }
                message.Subject = subject;
                // Pdf Attachment //
                //message.Attachments.Add(pdfLink);
                sent = SendMail(bodyString, message);
            }
            return sent;
        }
    }
}
