using System;
using System.Collections.Generic;
using System.Text;
using Gat.Common.Project.Model;

namespace Gat.Common
{
    /// <summary>
    /// Interface all vendors must implement.
    /// </summary>
    public interface IDatabaseEngine
    {
        void Install(project proj, ConnectionInfo connectionInfo, bool strict);

        void Update(project proj, ConnectionInfo connectionInfo, bool strict);

        void Update(string projectName, ConnectionInfo connectionInfo, bool strict);

        void Uninstall(project proj, ConnectionInfo connectionInfo);

        void Uninstall(string projectName, ConnectionInfo connectionInfo);

        List<string> Validate(project proj, ConnectionInfo connectionInfo, bool strict);

        List<string> GetProjects(ConnectionInfo connectionInfo);

        List<string> GetConditionals(string projectName, ConnectionInfo connectionInfo);

        project GetProject(string projectName, ConnectionInfo connectionInfo);

        conditional GetConditional(string projectName, ConnectionInfo connectionInfo);

        List<string> GetAppliedSchemas(project proj, string conditionalName, ConnectionInfo connectionInfo);

        List<string> GetAppliedTables(project proj, string conditionalName, ConnectionInfo connectionInfo);

        List<string> GetAppliedColumns(project proj, string conditionalName, ConnectionInfo connectionInfo);
    }
}
