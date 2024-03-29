using MarketingEvent.Database.Attachments.Dtos;
using MarketingEvent.Database.Attachments.Entities;
using System.Web;

namespace MarketingEvent.Database.Attachments.Mappers
{
    public class AttachmentMapper
    {
        public AttachmentMapper() { }

        public AttachmentDto ToDto(Attachment entity)
        {
            return new AttachmentDto()
            {
                Id = entity.Id,
                Description = entity.Description,
                CreatedDate = entity.CreatedDate,
                URL = "/cdn/" + HttpUtility.UrlEncode(entity.Path.Replace(Directory.GetCurrentDirectory(), ""))
            };
        }
    }
}
