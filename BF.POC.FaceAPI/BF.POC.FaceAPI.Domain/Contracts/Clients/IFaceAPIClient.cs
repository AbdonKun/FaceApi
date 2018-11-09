using Microsoft.ProjectOxford.Face.Contract;
using System;
using System.Threading.Tasks;

namespace BF.POC.FaceAPI.Domain.Contracts.Clients
{
    public interface IFaceAPIClient
    {
        Task<bool> GroupExistsAsync(string code);

        Task GroupCreateAsync(string code, string name);

        Task GroupUpdateAsync(string code, string name);

        Task GroupTrainAsync(string code);

        Task<Guid> PersonCreateAsync(string groupCode, string personName);

        Task PersonUpdateAsync(string groupCode, Guid personId, string personName);

        Task<Guid> FaceAddAsync(string groupCode, Guid personId, byte[] image);

        Task<Face[]> FaceDetectAsync(byte[] image);

        Task<Face[]> FaceCountFacesAsync(byte[] image);

        Task<IdentifyResult[]> FaceIdentifyFacesAsync(string groupCode, Guid[] faceIDs);

    }
}
