using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MvcCoreS3Post.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreS3Post.Controllers
{
    public class UploadFileController : Controller
    {
        private ServicesAWSS3 servicesAWSS3;

        public UploadFileController(ServicesAWSS3 servicesAWSS)
        {
            this.servicesAWSS3 = servicesAWSS;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> ListFiles()
        {
            List<String> files = await this.servicesAWSS3.GetAllFiles();

            return View(files);
        }

        public IActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            using (MemoryStream m = new MemoryStream())
            {
                file.CopyTo(m);
                await this.servicesAWSS3.UploadFileinBucket(m, file.FileName);
            }

            return RedirectToAction("Index");
        }

        public IActionResult UploadChangeName()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UploadChangeName(IFormFile file)
        {
            using (MemoryStream m = new MemoryStream())
            {
                file.CopyTo(m);
                await this.servicesAWSS3.UploadFileChangeName(m, file.FileName);
            }

            return RedirectToAction("Index");
        }

        public IActionResult UploadDirectory()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UploadDirectory(IFormFile file)
        {
            using (MemoryStream m = new MemoryStream())
            {
                file.CopyTo(m);
                await this.servicesAWSS3.UploadFileinDirectoryBucket(m, file.FileName);
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> FileAWS(string filename)
        {
            Stream stream = await this.servicesAWSS3.GetFile(filename);
            return File(stream, "image/jpg");
        }

        public IActionResult FileImg(string filename)
        {
            ViewData["img"] = "https://uploadfile-bucket-ans.s3.amazonaws.com/" + filename;
            return View();
        }

      
    }
}
