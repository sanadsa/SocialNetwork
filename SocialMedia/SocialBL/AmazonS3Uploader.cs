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
        private string bucketName = "myselabucket";
        readonly string bucketUrl;
        IAmazonS3 client;

        public AmazonS3Uploader()
        {
            client = new AmazonS3Client(Amazon.RegionEndpoint.EUCentral1);
            bucketUrl = ConfigurationManager.AppSettings["s3Key"];
        }

        public string UploadFile(byte[] imagePath, string guid)
        {
            try
            {
                string key = guid + ".";
                byte[] imageData = imagePath;
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
                return bucketUrl + key;
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
