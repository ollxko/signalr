using System;
using System.Collections.Generic;
using System.Linq;
using BadNews.Models.Comments;
using BadNews.Repositories.Comments;
using Microsoft.AspNetCore.Mvc;

namespace BadNews.Controllers
{
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly CommentsRepository commentsRepository;

        public CommentsController(CommentsRepository commentsRepository)
        {
            this.commentsRepository = commentsRepository;
        }

        // GET
        [HttpGet("api/news/{id}/comments")]
        public ActionResult<CommentsDto> GetCommentsForNews(Guid newsId)
        {
            var comments = commentsRepository.GetComments(newsId);

            var commentDtos =comments.Select(comment =>
            {
                var commentDto = new CommentDto();
                commentDto.User = comment.User;
                commentDto.Value = comment.Value;
                return commentDto;
            }).ToArray();
            var commentsDtos = new CommentsDto { NewsId=newsId, Comments= commentDtos };
            return commentsDtos;
        }
    }
}