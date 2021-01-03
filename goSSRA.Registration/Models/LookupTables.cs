using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace goSSRA.Registration.Models
{
    /// HTML Code
    /// <%= Html.DropDownList("state", States.StateSelectList)%>

    public class States
    {
        public static readonly IDictionary<string, string> StateDictionary = new Dictionary<string, string> {
            {"ALABAMA", "AL"},
            {"ALASKA", "AK"},
            {"AMERICAN SAMOA", "AS"},
            {"ARIZONA ", "AZ"},
            {"ARKANSAS", "AR"},
            {"CALIFORNIA ", "CA"},
            {"COLORADO ", "CO"},
            {"CONNECTICUT", "CT"},
            {"DELAWARE", "DE"},
            {"DISTRICT OF COLUMBIA", "DC"},
            {"FEDERATED STATES OF MICRONESIA", "FM"},
            {"FLORIDA", "FL"},
            {"GEORGIA", "GA"},
            {"GUAM ", "GU"},
            {"HAWAII", "HI"},
            {"IDAHO", "ID"},
            {"ILLINOIS", "IL"},
            {"INDIANA", "IN"},
            {"IOWA", "IA"},
            {"KANSAS", "KS"},
            {"KENTUCKY", "KY"},
            {"LOUISIANA", "LA"},
            {"MAINE", "ME"},
            {"MARSHALL ISLANDS", "MH"},
            {"MARYLAND", "MD"},
            {"MASSACHUSETTS", "MA"},
            {"MICHIGAN", "MI"},
            {"MINNESOTA", "MN"},
            {"MISSISSIPPI", "MS"},
            {"MISSOURI", "MO"},
            {"MONTANA", "MT"},
            {"NEBRASKA", "NE"},
            {"NEVADA", "NV"},
            {"NEW HAMPSHIRE", "NH"},
            {"NEW JERSEY", "NJ"},
            {"NEW MEXICO", "NM"},
            {"NEW YORK", "NY"},
            {"NORTH CAROLINA", "NC"},
            {"NORTH DAKOTA", "ND"},
            {"NORTHERN MARIANA ISLANDS", "MP"},
            {"OHIO", "OH"},
            {"OKLAHOMA", "OK"},
            {"OREGON", "OR"},
            {"PALAU", "PW"},
            {"PENNSYLVANIA", "PA"},
            {"PUERTO RICO", "PR"},
            {"RHODE ISLAND", "RI"},
            {"SOUTH CAROLINA", "SC"},
            {"SOUTH DAKOTA", "SD"},
            {"TENNESSEE", "TN"},
            {"TEXAS", "TX"},
            {"UTAH", "UT"},
            {"VERMONT", "VT"},
            {"VIRGIN ISLANDS", "VI"},
            {"VIRGINIA", "VA"},
            {"WASHINGTON", "WA"},
            {"WEST VIRGINIA", "WV"},
            {"WISCONSIN", "WI"},
            {"WYOMING", "WY"}
        };

        public static SelectList StateSelectList
        {
            // View Code
            // @Html.DropDownList("State", goSSRA.Registration.Models.StatesDictionary.StateSelectList )
            // or
            // @Html.DropDownListFor(model => model.State, goSSRA.Registration.Models.States.StateSelectList)

            // DB get list method
            // ViewBag.EmployeeId = New SelectList(db.Employees, "Id", "Name", 1)
            get { return new SelectList(StateDictionary, "Value", "Key", "WA"); }
        }
    }

    public class Gender
    {
        public static readonly IDictionary<string, string> GenderDictionary = new Dictionary<string, string> {
            {"", "N"},
            {"Male", "M"},
            {"Female", "F"},
        };

        public static SelectList GenderSelectList
        {
            // @Html.DropDownListFor(model => model.Gender, goSSRA.Registration.Models.Gender.GenderSelectList)
            get { return new SelectList(GenderDictionary, "Value", "Key", "N"); }
        }
    }

    public class PhoneTypes
    {
        public static readonly IDictionary<string, string> PhoneTypeDictionary = new Dictionary<string, string> {
            {"", "X"},
            {"Cell", "C"},
            {"Home", "H"},
            {"Work", "W"},
            {"Other", "O"},
        };

        public static SelectList PhoneTypeSelectList
        {
            // @Html.DropDownListFor(model => model.PhoneType, goSSRA.Registration.Models.PhoneTypesDictionary.PhoneTypeSelectList)
            get { return new SelectList(PhoneTypeDictionary, "Value", "Key", "X"); }
        }
    }
}