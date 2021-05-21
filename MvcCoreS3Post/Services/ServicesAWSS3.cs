using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MvcCoreS3Post.Services
{
    public class ServicesAWSS3
    {
        private String bucketName;
        private IAmazonS3 awsclient;

        public ServicesAWSS3(IAmazonS3 amazonS3, IConfiguration configuration)
        {
            this.awsclient = amazonS3;
            this.bucketName = configuration["AWSS3:BucktName"];
        }

        public async Task<List<string>> GetAllFiles()
        {
            ListVersionsResponse listVersions = await this.awsclient.ListVersionsAsync(this.bucketName);

            return listVersions.Versions.Select(x => x.Key).ToList();
        }

        public async Task<Stream> GetFile(String filename)
        {
            GetObjectResponse response = await this.awsclient.GetObjectAsync(this.bucketName, filename);

            if (response.HttpStatusCode == HttpStatusCode.OK)
            {
                return response.ResponseStream;
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> UploadFileinBucket(Stream stream, String filename)
        {
            PutObjectRequest request = new PutObjectRequest()
            {
                InputStream = stream,
                 Key = filename,
                BucketName = this.bucketName
            };
            
            PutObjectResponse response = await this.awsclient.PutObjectAsync(request);
            if (response.HttpStatusCode == HttpStatusCode.OK)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> UploadFileChangeName(Stream stream, String filename)
        {
            String namechange = "prueba" + filename;
             PutObjectRequest request = new PutObjectRequest()
            {
                InputStream = stream,
                Key = namechange,
                BucketName = this.bucketName
            };

            PutObjectResponse response = await this.awsclient.PutObjectAsync(request);
            if (response.HttpStatusCode == HttpStatusCode.OK)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<bool> UploadFileinDirectoryBucket(Stream stream, String filename)
        {
             String fileinDirectory = String.Format("{0}/{1}", "directorio1", filename);
            PutObjectRequest request = new PutObjectRequest()
            {
                InputStream = stream,
                Key = fileinDirectory,
                BucketName = this.bucketName
            };

            PutObjectResponse response = await this.awsclient.PutObjectAsync(request);
            if (response.HttpStatusCode == HttpStatusCode.OK)
            {
                return true;
            }
            else
            {
                return false;
            }
        } 
    }
}
