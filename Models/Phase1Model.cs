using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GIATA_Integration_V2.Models
{
    public class Phase1Runner
    {

        private GiataConnections GiataConn = new GiataConnections();
        private List<GiataHotel> GiataHotelDetails = new List<GiataHotel>();
        private List<TourplanHotel> ClientHotelDetails = new List<TourplanHotel>();

        private string clientID { get; set; }
        
        public Phase1Runner(string ID)
        {
            this.clientID = ID;
        }
        
        public void doUpdate()
        {
            /*
            List<TourplanHotel> AllClientHotels = getClientHotels(this.clientID);
            List<TourplanHotel> AllClientHotelsUpdated = new List<TourplanHotel>();
            List<GiataHotel> AllGiataHotels = GiataConn.getGiataHotels();
            bool matchFound = false;
            foreach (TourplanHotel currentTHotel in AllClientHotels)
            {
                matchFound = false;
                foreach (GiataHotel currentGHotel in AllGiataHotels)
                {
                    if (getMatch(currentTHotel, currentGHotel))
                    {
                        TourplanHotel UpdatedHotel = currentTHotel;
                        UpdatedHotel.GiataID = currentGHotel.GiataID;
                        AllClientHotelsUpdated.Add(UpdatedHotel);
                        matchFound = true;
                        break;
                    }
                }
                if(matchFound)
                {
                    break;
                }
            }
            foreach(TourplanHotel currentHotel in AllClientHotelsUpdated)
            {
                if(currentHotel.GiataID != null)
                {
                    //updateGiataID(currentHotel.TourplanID, currentHotel.GiataID);
                }
            }
            this.currentStatus = "Complete";
            */
        }

        public void updateGiataHotels()
        {
            GiataHotelDetails = new List<GiataHotel>();
            GiataHotelDetails = GiataConn.getGiataHotels();
        }
        public void updateGiataHotelsSpecific(string country)
        {
            GiataHotelDetails = new List<GiataHotel>();
            GiataHotelDetails = GiataConn.getGiataHotelsSpecific(country);
        }

        public List<GiataHotel> GetAllGiataHotels()
        {
            return this.GiataHotelDetails;
        }

        public List<TourplanHotel> GetClientHotels()
        {
            return this.ClientHotelDetails;
        }

        

    }
}