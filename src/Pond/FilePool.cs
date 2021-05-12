using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pond
{
    public class FilePool<TFilePool> : IFilePool<TFilePool> where TFilePool : class
    {

    }

    public class FilePool : IFilePool
    {

        protected ILogger Logger { get; }
        public FilePool(ILogger<FilePool> logger)
        {
            Logger = logger;
        }


        public void Run()
        {

        }


    }
}
