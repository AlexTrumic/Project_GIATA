using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Xml;

namespace GIATA_Integration_V2.Models
{
    public class GiataConnections
    {
        public GiataHotel updateHotelDetails(GiataHotel givenHotel)
        {
            GiataHotel returnedHotel = givenHotel;


            return returnedHotel;
        }

        public List<GiataHotel> getGiataHotelsSpecific(string country)
        {
            string GiataResponseString = getGiataData("http://ghgml.giatamedia.com/webservice/rest/1.0/items/" + "?country=" + country);
            XmlDocument GiataResponseXML = formatRawGiataString(GiataResponseString);
            List<GiataHotel> AllGiataHotels = extractGiataHotels(GiataResponseXML);
            return AllGiataHotels;
        }

        public List<GiataHotel> getGiataHotels()
        {
            string GiataResponseString = getGiataData("http://ghgml.giatamedia.com/webservice/rest/1.0/items/");
            XmlDocument GiataResponseXML = formatRawGiataString(GiataResponseString);
            List<GiataHotel> AllGiataHotels = extractGiataHotels(GiataResponseXML);
            return AllGiataHotels;
        }

        private string getGiataData(string addressString)
        {
            string password = "HqsGvDbt";
            string username = "multicodes|tourplan.com";
            WebClient client = new WebClient();
            client.UseDefaultCredentials = true;
            client.Credentials = new NetworkCredential(username, password);
            string content = client.DownloadString(addressString);
            return content;
        }

        private XmlDocument formatRawGiataString(string GiataItemsRaw)
        {
            XmlDocument MyData = new XmlDocument();
            MyData.LoadXml(GiataItemsRaw);
            return MyData;
        }

        private List<GiataHotel> extractGiataHotels(XmlDocument GiataResponse)
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



    }
}