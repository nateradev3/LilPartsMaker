using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace LilPartsMaker
{
    class Item
    {
        private ArrayList manufacturers;
        private ArrayList makes;
        private ArrayList dimensions;
        public string category { get; set; }
        public string partNumber { get; set; }
        public string imageTitle { get; set; }
        public string imageAlt { get; set; }
        public string makesAndModels { get; set; }
        public string itemNumber { get; set; }
        public string itemName { get; set; }
        public string imageLink { get; set; }
        public string application { get; set; }
        public string price1 { get; set; }
        public string price10 { get; set; }
        public string price20 { get; set; }
        public string price50 { get; set; }
        public string price100 { get; set; }
        public string color { get; set; }
        public string autoPartNumber {get; set; }
        public string aevecoPartNumber {get; set; }
        public Item()
        {
            manufacturers = new ArrayList();
            makes = new ArrayList();
            dimensions = new ArrayList();
        }
        public string getPartLink(string make, string manufacturer)
        {
            return ("http://www.lilparts.com/" + make + "_" + this.category + "/" + manufacturer + ".html").ToLower().Replace("-", "_").Replace(" ", "_");
        }

        public string getPartNumberLink()
        {
            return ("lilparts.com/mypn/" + partNumber + ".html").ToLower().Replace("-", "_").Replace(" ", "_");
        }
        public string getPartLink(int makeIndex)
        {
            return getPartLink((string)getMakes()[makeIndex], (string)getManufacturers()[makeIndex]);
        }
        public ArrayList getMakes()
        {
            return makes;
        }

        public ArrayList getManufacturers()
        {
            return manufacturers;
        }

        public void addMake(string make)
        {
            makes.Add(make);
        }

        public void addManufacturer(string manufacturer)
        {
            manufacturers.Add(manufacturer);
        }

        public void addDimension(string dimension)
        {
            dimensions.Add(dimension);
        }

        public ArrayList getDimensions()
        {
            return dimensions;
        }

        public string toString()
        {
            string output = "";
            output += "Application: " + application + "\n";
            output += "Manufacturer 1: " + manufacturers[0] + "\n";
            output += "Manufacturer 2: " + manufacturers[1] + "\n";
            output += "Manufacturer 3: " + manufacturers[2] + "\n";
            output += "Make 1: " + makes[0] + "\n";
            output += "Make 2: " + makes[1] + "\n";
            output += "Make 3: " + makes[2] + "\n";
            output += "Image Link: " + imageLink + "\n";
            output += "Price 1: " + price1 + "\n";
            output += "Price 10: " + price10 + "\n";
            output += "Price 20: " + price20 + "\n";
            output += "Price 50: " + price50 + "\n";
            output += "Price 100: " + price100 + "\n";
            output += "partNumber: " + partNumber + "\n";
            output += "itemNumber: " + itemNumber + "\n";
            output += "itemName: " + itemName + "\n";
            return output;
        }

    }

}
