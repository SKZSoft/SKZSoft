using SKZTweets.TwitterModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using SKZTweets.TwitterJobs.Consts;
using SKZTweets.TwitterJobs.Interfaces;

namespace SKZTweets.TwitterJobs
{
    /// <summary>
    /// Post media
    /// </summary>
    public class JobPostMedia: TwitterJob
    {
        private MediaUploaded m_mediaUploaded;

        internal JobPostMedia(EventHandler<JobCompleteArgs> completionDelegate, string localPath) : base(completionDelegate)
        {
            Byte[] bytes = File.ReadAllBytes(localPath);
            ParametersBinary.Add("media", bytes);
        }

        public override bool AuthParametersOnly { get { return true; } }

        public override string JobDescription { get { return TwitterDataStrings.JobDescUploadMedia; } } 

        public override string URL { get { return URLs.URL_API_UPLOAD_MEDIA; } }

        public override ApiResponseType ResponseType { get { return ApiResponseType.json; } }
        public override ParameterType ParameterType { get { return ParameterType.http; } }

        public override HttpMethod RequestType { get { return HttpMethod.Post; } }

        public override void Finalize(string results)
        {
            m_mediaUploaded = Newtonsoft.Json.JsonConvert.DeserializeObject<MediaUploaded>(results);
        }

        public override void InitializeFromLastJob(Job previousJob)
        {

        }

        public override void AddParameters()
        {
            MultipartFormDataContent form = new MultipartFormDataContent();
            foreach (KeyValuePair<string, object> kvp in ParametersBinary)
            {
                byte[] data = (byte[])kvp.Value;
                form.Add(new ByteArrayContent(data), kvp.Key);
            }

            m_httpRequest.Content = form;
        }



        /// <summary>
        /// The uploaded media data
        /// </summary>
        public MediaUploaded MediaUploaded {  get { return m_mediaUploaded; } }
    }
}
