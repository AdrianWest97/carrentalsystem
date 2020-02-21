namespace CarRentalAdatSolutions.Models
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Net;
    using System.Net.Mail;

    /// <summary>
    /// Defines the <see cref="SendMail" />
    /// </summary>
    public class SendMail
    {
        /// <summary>
        /// The SendEmail
        /// </summary>
        /// <param name="user">The user<see cref="ApplicationUser"/></param>
        /// <param name="vehicle">The vehicle<see cref="Vehicle"/></param>
        /// <param name="booking">The booking<see cref="Booking"/></param>
        /// <returns>The <see cref="bool"/></returns>
        public bool SendEmail(ApplicationUser user, Vehicle vehicle, Booking booking)
        {
            ConvertToBase64 toBase64 = new ConvertToBase64();

            List<VehicleImage> imageList = vehicle.Images.ToList();

          
            String htmlBodyPart = "<html lang='en'>" +
    "<body>" +
        "<div class='conatiner' style='width: 500px; background-color: #f7f7f7;height: auto;padding: 10px'>" +
            "<header style = 'text-align: center;margin:10px '><h1 style='color:#ffd000'>ADAT CRS</h1></header>" +
           " <p>Booking reciept for Adrin west</p>" +
           " <p>Date: " + DateTime.Now.ToString("MMM dd, yyyy") + "</p>" +
           " <h1 style = 'text-align: center'>" + vehicle.make + "&nbsp;" + vehicle.model + "</h1>" +
               //"<img src='"+img+"' style='width:200px'/>"+
               " <p style = 'font-weight: bold'> Car rental information</p>" +
           " <table style = 'text-transform: uppercase'>" +

                  "  <tr>" +

                      " <th> Brand </th>" +

                     "  <td> " + vehicle.make + " </td>" +

                    "</tr> <tr> <th> Model </th>  <td> " + vehicle.model + " </td></tr><tr><th> Year </th> <td> " + vehicle.year + " </td> </tr>" +

                     "<tr><th> Daily rate</th><td>" + string.Format("{0:c}", Int32.Parse(vehicle.RentalPrice)) + "</td></tr><tr><th>Duration</th><td>" + booking.NumberOfDays + " days</td> </tr><tr><th>Tax applied</th>" +

                   "<td>" + string.Format("{0:c}", booking.Reciept.TAX) + "</td> </tr><br><tr><th><h2>Total charge</h2></th><td><h2 style = 'color:green'>" + string.Format("{0:c}", booking.Reciept.TotalCharges) + "</h2></td></tr><br>" +
            "</table>" +
            "<footer><p>Thank you for your business. Any queries can be addressed to this email<a href='mailto:westsparta@gmail.com'> westsparta@gmail.com</a>" +
              "</p>" +
               " <p>Phone: 1876 337-2750</p> </footer>" +
       " </div>" +
  "  </body>" +
"</html>";

            var senderEmail = new MailAddress(ConfigurationManager.AppSettings["Email"].ToString(), "ADAT CRS");
            var receiverEmail = new MailAddress(user.Email, "Reciever");
            var password = ConfigurationManager.AppSettings["Password"].ToString();
            var sub = "ADAT CRS Rental Reciept";
            var body = htmlBodyPart;
            try
            {
                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(senderEmail.Address, password),
                    Timeout = 3000,
                };
                using (var mess = new MailMessage(senderEmail, receiverEmail)
                {
                    IsBodyHtml = true,
                    Subject = sub,
                    Body = body
                })
                {
                    smtp.Send(mess);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
