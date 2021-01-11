using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace PTStore.Common.TienIch
{
    public static class TIMail
    {
        public static bool SendMail(string _from, string _to, string _subject, string _body, SmtpClient client)
        {
            // Tạo nội dung Email
            MailMessage message = new MailMessage(
                from: _from,
                to: _to,
                subject: _subject,
                body: _body
            );
            message.BodyEncoding = System.Text.Encoding.UTF8;
            message.SubjectEncoding = System.Text.Encoding.UTF8;
            message.IsBodyHtml = true;
            message.ReplyToList.Add(new MailAddress(_from));
            message.Sender = new MailAddress(_from);

            try
            {
                client.SendMailAsync(message);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public static bool SendMailGoogleSmtp(string _to, string _subject, string _body)
        {
            string _gmailsend = "ptstore.hcmute@gmail.com";
            string _gmailpassword = "ptstore.2020";
            string _from = "ptstore.hcmute@gmail.com";
            MailMessage message = new MailMessage(
                from: _from,
                to: _to,
                subject: _subject,
                body: _body
            );
            message.BodyEncoding = System.Text.Encoding.UTF8;
            message.SubjectEncoding = System.Text.Encoding.UTF8;
            message.IsBodyHtml = true;
            message.ReplyToList.Add(new MailAddress(_from));
            message.Sender = new MailAddress(_from);

            // Tạo SmtpClient kết nối đến smtp.gmail.com
            using (SmtpClient client = new SmtpClient("smtp.gmail.com"))
            {
                client.Port = 587;
                client.Credentials = new NetworkCredential(_gmailsend, _gmailpassword);
                client.EnableSsl = true;
                return SendMail(_from, _to, _subject, _body, client);
            }

        }

        public static bool GuiMailDoiMatKhau(string _to, string maxacnhan)
        {
            string _subject = "Mã xác nhận thay đổi mật khẩu từ PTStore";
            string _body = "<p>Mã xác nhận của bạn là:</p><h1><strong>" + maxacnhan + "</strong></h1>";
            return SendMailGoogleSmtp(_to, _subject, _body);
        }

        public static bool GuiMailDangKy(string _to)
        {
            string _subject = "Đăng ký nhận tin tức từ PTStore thành công!";
            string _body = "<p>Cảm ơn bạn đã đăng ký cập nhật thông tin từ website của chúng tôi!</p>";
            return SendMailGoogleSmtp(_to, _subject, _body);
        }

        public static bool GuiMailDatHangThanhCong(string _to)
        {
            string _subject = "Đăng ký nhận tin tức từ PTStore thành công!";
            string _body = "<p>Đặt hàng thành công!</p><p>Để xem trạng thái đơn hàng, vui lòng vào trang thái khoản, chọn lịch sử mua hàng!<p>Xin cảm ơn!</p>";
            return SendMailGoogleSmtp(_to, _subject, _body);
        }

    }
}