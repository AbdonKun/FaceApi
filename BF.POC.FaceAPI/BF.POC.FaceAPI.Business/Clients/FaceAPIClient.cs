using BF.POC.FaceAPI.Domain.Contracts.Clients;
using Microsoft.ProjectOxford.Face;
using Microsoft.ProjectOxford.Face.Contract;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Threading.Tasks;

namespace BF.POC.FaceAPI.Business.Clients
{
    public class FaceAPIClient : IFaceAPIClient
    {
        protected readonly IFaceServiceClient faceServiceClient;

        protected readonly IEnumerable<FaceAttributeType> faceAttributes = new FaceAttributeType[]
        {
            FaceAttributeType.Gender,
            FaceAttributeType.Age,
            FaceAttributeType.HeadPose,
            FaceAttributeType.Smile,
            FaceAttributeType.FacialHair,
            FaceAttributeType.Glasses,
            FaceAttributeType.Emotion,
            FaceAttributeType.Hair,
            FaceAttributeType.Makeup,
            FaceAttributeType.Occlusion,
            FaceAttributeType.Accessories,
            FaceAttributeType.Blur,
            FaceAttributeType.Exposure,
            FaceAttributeType.Noise
        };

        public FaceAPIClient()
        {
            var subscriptionKey = ConfigurationManager.AppSettings["FaceApiSubscriptionKey"];
            var endpoint = ConfigurationManager.AppSettings["FaceApiEndpoint"];

            // ToDo: initialize faceServiceClient object here...
        }

        #region - Group Managment -

        public async Task<bool> GroupExistsAsync(string code)
        {
            try
            {
                // ToDo: Implement faceServiceClient.GetPersonGroupAsync() here...
                throw new NotImplementedException();
            }
            catch (FaceAPIException ex)
            {
                if (ex.ErrorCode == "PersonGroupNotFound")
                {
                    return false;
                }
                throw;
            }
        }

        public async Task GroupCreateAsync(string code, string name)
        {
            // ToDo: Implement faceServiceClient.CreatePersonGroupAsync() here...
            throw new NotImplementedException();
        }

        public async Task GroupUpdateAsync(string code, string name)
        {
            // ToDo: Implement faceServiceClient.UpdatePersonGroupAsync() here...
            throw new NotImplementedException();
        }

        public async Task GroupTrainAsync(string code)
        {
            // ToDo: Implement faceServiceClient.TrainPersonGroupAsync() here...
            throw new NotImplementedException();
        }

        #endregion - Group Managment -

        #region - Person Management -

        public async Task<Guid> PersonCreateAsync(string groupCode, string personName)
        {
            // ToDo: Implement faceServiceClient.CreatePersonInPersonGroupAsync() here...
            throw new NotImplementedException();
        }

        public async Task PersonUpdateAsync(string groupCode, Guid personId, string personName)
        {
            // ToDo: Implement faceServiceClient.UpdatePersonInPersonGroupAsync() here...
            throw new NotImplementedException();
        }

        #endregion - Person Management -

        #region - Face Management -

        public async Task<Guid> FaceAddAsync(string groupCode, Guid personId, byte[] image)
        {
            var stream = new MemoryStream(image);
            
            // ToDo: Implement faceServiceClient.AddPersonFaceInPersonGroupAsync() here...
            throw new NotImplementedException();
        }

        public async Task<Face[]> FaceDetectAsync(byte[] image)
        {
            var stream = new MemoryStream(image);

            // ToDo: Implement faceServiceClient.DetectAsync() here...
            throw new NotImplementedException();
        }

        public async Task<Face[]> FaceCountFacesAsync(byte[] image)
        {
            var stream = new MemoryStream(image);

            // ToDo: Implement faceServiceClient.DetectAsync() here...
            throw new NotImplementedException();
        }

        public async Task<IdentifyResult[]> FaceIdentifyFacesAsync(string groupCode, Guid[] faceIDs)
        {
            string largePersonGroupId = null;
            var confidenceThreshold = (float)0.65;
            var maxNumOfCandidatesReturned = 1;

            // ToDo: Implement faceServiceClient.IdentifyAsync() here...
            throw new NotImplementedException();
        }

        #endregion - Face Management -
    }
}
