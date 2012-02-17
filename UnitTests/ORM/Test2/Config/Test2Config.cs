﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utilities.ORM.QueryProviders.Interfaces;

namespace UnitTests.ORM.Test1.Config
{
    public class Test2Config : IDatabase
    {
        public string Name
        {
            get { return "Test 2 Configuration"; }
        }

        public string ConnectionString
        {
            get { return "Data Source=localhost;Initial Catalog=ORMTestDatabase2;Integrated Security=SSPI;Pooling=false"; }
        }

        public string ParameterStarter
        {
            get { return "@"; }
        }

        public bool Update
        {
            get { return true; }
        }

        public bool Writable
        {
            get { return true; }
        }

        public bool Readable
        {
            get { return true; }
        }

        public int Order
        {
            get { return 1; }
        }
    }
}
