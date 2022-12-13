﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Project.MVCUI.Tools
{
    public class ImageUploader
    {
        public static string UploadImage(string serverPath, HttpPostedFileBase file, string name)
        {
            if (file != null)
            {
                Guid uniqueName = Guid.NewGuid();

                string[] fileArray = file.FileName.Split('.'); //burada Split metodu sayesinde ilgili yapının uzantısının da iceride bulundugu bir string dizisi almıs olduk...Split metodu belirttiginiz char karakterinden metni bölerek size bir array sunar...

                string extension = fileArray[fileArray.Length - 1].ToLower(); //dosya uzantısını yakalayarak kücük harflere cevirdik...

                string fileName = $"{uniqueName}.{name}.{extension}"; //normal şartlarda biz burada Guid kullandıgımız icin asla bir dosya ismi aynı olmayacaktır... Lakin siz Guid kullanmazsanız(sadece kullanıcıya yüklemek istedigi dosyanın ismini girdirmek isterseniz) Böyle bir durumda aynı isimde dosya upload'u mümkün hale gelecektir...Dolayısıyla öyle bir durumda ek olarak bir kontrol yapmanız gerekir...Tabii ki böyle bir senaryo olsun veya olmasın önce extension kontrol edilmelidir...Ek kontrol daha sonra yapılmalıdır...

                if (extension == "jpg" || extension == "gif" || extension == "png")
                {
                    //Eger dosya ismi zaten varsa
                    if (File.Exists(HttpContext.Current.Server.MapPath(serverPath + fileName)))
                    {
                        return "1"; //Ancak Guid kullandıgımız icin bu acıdan zaten güvendeyiz...(Dosya zaten var kodu)
                    }
                    else
                    {
                        string filePath = HttpContext.Current.Server.MapPath(serverPath + fileName);
                        file.SaveAs(filePath);
                        return serverPath + fileName;

                    }
                }
                else
                {
                    return "2";//Secilen dosya bir resim degildir kodu
                }




            }
            else
            {
                return "3"; //Dosya bos kodu
            }
        }




    }
}