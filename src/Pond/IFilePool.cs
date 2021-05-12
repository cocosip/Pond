using System;
using System.Collections.Generic;
using System.Text;

namespace Pond
{
    public interface IFilePool<TFilePool> : IFilePool
        where TFilePool : class
    {

    }

    public interface IFilePool
    {

    }
}
