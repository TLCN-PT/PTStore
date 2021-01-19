using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace PTStore.Common.TienIch
{
    public static class TIMail
    {
        public static int idienthoai { get; set; }
        public static void GuiMailMaXacNhan(string email, string mxn)
        {
            MailMessage mail = new MailMessage();
            mail.To.Add(email);
            mail.From = new MailAddress("ptstore.hcmute@gmail.com");
            mail.Subject = "Lấy lại mật khẩu !!!";
            string body = @"Yêu cầu cấp lại mật khẩu của bạn thành công !!! <br />";
            //body += Environment.NewLine;
            body += @"Mã xác nhận của bạn là: <br />";
            //body += Environment.NewLine;
            body += @"<strong>" + mxn + "</strong><br />";
            //body += Environment.NewLine;
            body += @"Xin cảm ơn !!!";
            mail.Body = body;
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = Convert.ToInt16("587");
            smtp.Credentials = new NetworkCredential("ptstore.hcmute@gmail.com", "ptstore.2020");
            smtp.EnableSsl = true;
            smtp.Send(mail);
        }

        public static void GuiMailThongBaoDangKy(string email)
        {
            MailMessage mail = new MailMessage();
            mail.To.Add(email);
            mail.From = new MailAddress("ptstore.hcmute@gmail.com");
            mail.Subject = "Subscribe PTStore thành công !!!";
            string body = "<strong>Bạn đã đăng kí nhận thông tin qua email từ PTStore !</strong>";
            body += Environment.NewLine;
            body += "Hãy kiểm tra email thường xuyên để cập nhật thông tin về sản phẩm cũng như các chế độ ưu đãi của chúng tôi!";
            body += Environment.NewLine;
            body += "Xin cảm ơn !!!";
            mail.Body = body;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = Convert.ToInt16("587");
            smtp.Credentials = new NetworkCredential("ptstore.hcmute@gmail.com", "ptstore.2020");
            smtp.EnableSsl = true;
            smtp.Send(mail);
        }

        public static void GuiMailDatHangThanhCong(string email)
        {
            MailMessage mail = new MailMessage();
            mail.To.Add(email);
            mail.From = new MailAddress("ptstore.hcmute@gmail.com");
            mail.Subject = "Subscribe PTStore thành công !!!";
            string body = "Bạn đã đăng kí nhận thông tin qua email từ PTStore !";
            body += Environment.NewLine;
            body += "Hãy kiểm tra email thường xuyên để cập nhật thông tin về sản phẩm cũng như các chế độ ưu đãi của chúng tôi!";
            body += Environment.NewLine;
            body += "Xin cảm ơn !!!";
            mail.Body = body;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = Convert.ToInt16("587");
            smtp.Credentials = new NetworkCredential("ptstore.hcmute@gmail.com", "ptstore.2020");
            smtp.EnableSsl = true;
            smtp.Send(mail);
        }
    }
}