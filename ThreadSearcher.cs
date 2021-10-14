using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributedSearch
{
    class ThreadSearcher
    {
        int del;
        String text;
        String TS;
        char[] buff;
        int startindex = -1;
        int id;

        public ThreadSearcher(int d, String txt, String ts, char[] buffer)
        {
            text = txt;
            TS = ts;
            del = d;
            buff = buffer;


        }
        public void run()
        {
            Search(this);
        }
        public String getStringToSearch()
        {
            return TS;
        }
        public int getDelta()
        {
            return del;
        }
        public String getText()
        {
            return text;
        }
        public void setIndex(int n)
        {
            this.startindex = n;
        }
        public int getIndex()
        {
            return startindex;
        }
        public void setId(int ID)
        {
            this.id = ID;
        }
        public char[] getBuffer()
        {
            return this.buff;
        }
        public void setBuffer(char[] b)
        {
            this.buff = b;
        }
        public static void Search(object o)
        {

            int index = -1;

            ThreadSearcher t = (ThreadSearcher)o;
            int delta = ((ThreadSearcher)o).getDelta();


            int buffernumber = 0;
            
            String subString = ((ThreadSearcher)o).getStringToSearch();


            String location = ((ThreadSearcher)o).getText();


            using (StreamReader r = new StreamReader(location))
            {
                char[] buffer = ((ThreadSearcher)o).getBuffer();

                for (int i = 0; i < buffer.Length; i++)
                {
                    if (index != -1)
                    {

                        break;
                    }
                    Boolean found = true;
                    int count = 0;
                    for (int j = i; j < i + ((delta) * subString.Length); j += delta)
                    {
                        found = true;
                        if (j < 10000)
                        {
                            if (subString[count++] != buffer[j])
                            {

                                found = false;
                                break;
                            }
                        }
                    }
                    if (found)
                    {
                        index = i;

                        break;
                    }


                }

            }
            

            ((ThreadSearcher)o).setIndex(index);
        }
    }
}

