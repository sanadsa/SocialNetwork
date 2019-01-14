using System;
using System.Configuration;
using System.IO;
using System.Text.RegularExpressions;
using Amazon.S3;
using Amazon.S3.Model;

namespace SocialBL
{
    public class AmazonS3Uploader
    {
        private string keyName = "Boxing.jpg";
        private string filePath = @"C:\Users\Sanad\Pictures\Boxing.jpg";
        private string bucketName = "myselabucket";
        static readonly string bucketUrl = ConfigurationManager.AppSettings["s3Key"];
        IAmazonS3 client;

        public AmazonS3Uploader()
        {
            client = new AmazonS3Client(Amazon.RegionEndpoint.EUCentral1);
        }

        public string UploadFile()
        {
            try
            {
                //string format = Regex.Match(image, @"^data:image\/([a-zA-Z]+);").Groups[1].Value;
                //string result = Regex.Replace(image, @"^data:image\/[a-zA-Z]+;base64,", String.Empty);
                //byte[] bytes = Convert.FromBase64String(result);
                //string key = guid + "." + format;
                byte[] imageData = File.ReadAllBytes(filePath);
                using (client)
                {
                    var putRequest = new PutObjectRequest();
                    putRequest.BucketName = bucketName;
                    putRequest.Key = keyName;

                    using (var ms = new MemoryStream(imageData))
                    {
                        putRequest.InputStream = ms;
                        var response = client.PutObject(putRequest);
                    }
                }
                return "f";
            }
            catch (AmazonS3Exception amazonS3Exception)
            {
                if (amazonS3Exception.ErrorCode != null &&
                    (amazonS3Exception.ErrorCode.Equals("InvalidAccessKeyId")
                    ||
                    amazonS3Exception.ErrorCode.Equals("InvalidSecurity")))
                {
                    throw new Exception("Check the provided AWS Credentials.");
                }
                else
                {
                    throw new Exception("Error occurred: " + amazonS3Exception.Message);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error while uploading image to aws s3 " + ex.Message);
            }
        }
    }

}
