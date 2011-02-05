using System;
using System.Collections.Generic;
using System.Text;

namespace Gat.Common
{
    public class ConnectionInfo
    {

        private string _dbHost;
        private int? _dbPort;
        private string _dbName;
        private string _dbUser;
        private string _dbPass;

        public string DbHost
        {
            get { return _dbHost; }
            set { _dbHost = value; }
        }

        public int? DbPort
        {
            get { return _dbPort; }
            set { _dbPort = value; }
        }

        public string DbName
        {
            get { return _dbName; }
            set { _dbName = value; }
        }

        public string DbUser
        {
            get { return _dbUser; }
            set { _dbUser = value; }
        }

        public string DbPass
        {
            get { return _dbPass; }
            set { _dbPass = value; }
        }

    }
}
