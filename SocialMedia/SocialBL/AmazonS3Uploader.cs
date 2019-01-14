using System;
using Amazon.S3;
using Amazon.S3.Model;

namespace SocialBL
{
    public class AmazonS3Uploader
    {
        private string bucketName = "myselabucket";
        private string keyName = "s2Test.txt";
        private string filePath = "D:\\Documents\\Sela\\Project 2 - Social\\s2Test.txt";
        IAmazonS3 client;

        public AmazonS3Uploader()
        {
            client = new AmazonS3Client(Amazon.RegionEndpoint.EUCentral1);
        }

        public void UploadFile()
        {
            try
            {
                var putRequest = new PutObjectRequest
                {
                    BucketName = bucketName,
                    Key = keyName,
                    FilePath = filePath,
                    ContentType = "text/plain"
                };

                PutObjectResponse response = client.PutObject(putRequest);
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
        }
    }

}
