using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;
using System.IO;
using System.Xml;
using System.Xml.Linq;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("here");
            RunNow();
            Console.WriteLine("here1");
            Console.ReadLine();
        }

        private async static void RunNow()
        {
            await getGiataData("http://ghgml.giatamedia.com/webservice/rest/1.0/items");
        }

        static async private Task getGiataData(string addressString)
        {
            string password = "HqsGvDbt";
            string username = "multicodes|tourplan.com";
            var handler = new HttpClientHandler();
            NetworkCredential myCredentials = new NetworkCredential(username, password);
            handler.Credentials = myCredentials;
            HttpClient client = new HttpClient(handler);

            List<GiataHotel> myHotels = getGiataHotels();

            string xml1 = @"<?xml version='1.0' encoding='utf-8'?>
            <giataIds>
                <giataId>3</giataId>
                <giataId>75</giataId>
            </giataIds>";

            string xml = @"<?xml version='1.0' encoding='utf-8'?>
            <giataIds>
                <giataId>";
            foreach (GiataHotel currHotel in myHotels)
            {
                xml = xml + currHotel.GiataID.ToString() + @"</giataId>
                <giataId>";
            };
            xml = xml.Substring(0, xml.Length-12) + "</giataIds>";

            var httpContent = new StringContent(xml, Encoding.UTF8, "application/xml");


            var response = await client.PostAsync(addressString, httpContent);

            var responseString = await response.Content.ReadAsStringAsync();




        }




        static private XmlDocument formatRawGiataString(string GiataItemsRaw)
        {
            XmlDocument MyData = new XmlDocument();
            MyData.LoadXml(GiataItemsRaw);
            return MyData;
        }

        static public List<GiataHotel> getGiataHotels()
        {
            string GiataResponseString = getGiataData1("http://ghgml.giatamedia.com/webservice/rest/1.0/items/?country=ES");
            XmlDocument GiataResponseXML = formatRawGiataString(GiataResponseString);
            List<GiataHotel> AllGiataHotels = extractGiataHotels(GiataResponseXML);
            return AllGiataHotels;
        }

        static private string getGiataData1(string addressString)
        {
            string password = "HqsGvDbt";
            string username = "multicodes|tourplan.com";
            WebClient client = new WebClient();
            client.UseDefaultCredentials = true;
            client.Credentials = new NetworkCredential(username, password);
            string content = client.DownloadString(addressString);
            return content;
        }



        static private List<GiataHotel> extractGiataHotels(XmlDocument GiataResponse)
        {
            List<GiataHotel> AllGiataHotels = new List<GiataHotel>();
            XmlNodeList MyNodes = GiataResponse.SelectNodes("//item");
            foreach (XmlNode childNode in MyNodes)
            {
                string GiataID = childNode.Attributes["giataId"].Value;
                string lastUpdate = childNode.Attributes["lastUpdate"].Value;
                string reference = childNode.Attributes["xlink:href"].Value;
                GiataHotel newGiataHotel = new GiataHotel(GiataID, lastUpdate, reference);
                AllGiataHotels.Add(newGiataHotel);
            }
            return AllGiataHotels;
        }






















        private static async Task SumPageSizesAsync()
        {
            DateTime a = DateTime.Now;
            // Make a list of web addresses.  
            HttpClient client =
                new HttpClient() { MaxResponseContentBufferSize = 1000000 };

            // Make a list of web addresses.  
            List<string> urlList = SetUpURLList();

            var total = 0;

            foreach (var url in urlList)
            {
                // GetByteArrayAsync returns a task. At completion, the task  
                // produces a byte array.  
                byte[] urlContents = await client.GetByteArrayAsync(url);

                // The following two lines can replace the previous assignment statement.  
                //Task<byte[]> getContentsTask = client.GetByteArrayAsync(url);  
                //byte[] urlContents = await getContentsTask;  

                DisplayResults(url, urlContents);

                // Update the total.  
                total += urlContents.Length;
            }

            // Display the total count for all of the web addresses.  
            Console.WriteLine(string.Format("\r\n\r\nTotal bytes returned:  {0}\r\n", total));
            DateTime b = DateTime.Now;
            Console.WriteLine("Total Time Async = " + b.Subtract(a).TotalSeconds);
        }

        static private List<string> SetUpURLList()
        {
            var urls = new List<string>
            {
                "http://msdn.microsoft.com/library/windows/apps/br211380.aspx",
                "http://msdn.microsoft.com",
                "http://msdn.microsoft.com/library/hh290136.aspx",
                "http://msdn.microsoft.com/library/ee256749.aspx",
                "http://msdn.microsoft.com/library/hh290138.aspx",
                "http://msdn.microsoft.com/library/hh290140.aspx",
                "http://msdn.microsoft.com/library/dd470362.aspx",
                "http://msdn.microsoft.com/library/aa578028.aspx",
                "http://msdn.microsoft.com/library/ms404677.aspx",
                "http://msdn.microsoft.com/library/ff730837.aspx"
            };
            return urls;
        }

        static async Task<byte[]> GetURLContentsAsync(string url)
        {
            // The downloaded resource ends up in the variable named content.  
            var content = new MemoryStream();

            // Initialize an HttpWebRequest for the current URL.  
            var webReq = (HttpWebRequest)WebRequest.Create(url);

            // Send the request to the Internet resource and wait for  
            // the response.  
            // Note: you can't use HttpWebRequest.GetResponse in a Windows Store app.  
            using (WebResponse response = await webReq.GetResponseAsync())
            {
                // Get the data stream that is associated with the specified URL.  
                using (Stream responseStream = response.GetResponseStream())
                {
                    // Read the bytes in responseStream and copy them to content.    
                    await responseStream.CopyToAsync(content);
                }
            }

            // Return the result as a byte array.  
            return content.ToArray();
        }

        static private void DisplayResults(string url, byte[] content)
        {
            // Display the length of each website. The string format   
            // is designed to be used with a monospaced font, such as  
            // Lucida Console or Global Monospace.  
            var bytes = content.Length;
            // Strip off the "http://".  
            var displayURL = url.Replace("http://", "");
            Console.WriteLine(string.Format("\n{0,-58} {1,8}", displayURL, bytes));
        }


        static private void SumPageSizes()
        {
            // Make a list of web addresses.  
            List<string> urlList = SetUpURLList1();
            DateTime a = DateTime.Now;
            var total = 0;
            foreach (var url in urlList)
            {
                // GetURLContents returns the contents of url as a byte array.  
                byte[] urlContents = GetURLContents(url);

                DisplayResults1(url, urlContents);

                // Update the total.  
                total += urlContents.Length;
            }

            // Display the total count for all of the web addresses.  
            Console.WriteLine(string.Format("\r\n\r\nTotal bytes returned:  {0}\r\n", total));

            DateTime b = DateTime.Now;
            Console.WriteLine("Total Time Not Async = " + b.Subtract(a).TotalSeconds);
        }

        static private List<string> SetUpURLList1()
        {
            var urls = new List<string>
    {
        "http://msdn.microsoft.com/library/windows/apps/br211380.aspx",
        "http://msdn.microsoft.com",
        "http://msdn.microsoft.com/library/hh290136.aspx",
        "http://msdn.microsoft.com/library/ee256749.aspx",
        "http://msdn.microsoft.com/library/hh290138.aspx",
        "http://msdn.microsoft.com/library/hh290140.aspx",
        "http://msdn.microsoft.com/library/dd470362.aspx",
        "http://msdn.microsoft.com/library/aa578028.aspx",
        "http://msdn.microsoft.com/library/ms404677.aspx",
        "http://msdn.microsoft.com/library/ff730837.aspx"
    };
            return urls;
        }

        static private byte[] GetURLContents(string url)
        {
            // The downloaded resource ends up in the variable named content.  
            var content = new MemoryStream();

            // Initialize an HttpWebRequest for the current URL.  
            var webReq = (HttpWebRequest)WebRequest.Create(url);

            // Send the request to the Internet resource and wait for  
            // the response.  
            // Note: you can't use HttpWebRequest.GetResponse in a Windows Store app.  
            using (WebResponse response = webReq.GetResponse())
            {
                // Get the data stream that is associated with the specified URL.  
                using (Stream responseStream = response.GetResponseStream())
                {
                    // Read the bytes in responseStream and copy them to content.    
                    responseStream.CopyTo(content);
                }
            }

            // Return the result as a byte array.  
            return content.ToArray();
        }

        static private void DisplayResults1(string url, byte[] content)
        {
            // Display the length of each website. The string format   
            // is designed to be used with a monospaced font, such as  
            // Lucida Console or Global Monospace.  
            var bytes = content.Length;
            // Strip off the "http://".  
            var displayURL = url.Replace("http://", "");
            Console.WriteLine(string.Format("\n{0,-58} {1,8}", displayURL, bytes));
        }


    }
}
