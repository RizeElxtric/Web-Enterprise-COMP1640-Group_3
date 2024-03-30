using MarketingEvent.Database.Attachments.Mappers;
using MarketingEvent.Database.Authentication.Mappers;
using MarketingEvent.Database.Comments.Mappers;
using MarketingEvent.Database.Events.Mappers;
using MarketingEvent.Database.Submissions.Command;
using MarketingEvent.Database.Submissions.Dtos;
using MarketingEvent.Database.Submissions.Entities;
using System.Web;

namespace MarketingEvent.Database.Submissions.Mappers
{
    public class SubmissionMapper
    {
        private readonly UserMapper _userMapper;
        private readonly AttachmentMapper _attachmentMapper;
        private readonly CommentMapper _commentMapper;
        private readonly EventMapper _eventMapper;

        public SubmissionMapper(
            UserMapper userMapper,
            AttachmentMapper attachmentMapper,
            CommentMapper commentMapper,
            EventMapper eventMapper
            )
        {
            _userMapper = userMapper;
            _attachmentMapper = attachmentMapper;
            _commentMapper = commentMapper;
            _eventMapper = eventMapper;
        }

        public async Task<Submission> ToEntityAsync(SubmitSubmissionCommand command, Guid currentUser)
        {
            return new Submission()
            {
                Id = Guid.NewGuid(),
                Title = command.Title,
                EventId = command.EventId,
                CreatedById = currentUser
            };
        }

        public SubmissionDto ToDto(Submission entity)
        {
            return new SubmissionDto()
            {
                Id = entity.Id,
                Title = entity.Title,
                URL = "/cdn/" + HttpUtility.UrlEncode(entity.Path.Replace(Directory.GetCurrentDirectory(), "")),
                CreatedDate = entity.CreatedDate,
                IsPublicized = entity.IsPublicized,
                Event = _eventMapper.ToDto(entity.Event),
                CreatedBy = _userMapper.ToDto(entity.CreatedBy),
                Attachments = entity.Attachments.Select(x => _attachmentMapper.ToDto(x)).ToList(),
                Comments = entity.Comments.Select(x => _commentMapper.ToDto(x)).ToList(),
            };
        }
    }
}
