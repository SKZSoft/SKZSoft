﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKZSoft.Twitter.TwitterJobs.Factories
{
    public class JobFactories
    {
        private DirectMessage m_directMessage;
        private Followers m_followers;
        private Help m_help;
        private Media m_media;
        private Oauth m_oauth;
        private Statuses m_statuses;

        public DirectMessage DirectMessage { get { return m_directMessage; } }
        public Followers Followers { get { return m_followers; } }
        public Help Help { get { return m_help; } }
        public Media Media { get { return m_media; } }
        public Oauth Oauth { get { return m_oauth; } }
        public Statuses Statuses { get { return m_statuses; } }


        public JobFactories(Batch batch)
        {
            m_directMessage = new DirectMessage(batch);
            m_followers = new Followers(batch);
            m_help = new Help(batch);
            m_media = new Media(batch);
            m_oauth = new Oauth(batch);
            m_statuses = new Statuses(batch);
        }



    }
}
