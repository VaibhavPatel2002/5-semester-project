using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Music
{
    class Connection
    {
        SqlConnection con = new SqlConnection(@"Data Source=" + Application.StartupPath + @"\bin\Debug\MusicSchoolDb.mdf;Integrated Security=True;Connect Timeout=30");
    }
}
