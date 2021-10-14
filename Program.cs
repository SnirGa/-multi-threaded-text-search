using System;
using System.IO;
using System.Threading;

namespace DistributedSearch
{
    class Program
    {
        static void Main(string[] args)
        {

            Boolean B = false;
            Boolean br = false;
            CancellationTokenSource source = new CancellationTokenSource();
            CancellationToken token = source.Token;
            ThreadSearcher T1;
           // using (StreamReader SR = new StreamReader("textfile.txt"))
            using (StreamReader SR = new StreamReader(args[0]))

            {
                char[] buffer = new char[10000];

                //T1 = new ThreadSearcher(2, "textfile.txt", "OW", buffer);
                int d = Int32.Parse(args[3]);
                T1 = new ThreadSearcher(d+1, args[0], args[1], buffer);
                int found = -1;
                int blocknumber = 0;
                int numofthreads= Int32.Parse(args[2]);
                 ThreadPool.SetMinThreads(numofthreads, 0);
               ThreadPool.SetMaxThreads(numofthreads, 0);
                // ThreadPool.SetMinThreads(1, 0);
                //ThreadPool.SetMaxThreads(1, 0);
                for (int i = 0; i < numofthreads; i++)
                {
                    while (!SR.EndOfStream)
                    {
                        SR.ReadBlock(buffer, 0, 10000);
                        T1.setBuffer(buffer);
                        ThreadPool.QueueUserWorkItem(ThreadSearcher.Search, T1);
                        Thread.Sleep(100);
                        if (T1.getIndex() != -1)
                        {
                            found = T1.getIndex() + blocknumber * 10000;
                            br = true;
                            break;
                        }
                        blocknumber++;
                    }
                    if (br == true)
                    {
                        break;
                    }
                }
                if (found != -1)
                {
                    Console.WriteLine(found);
                }
                else
                {
                    Console.WriteLine("not found");

                }


            }
        }
    }
}
