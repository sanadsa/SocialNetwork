﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Common.Interfaces
{
    public interface ITableDataService
    {
        void Store<T>(T item) where T : new();
        void BatchStore<T>(IEnumerable<T> items) where T : class;
        IEnumerable<T> GetAll<T>() where T : class;
        T GetItem<T>(string key) where T : class;
    }
}
