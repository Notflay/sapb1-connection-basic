using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sapb1_connection_basic
{
    public interface ISapRepository
    {
        int add(object obj);
        int update(object obj);
        object readAll();
        object read(string id);

    }
}
