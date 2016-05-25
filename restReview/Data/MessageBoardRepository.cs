using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace restReview.Data
{
    public class MessageBoardRepository : IMessageBoardRepository
    {
        private MessageBoardContext _db;

        public MessageBoardRepository(MessageBoardContext db)
        {
            _db = db;
        }

        public IQueryable<Topic> GetTopics()
        {
            
            return _db.Topics;
        }

        public IQueryable<Topic> GetTopicsIncludingReplies()
        {
            return _db.Topics.Include("replies");
        }

        public IQueryable<Reply> GetRepliesByTopic(int topicId)
        {
            return _db.Replies.Where(r => r.TopicId == topicId);
        }

        public bool Save()
        {
            try
            {
                return _db.SaveChanges() > 0;
            }
            catch (Exception ex) 
            {
                //TODO log this error
                return false;
            }
        }

        public bool AddTopic(Topic newTopic)
        {
            try
            {
                _db.Topics.Add(newTopic);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool AddReply(Reply newReply)
        {
            try
            {
                _db.Replies.Add(newReply);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}