﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevInstance.SampleWebApp.Server.Database.Core.Data.Queries.Base
{
    public interface IQSearchable<T>
    {
        T ByPublicId(string id);
    }
}
