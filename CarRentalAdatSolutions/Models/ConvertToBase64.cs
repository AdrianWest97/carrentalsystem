using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarRentalAdatSolutions.Models
{
    public class ConvertToBase64
    {
       public VehicleImage ConvertImageToByte(VehicleImage image, HttpPostedFileBase ImagePath)
        {
            if (image != null)
            {
                if (ImagePath.ContentLength > 0)
                {
                    image.ImagePath = new byte[ImagePath.ContentLength];
                    ImagePath.InputStream.Read(image.ImagePath, 0, ImagePath.ContentLength);
                }

                return image;
            }
            return null;

        }

        public string convertToBase64(byte[] image)
        {
            var base64 = Convert.ToBase64String(image);
            return String.Format("data:image/gif;base64,{0}", base64);

        }
    }
}