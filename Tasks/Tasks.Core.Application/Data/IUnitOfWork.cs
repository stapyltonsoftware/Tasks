﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasks.Core.Application.Data
{
    public interface IUnitOfWork
    {
        Task SaveAsync();
    }
}
