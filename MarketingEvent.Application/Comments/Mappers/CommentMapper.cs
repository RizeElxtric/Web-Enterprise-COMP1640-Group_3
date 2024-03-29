using MarketingEvent.Database.Authentication.Mappers;
using MarketingEvent.Database.Comments.Dtos;
using MarketingEvent.Database.Comments.Entities;

namespace MarketingEvent.Database.Comments.Mappers
{
    public class CommentMapper
    {
        private readonly UserMapper _userMapper;

        public CommentMapper(UserMapper userMapper)
        {
            _userMapper = userMapper;
        }

        public CommentDto ToDto(Comment entity)
        {
            return new CommentDto()
            {
                Id = entity.Id,
                Content = entity.Content,
                CreatedDate = entity.CreatedDate,
                CommentBy = _userMapper.ToDto(entity.CommentBy)
            };
        }
    }
}
