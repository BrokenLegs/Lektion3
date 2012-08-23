using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lektion3.Model
{
    public class Post
    {
        public Post() { }
        public Post(Guid createdByID, string title, string body, Post parent = null)
        {
            CreatedByID = createdByID;
            Title = title;
            Body = body;
            ParentPost = parent;
            CreateDate = DateTime.UtcNow;
        }

        private User _CreatedBy { get; set; }
        public Guid CreatedByID { get; set; }
        public User CreatedBy { get { return _CreatedBy; } }

        public DateTime CreateDate { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public Post ParentPost { get; set; }
        public List<PostTags> Tags { get; set; }

        public string FormattedTagList
        {
            get
            {
                if (Tags.Count == 0)
                    return "";

                string tagListString = "";
                foreach (var tag in Tags)
                {
                    if (!string.IsNullOrEmpty(tagListString))
                        tagListString += ", ";
                    tagListString += tag;
                }

                return tagListString;
            }
        }

        public string ToString(bool ShortFormat = true)
        {
            string postString = string.Format("\tTitle: '{0}' - By: '{1}' - Posted: '{2:d}'", Title, CreatedBy != null ? CreatedBy.UserName : "?", CreateDate);
            if (!ShortFormat)
                postString += string.Format("\n\t\t Message: {0}\n\t\t Tags: {1}", Body, FormattedTagList);

            return postString;
        }

        /*
         * OBS! Denna konstruktion för att ladda relaterad data är varken en bra eller vanlig konstruktion.
         * 
         * Vi kommer se hur detta kan skötas i verkligheten senare i kursen
         * 
         */
        public void LoadUser(List<User> users)
        {
            _CreatedBy = users.Where(u => u.UserID == CreatedByID).FirstOrDefault();
        }

        public enum PostTags
        {
            Interesting,
            Funny,
            Troll,
            JawDropping,
            Useful
        }
    }
}
