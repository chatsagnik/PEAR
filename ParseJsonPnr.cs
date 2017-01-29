using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PEAR
{
    public class ToStation
    {
        public string code { get; set; }
        public string name { get; set; }
    }

    public class FromStation
    {
        public string code { get; set; }
        public string name { get; set; }
    }

    public class Passenger
    {
        public string booking_status { get; set; }
        public int no { get; set; }
        public int coach_position { get; set; }
        public string current_status { get; set; }
    }

    public class TrainStartDate
    {
        public int day { get; set; }
        public int month { get; set; }
        public int year { get; set; }
    }

    public class ReservationUpto
    {
        public string code { get; set; }
        public string name { get; set; }
    }

    public class BoardingPoint
    {
        public string code { get; set; }
        public string name { get; set; }
    }

    public class RootObjectPNR
    {
        public string train_num { get; set; }
        public ToStation to_station { get; set; }
        public bool error { get; set; }
        public FromStation from_station { get; set; }
        public List<Passenger> passengers { get; set; }
        public string train_name { get; set; }
        public TrainStartDate train_start_date { get; set; }
        public int total_passengers { get; set; }
        public int response_code { get; set; }
        public string chart_prepared { get; set; }
        public ReservationUpto reservation_upto { get; set; }
        public double failure_rate { get; set; }
        public string pnr { get; set; }
        public string @class { get; set; }
        public string doj { get; set; }
        public BoardingPoint boarding_point { get; set; }
    }
}