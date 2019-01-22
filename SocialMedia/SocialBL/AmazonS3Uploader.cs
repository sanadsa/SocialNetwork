using System;
using System.Configuration;
using System.IO;
using System.Text.RegularExpressions;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;

namespace SocialBL
{
    public class AmazonS3Uploader
    {
        //private string keyName = "Statham.png";
        private string keyName = "instabook";
        private string bucketName = "myselabucket";
        readonly string bucketUrl;
        private string hostUrl = "https://s3.eu-central-1.amazonaws.com";
        IAmazonS3 s3client;

        public AmazonS3Uploader()
        {
            s3client = new AmazonS3Client(Amazon.RegionEndpoint.EUCentral1);
            bucketUrl = ConfigurationManager.AppSettings["s3Key"];
        }

        public string UploadFile(byte[] imageFile, string guid)
        {
            var image = new MemoryStream(imageFile);
            var fileName = $"{keyName}/{DateTime.Now.ToString()}.png";
            var fileTransferUtility = new TransferUtility(s3client);

            try
            {
                fileTransferUtility.Upload(stream: image, bucketName: bucketName, key: fileName);

                fileTransferUtility.S3Client.PutACL(new PutACLRequest
                {
                    CannedACL = S3CannedACL.PublicReadWrite,
                    BucketName = bucketName,
                    Key = fileName
                });

                var URL = s3client.GetPreSignedURL(new GetPreSignedUrlRequest
                {
                    BucketName = bucketName,
                    Key = fileName,
                    Expires = DateTime.MaxValue
                });

                var finalURL = URL.Split('?');
                return finalURL[0];
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
