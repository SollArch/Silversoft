using Core.Entities.Abstract;

namespace Entities.DTO.Post.CloudinaryConnection;

public class AddCloudinaryConnectionDto : IDto
{
    public string ApiKey { get; set; }
    public string ApiSecret { get; set; }
    public string Cloud { get; set; }
}