using System;
using System.Collections.Generic;
using System.Text;
using Gat.Common;
using Gat.Common.Project.Model;

namespace Gat.SqlServer
{
    public class SqlServerEngine : IDatabaseEngine
    {
        public void Install(project proj, ConnectionInfo connectionInfo, bool strict)
        {
            throw new NotImplementedException();
        }

        public void Update(project proj, ConnectionInfo connectionInfo, bool strict)
        {
            throw new NotImplementedException();
        }

        public void Update(string projectName, ConnectionInfo connectionInfo, bool strict)
        {
            throw new NotImplementedException();
        }

        public void Uninstall(project proj, ConnectionInfo connectionInfo)
        {
            throw new NotImplementedException();
        }

        public void Uninstall(string projectName, ConnectionInfo connectionInfo)
        {
            throw new NotImplementedException();
        }

        public List<string> Validate(project proj, ConnectionInfo connectionInfo, bool strict)
        {
            throw new NotImplementedException();
        }

        public List<string> GetProjects(ConnectionInfo connectionInfo)
        {
            throw new NotImplementedException();
        }

        public List<string> GetConditionals(string projectName, ConnectionInfo connectionInfo)
        {
            throw new NotImplementedException();
        }

        public project GetProject(string projectName, ConnectionInfo connectionInfo)
        {
            throw new NotImplementedException();
        }

        public conditional GetConditional(string projectName, ConnectionInfo connectionInfo)
        {
            throw new NotImplementedException();
        }

        public List<string> GetAppliedSchemas(project proj, string conditionalName, ConnectionInfo connectionInfo)
        {
            throw new NotImplementedException();
        }

        public List<string> GetAppliedTables(project proj, string conditionalName, ConnectionInfo connectionInfo)
        {
            throw new NotImplementedException();
        }

        public List<string> GetAppliedColumns(project proj, string conditionalName, ConnectionInfo connectionInfo)
        {
            throw new NotImplementedException();
        }
    }
}
