using System;
using Business.Abstract;

namespace Business.Concrete
{
    public class BlogManager : IBlogService
    {
        public void Test()
        {
            Console.WriteLine("Test");
        }
    }
}