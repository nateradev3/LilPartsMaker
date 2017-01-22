using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Text.RegularExpressions;
using System.Windows.Forms;


namespace LilPartsMaker
{
    class WebsiteMaker
    {
        private string csvFile = "";
        private string xlsFile = "";
        private string saveLocation = "";
        private readonly string topOfPageFile = "topOfPage.txt";
        private readonly string bottomOfPageFile = "bottomOfPage.txt";
        private ArrayList items;
        private HashSet<string> makes;
        private ArrayList categories;
        private readonly char fieldDelimiter = ';';

        private Dictionary<string, ArrayList> makeToCategories;

        public WebsiteMaker(string filePath, string saveLoc, Boolean isXls)
        {
            if (isXls)
            {
                xlsFile = filePath;
                csvFile = "";
            }
            else
            {
                xlsFile = "";
                csvFile = filePath;
            }
            saveLocation = saveLoc;
            items = new ArrayList();
            makes = new HashSet<string>();
            categories = new ArrayList();
            makeToCategories = new Dictionary<string, ArrayList>();
            
        }

        public int init()
        {
            if (xlsFile.Equals(""))
            {
                return loadCsv();
            } else {
                return loadXls();
            }
        }

        public ArrayList getCategories()
        {
            return categories;
        }
        public Dictionary<string, ArrayList> getMakeToCategories()
        {
            return makeToCategories;
        }

        public void makeAllPage()
        {
            string page = getTopOfPage("All Parts", "");
            for (int i = 0; i < items.Count; i++) {
                Item item = (Item) items[i];
                for (int currentIndexMakeManufacturer = 0; currentIndexMakeManufacturer < item.getMakes().Count; currentIndexMakeManufacturer++)
                {
                    page += getGenericGuts(item, currentIndexMakeManufacturer, false, true);
                }
            }
            page += getBottomOfPage();
            System.IO.File.WriteAllText(saveLocation + @"\all.html", page);
        }

        public void makeIndividualPages()
        {
            foreach (Item item in items)
            {
             //MAKES the pages /mypn/ab_pc_0006
                string keywordString = "";
                for (int i = 0; i < item.getMakes().Count; i++)
                {
                    string make = (string) item.getMakes()[i];
                    string manufacturer = (string) item.getManufacturers()[i];
                    keywordString += "," + make + "," + manufacturer;
                }
                if (item.aevecoPartNumber.Trim().Length > 0 && item.aevecoPartNumber.Split(' ').Length > 1)
                {
                    keywordString += ",aeveco," + "A" + item.aevecoPartNumber.Split(' ')[1];
                }
                if (keywordString.Length > 0)
                {
                    keywordString = keywordString.Substring(1);
                }

                string page = getTopOfPage(item.partNumber, keywordString, item.application);
                for (int j = 0; j < item.getMakes().Count; j++)
                {
                    page += getGenericGuts(item, j, true, true);
                }
                page += getBottomOfPage();
                string path = saveLocation + "/mypn/";
                System.IO.Directory.CreateDirectory(path);
                path += ((item.partNumber).Replace(" ", "_").Replace("-", "_") + ".html").ToLower();
                System.IO.File.WriteAllText(path, page);
             //MAKES the pages /mypn/ab_pc_0006

             //MAKES the pages /bmw_push_type_retainers/0006_p_0031.html
                for (int j =0; j < item.getMakes().Count; j++) {//make the pages for the manufacturerNumbers
                    string make = (string)item.getMakes()[j];
                    string manufacturer = (string)item.getManufacturers()[j];
                    keywordString = make + "," + manufacturer;
                    if (item.aevecoPartNumber.Trim().Length > 0 && item.aevecoPartNumber.Split(' ').Length > 1)
                    {
                        keywordString += ",aeveco," + "A" + item.aevecoPartNumber.Split(' ')[1];
                    }

                    page = getTopOfPage(make + " " + manufacturer, keywordString, item.application);
                    page += getGenericGuts(item, j, true, false);
                    page += getBottomOfPage();
                    path = saveLocation + "/" + ((string)item.getMakes()[j]).Replace(" ", "_") + "_" + item.category.Replace(" ", "_") + "/";
                    System.IO.Directory.CreateDirectory(path.ToLower());
                    path += ((string)item.getManufacturers()[j]).Replace("-", "_").ToLower() + ".html";
                    System.IO.File.WriteAllText(path, page);
                }
                //MAKES the pages /bmw/push_type_retainers/0006_p_0031.html
            }
        }

        public void makeCarPage(string model)
        {
            string page = getTopOfPage(model, "");

            for (int i = 0; i < items.Count; i++)
            {
                Item item = (Item)items[i];
                for (int currentIndexMakeManufacturer = 0; currentIndexMakeManufacturer < item.getMakes().Count; currentIndexMakeManufacturer++)
                {
                    if (((string)item.getMakes()[currentIndexMakeManufacturer]).Equals(model, StringComparison.InvariantCultureIgnoreCase))
                    {
                        page += getGenericGuts(item, currentIndexMakeManufacturer, false, true);
                    }
                }
            }
            page += getBottomOfPage();
            model = model.Replace(" ", "_").ToLower();
            System.IO.File.WriteAllText(saveLocation + @"\" + model + ".html", page);
        }

        public void makeCategoryPage(string model, string category)
        {
            string page = getTopOfPage(model, category);
            for (int i = 0; i < items.Count; i++)
            {
                Item item = (Item)items[i];
                for (int currentIndexMakeManufacturer = 0; currentIndexMakeManufacturer < item.getMakes().Count; currentIndexMakeManufacturer++)
                {
                    if (category.Equals(item.category, StringComparison.InvariantCultureIgnoreCase) && ((string)item.getMakes()[currentIndexMakeManufacturer]).Equals(model, StringComparison.InvariantCultureIgnoreCase))
                    {
                        page += getGenericGuts(item, currentIndexMakeManufacturer, false, true);
                    }
                }
            }
            page += getBottomOfPage();
            category = category.Replace(" ", "_").ToLower();
            model = model.Replace(" ", "_").ToLower();
            System.IO.File.WriteAllText(saveLocation + @"\" + model + "_" + category + ".html", page);
        }

        public void makeFlatCategoryPage(string category)
        {
            string page = getTopOfPage("", category);

            for (int i = 0; i < items.Count; i++)
            {
                Item item = (Item)items[i];
                for (int currentIndexMakeManufacturer = 0; currentIndexMakeManufacturer < item.getMakes().Count; currentIndexMakeManufacturer++)
                {
                    if (category.ToLower().Equals(item.category.ToLower(), StringComparison.InvariantCultureIgnoreCase))
                    {
                        page += getGenericGuts(item, currentIndexMakeManufacturer, false, true);
                    }
                }
            }
            page += getBottomOfPage();
            category = category.Replace(" ", "_").ToLower();
            System.IO.File.WriteAllText(saveLocation + @"\" + category + ".html", page);
        }

        public HashSet<string> getMakes()
        {
            return makes;
        }

        public int loadXls()
        {
            Net.SourceForge.Koogra.Excel.Worksheet worksheet = ExcelReader.loadFirstExcelSheetInBook(xlsFile);
            for (uint r = worksheet.Rows.MinRow + 2; r <= worksheet.Rows.MaxRow; ++r)
            {
                Net.SourceForge.Koogra.Excel.Row row = worksheet.Rows[r];
                Console.WriteLine("Reading Line number: " + r + 1);
                    Item i = new Item();
                    i.partNumber = row.Cells[0] == null ? "" : row.Cells[0].FormattedValue().Trim();
                    i.autoPartNumber = row.Cells[1] == null ? "" : row.Cells[1].FormattedValue().Trim();
                    foreach (string x in ((row.Cells[2] == null ? "" : row.Cells[2].FormattedValue()).Split(',')))
                    {
                        i.addMake(x.Trim());
                        makes.Add(x.Trim());
                    }
                    foreach (string x in (row.Cells[3].FormattedValue().Split(',')))
                    {
                        i.addManufacturer(x.Trim());
                    }
                    if (i.getMakes().Count != i.getManufacturers().Count)
                    {
                        MessageBox.Show("There is a problem in row " + (r+1) + " of the csv file.  The number of makes and manufacturers do not match: " + i.partNumber + " " + row.Cells[2].FormattedValue() + " " + row.Cells[3].FormattedValue());
                        return -1;
                    }
                    i.color = row.Cells[4] == null ? "" : row.Cells[4].FormattedValue().Trim();
                    i.application = row.Cells[5] == null ? "" : row.Cells[5].FormattedValue().Trim();
                    i.itemName = row.Cells[6] == null ? "" : row.Cells[6].FormattedValue().Trim();
                    i.itemNumber = row.Cells[7] == null ? "" : row.Cells[7].FormattedValue().Trim();
                    i.aevecoPartNumber = row.Cells[8] == null ? "" : row.Cells[8].FormattedValue().Trim();
                    i.imageLink = row.Cells[9] == null ? "" : row.Cells[9].FormattedValue().Trim();
                    i.imageTitle = row.Cells[10] == null ? "" : row.Cells[10].FormattedValue().Trim();
                    i.imageAlt = row.Cells[11] == null ? "" : row.Cells[11].FormattedValue().Trim();
                    i.addDimension(row.Cells[12] == null ? "" : row.Cells[12].FormattedValue().Trim());
                    i.addDimension(row.Cells[13] == null ? "" : row.Cells[13].FormattedValue().Trim());
                    i.addDimension(row.Cells[14] == null ? "" : row.Cells[14].FormattedValue().Trim());
                    i.addDimension(row.Cells[15] == null ? "" : row.Cells[15].FormattedValue().Trim());
                    i.makesAndModels = row.Cells[16] == null ? "" : row.Cells[16].FormattedValue().Trim();;
                    i.price1 = row.Cells[17] == null ? "" : row.Cells[17].FormattedValue().Trim();;
                    i.price10 = row.Cells[18] == null ? "" : row.Cells[18].FormattedValue().Trim();;
                    i.price20 = row.Cells[19] == null ? "" : row.Cells[19].FormattedValue().Trim();;
                    i.price50 = row.Cells[20] == null ? "" : row.Cells[20].FormattedValue().Trim();;
                    i.price100 = row.Cells[21] == null ? "" : row.Cells[21].FormattedValue().Trim();;
                    i.category = fixCapitals(row.Cells[22] == null ? "" : row.Cells[22].FormattedValue().Trim().Replace("_", " "));
                    items.Add(i);

                    if (i.itemNumber.Equals(""))
                        i.itemNumber = "&nbsp";
                    if (i.partNumber.Equals(""))
                        i.partNumber = "&nbsp";
                    for (int t = 0; t < i.getDimensions().Count; t++)
                    {
                        if (i.getDimensions()[t].Equals(""))
                            i.getDimensions()[t] = "&nbsp";
                    }
                    for (int t = 0; t < i.getMakes().Count; t++)
                    {
                        if (i.getMakes()[t].Equals(""))
                            i.getMakes()[t] = "&nbsp";
                    }
                    for (int t = 0; t < i.getManufacturers().Count; t++)
                    {
                        if (i.getManufacturers()[t].Equals(""))
                            i.getManufacturers()[t] = "&nbsp";
                    }
                    if (i.category.Equals(""))
                        i.category = "&nbsp";
                    if (i.makesAndModels.Equals(""))
                        i.makesAndModels = "&nbsp";
                    if (i.imageLink.Equals(""))
                        i.imageLink = "&nbsp";
                    if (i.application.Equals(""))
                        i.application = "&nbsp";
                    if (i.imageTitle.Equals(""))
                        i.imageTitle = "&nbsp";
                    if (i.imageAlt.Equals(""))
                        i.imageAlt = "&nbsp";
                    if (i.itemName.Equals(""))
                        i.itemName = "&nbsp";

                    if (!categories.Contains(i.category) && !"nbsp".Equals(i.category))
                        categories.Add(i.category);

                    //add mapping
                    for (int mIndex = 0; mIndex < i.getMakes().Count;         mIndex++)
                    {
                        if (!"&nbsp".Equals((string)i.getMakes()[mIndex], StringComparison.InvariantCultureIgnoreCase) && !"".Equals((string)i.getMakes()[mIndex], StringComparison.InvariantCultureIgnoreCase) && !"0".Equals((string)i.getMakes()[mIndex], StringComparison.InvariantCultureIgnoreCase) && !makeToCategories.ContainsKey((string)i.getMakes()[mIndex]))
                        {
                            ArrayList temp = new ArrayList();
                            temp.Add(i.category);
                            makeToCategories.Add((string)i.getMakes()[mIndex], temp);
                        }
                        else if (!"&nbsp".Equals((string)i.getMakes()[mIndex], StringComparison.InvariantCultureIgnoreCase) &&
                            !"".Equals((string)i.getMakes()[mIndex], StringComparison.InvariantCultureIgnoreCase) &&
                            !"0".Equals((string)i.getMakes()[mIndex], StringComparison.InvariantCultureIgnoreCase) &&
                            !makeToCategories[(string)i.getMakes()[mIndex]].Contains(i.category))
                        {
                            makeToCategories[(string)i.getMakes()[mIndex]].Add(i.category);
                        }
                    }                                                    
            }
            //sort the array
            Sort(items);
            return 1;
        }

        public int loadCsv()
        {
            int counter = 0;
            string line;

            // Read the file line by line
            System.IO.StreamReader file =
               new System.IO.StreamReader(csvFile);
            while ((line = file.ReadLine()) != null)
            {
                if (counter > 1) {
                    Console.WriteLine("Reading Line number: " + counter);
                    Item i = new Item();
                    //string[] values = Regex.Split(line, fieldDelimiter);
                    string[] values = line.Split(fieldDelimiter);
                    while (values.Length < 23)
                    {
                        string temp = file.ReadLine();
                        line = line + temp;
                        values = line.Split(fieldDelimiter);
                        //values = Regex.Split(line, fieldDelimiter);
                    }
                    if (values.Length > 23)
                    {
                        MessageBox.Show("There is a problem in row " + (counter+1) + " of the csv file.  Most likely a semi-colon");
                        return -1;
                    }
                    i.partNumber = values[0].Trim();
                    i.autoPartNumber = values[1].Trim();
                    foreach (string x in (values[2].Split(','))) {
                        i.addMake(x.Trim());
                        makes.Add(x.Trim());
                    }
                    foreach (string x in (values[3].Split(',')))
                    {
                        i.addManufacturer(x.Trim());
                    }
                    if (i.getMakes().Count != i.getManufacturers().Count)
                    {
                        MessageBox.Show("There is a problem in row " + (counter+1) + " of the csv file.  The number of makes and manufacturers do not match: " + i.partNumber + " " + values[2] + " " + values[3]);
                        return -1;
                    }
                    i.color = values[4].Trim();
                    i.application = values[5].Trim();
                    i.itemName = values[6].Trim();
                    i.itemNumber = values[7].Trim();
                    i.aevecoPartNumber = values[8].Trim();
                    i.imageLink = values[9].Trim();
                    i.imageTitle = values[10].Trim();
                    i.imageAlt = values[11].Trim();
                    i.addDimension(values[12].Trim());
                    i.addDimension(values[13].Trim());
                    i.addDimension(values[14].Trim());
                    i.addDimension(values[15].Trim());
                    i.makesAndModels = values[16].Trim();
                    i.price1 = values[17].Trim();
                    i.price10 = values[18].Trim();
                    i.price20 = values[19].Trim();
                    i.price50 = values[20].Trim();
                    i.price100 = values[21].Trim();
                    i.category = fixCapitals(values[22].Trim().Replace("_"," "));
                    items.Add(i);

                    if (i.itemNumber.Equals(""))
                        i.itemNumber = "&nbsp";
                    if (i.partNumber.Equals(""))
                        i.partNumber = "&nbsp";
                    for (int t = 0; t < i.getDimensions().Count; t++)
                    {
                        if (i.getDimensions()[t].Equals(""))
                            i.getDimensions()[t] = "&nbsp";
                    }
                    for (int t = 0; t < i.getMakes().Count; t++)
                    {
                        if (i.getMakes()[t].Equals(""))
                            i.getMakes()[t] = "&nbsp";
                    }
                    for (int t = 0; t < i.getManufacturers().Count; t++)
                    {
                        if (i.getManufacturers()[t].Equals(""))
                            i.getManufacturers()[t] = "&nbsp";
                    }
                    if (i.category.Equals(""))
                        i.category = "&nbsp";
                    if (i.makesAndModels.Equals(""))
                        i.makesAndModels = "&nbsp";
                    if (i.imageLink.Equals(""))
                        i.imageLink = "&nbsp";
                    if (i.application.Equals(""))
                        i.application = "&nbsp";
                    if (i.imageTitle.Equals(""))
                        i.imageTitle = "&nbsp";
                    if (i.imageAlt.Equals(""))
                        i.imageAlt = "&nbsp";
                    if (i.itemName.Equals(""))
                        i.itemName = "&nbsp";
                    
                    if (!categories.Contains(i.category) && !"nbsp".Equals(i.category))
                        categories.Add(i.category);

                    //add mapping
                    for (int mIndex = 0; mIndex < i.getMakes().Count; mIndex++)
                    {
                        if (!"&nbsp".Equals((string)i.getMakes()[mIndex], StringComparison.InvariantCultureIgnoreCase) && !"".Equals((string)i.getMakes()[mIndex], StringComparison.InvariantCultureIgnoreCase) && !"0".Equals((string)i.getMakes()[mIndex], StringComparison.InvariantCultureIgnoreCase) && !makeToCategories.ContainsKey((string)i.getMakes()[mIndex]))
                        {
                            ArrayList temp = new ArrayList();
                            temp.Add(i.category);
                            makeToCategories.Add((string)i.getMakes()[mIndex], temp);
                        }
                        else if (!"&nbsp".Equals((string)i.getMakes()[mIndex], StringComparison.InvariantCultureIgnoreCase) &&
                            !"".Equals((string)i.getMakes()[mIndex], StringComparison.InvariantCultureIgnoreCase) &&
                            !"0".Equals((string)i.getMakes()[mIndex], StringComparison.InvariantCultureIgnoreCase) &&
                            !makeToCategories[(string)i.getMakes()[mIndex]].Contains(i.category))
                        {
                            makeToCategories[(string)i.getMakes()[mIndex]].Add(i.category);
                        }
                    }
                }   

                counter++;
            }

            file.Close();

            //sort the array
            Sort(items);
            return 1;
        }

        private void Sort(ArrayList items)
        {
            int max = items.Count;

            for (int i = 1; i < max; i++)
            {
                int j = i;
                while (j > 0)
                {
                    if (((Item)items[j - 1]).partNumber.CompareTo(((Item)items[j]).partNumber) > 0)
                    {
                        object temp = items[j - 1];
                        items[j - 1] = items[j];
                        items[j] = temp;
                        j--;
                    }
                    else
                        break;
                }
            }
        }

        private string getTopOfPage(string make, string application)
        {
            string output = "";

            int counter = 0;
            string line;

            // Read the file and display it line by line.
            System.IO.StreamReader file =
               new System.IO.StreamReader(topOfPageFile);
            while ((line = file.ReadLine()) != null)
            {
                
                if (line.Trim().Equals("<h1>Insert The Title Here</h1>"))
                {
                    output += "<h1>" + make + " " + application + "</h1>";
                }
                else if (counter == 5)
                {
                    output += "<title>" + make + " " + application + "</title>\n";
                    output += "<meta name=\"description\" content=\"Lilparts.com little hard to find " + make + " " + application + " for Automobile Car Truck SUV RV Motorcycle and Boats\"/>\n";
                    output += "<meta name=\"keywords\" content=\"" + make + " Automobile Car Truck SUV RV Motorcycle Boat Automotive " + application + "\"/>\n";
                    output += line + "\n";
                    
                }
                else
                {
                    output += line + "\n";
                }

                counter++;
            }

            file.Close();

            return output;
        }

        private string getTopOfPage(string make, string keywordString, string application)
        {
            string output = "";

            int counter = 0;
            string line;

            // Read the file and display it line by line.
            System.IO.StreamReader file =
               new System.IO.StreamReader(topOfPageFile);
            while ((line = file.ReadLine()) != null)
            {

                if (line.Trim().Equals("<h1>Insert The Title Here</h1>"))
                {
                    output += "<h1>" + make + " " + application + "</h1>";
                }
                else if (counter == 5)
                {
                    output += "<title>" + make + " " + application + "</title>\n";
                    output += "<meta name=\"description\" content=\"Lilparts.com little hard to find " + keywordString + " " + application + " for Automobile Car Truck SUV RV Motorcycle and Boats\"/>\n";
                    output += "<meta name=\"keywords\" content=\"" + keywordString + " Automobile Car Truck SUV RV Motorcycle Boat Automotive " + application + "\"/>\n";
                    output += line + "\n";

                }
                else
                {
                    output += line + "\n";
                }

                counter++;
            }

            file.Close();

            return output;
        }

        private string getGenericGuts(Item item, int makeMnfcIndex, Boolean withAeveco, Boolean withLink)
        {
            string output = "<li>\n";
            output += "<a title=\"" + item.imageTitle + "\"><img alt=\"" + item.imageAlt + "\"" + "\n";
            output += "class=\"zit\" src=\"" + item.imageLink + "\" /></a>" + "\n";
            output += "<div class=\"container\">";
            output += "<div class=\"table table1\">" + "\n";
            output += "<div class=\"row row1\">" + "\n";
            output += "<span class=\"cell colspanned\">" + item.partNumber + "</span>" + "\n";
            output += "</div>" + "\n";
            output += "</div>" + "\n";
            output += "<div class=\"table table2\">" + "\n";
            output += "<div class=\"row row1\">" + "\n";
            output += "<span class=\"cell colspanned\">" + item.getMakes()[makeMnfcIndex] + "</span>" + "\n";
            output += "</div>" + "\n";
            output += "</div>" + "\n";
            output += "<div class=\"table table3\">" + "\n";
            output += "<div class=\"row row1\">" + "\n";
            output += "<span class=\"cell colspanned\">" + item.getManufacturers()[makeMnfcIndex] + "</span>" + "\n";
            output += "</div>" + "\n";
            output += "</div>" + "\n";
            if (withAeveco)
            {
                output += "<div class=\"table table3\">" + "\n";
                output += "<div class=\"row row1\">" + "\n";
                output += "<span class=\"cell colspanned\">" + item.aevecoPartNumber + "</span>" + "\n";
                output += "</div>" + "\n";
                output += "</div>" + "\n";
            }
            if (withLink)
            {
                output += "<div class=\"table table4\">" + "\n";
                output += "<div class=\"row row1\">" + "\n";
                output += "<span class=\"cell colspanned\"><a href=\"" + item.getPartLink(makeMnfcIndex) + "\">More Details</a></span>" + "\n";
                output += "</div>" + "\n";
                output += "</div>" + "\n";
            }
            output += "<div class=\"table table5\">" + "\n";
            output += "<div class=\"row row1\">" + "\n";
            output += "<span class=\"cell colspanned\">" + item.application + "</span>" + "\n";
            output += "</div>" + "\n";
            output += "</div>" + "\n";
            output += "<div class=\"table table6\">" + "\n";
            output += "<div class=\"row row1\">" + "\n";
            output += "<span class=\"cell col1\">" + item.getDimensions()[0] + "</span>" + "\n";
            output += "<span class=\"cell col2\"></span>" + "\n";
            output += "<span class=\"cell col3\">" + item.getDimensions()[1] + "</span>" + "\n";
            output += "</div>" + "\n";
            output += "</div>" + "\n";
            output += "<div class=\"table table7\">" + "\n";
            output += "<div class=\"row row1\">" + "\n";
            output += "<span class=\"cell col1\">" + item.getDimensions()[2] + "</span>" + "\n";
            output += "<span class=\"cell col2\"></span>" + "\n";
            output += "<span class=\"cell col3\">" + item.getDimensions()[3] + "</span>" + "\n";
            output += "</div>" + "\n";
            output += "</div>" + "\n";
            output += "<div class=\"table table8\">" + "\n";
            output += "<div class=\"row row1\">" + "\n";
            output += "<span class=\"cell colspanned\">" + item.makesAndModels + "</span>" + "\n";
            output += "</div>" + "\n";
            output += "</div>" + "\n";
            output += "<div class=\"table table9\">" + "\n";
            output += "<div class=\"row row1\">" + "\n";
            output += "<span class=\"cell colspanned\">" + item.category + "</span>" + "\n";
            output += "</div>" + "\n";
            output += "</div>" + "\n";
            output += "<div class=\"table table10\">" + "\n";
            output += "<div class=\"row1\">" + "\n";
            output += "<span class=\"cell colspanned\">" + "\n";
            output += "Select Quantity" + "\n";

            output += "<form target=\"paypal\" action=\"https://www.paypal.com/cgi-bin/webscr\" method=\"post\">" + "\n";
            output += "<input type=\"hidden\" name=\"business\" value=\"superlumination@cox.net\"/>" + "\n";
            output += "<input type=\"hidden\" name=\"cmd\" value=\"_cart\"/>" + "\n";
            output += "<input type=\"hidden\" name=\"add\" value=\"1\"/>" + "\n";
            output += "<input type=\"hidden\" name=\"item_name\" value=\"" + item.itemName + "\"/>" + "\n";
            output += "<input type=\"hidden\" name=\"item_number\" value=\"" + item.itemNumber + "\"/>" + "\n";
            output += "<input type=\"hidden\" name=\"currency_code\" value=\"USD\"/>" + "\n";
            output += "<input name=\"image_url\" type=\"hidden\" value=\"http://www.autolumination.com/backgrounds/autolumination2.gif\"/>" + "\n";
            output += "<input name=\"return\" type=\"hidden\" value=\"http://www.autolumination.com/order.htm/\"/>" + "\n";
            output += "<input name=\"cancel_return\" type=\"hidden\" value=\"http://www.autolumination.com\"/>" + "\n";
            output += "</span>" + "\n";
            output += "<span class=\"cell cart1\">" + "\n";
            output += "<input type=\"hidden\" name=\"on1\" value=\"Size\"/>" + "\n";
            output += "<select name=\"os1\">" + "\n";
            if (!item.price1.Equals("NA", StringComparison.InvariantCultureIgnoreCase))
            {
                if (item.price1[0] == '$')
                    output += "<option value=\"1-Pc.\">1-Pc. - " + item.price1 + " USD</option>" + "\n";
                else
                    output += "<option value=\"1-Pc.\">1-Pc. - $" + item.price1 + " USD</option>" + "\n";
            }
            if (!item.price10.Equals("NA", StringComparison.InvariantCultureIgnoreCase))
            {
                if (item.price10[0] == '$')
                    output += "<option value=\"10-Pack\">10-Pack - " + item.price10 + " USD</option>" + "\n";
                else
                    output += "<option value=\"10-Pack\">10-Pack - $" + item.price10 + " USD</option>" + "\n";
            }
            if (!item.price20.Equals("NA", StringComparison.InvariantCultureIgnoreCase))
            {
                if (item.price20[0] == '$')
                    output += "<option value=\"20-Pack\">20-Pack - " + item.price20 + " USD</option>" + "\n";
                else
                    output += "<option value=\"20-Pack\">20-Pack - $" + item.price20 + " USD</option>" + "\n";
            }
            if (!item.price50.Equals("NA", StringComparison.InvariantCultureIgnoreCase))
            {
                if (item.price50[0] == '$')
                    output += "<option value=\"50-Pack\">50-Pack - " + item.price50 + " USD</option>" + "\n";
                else
                    output += "<option value=\"50-Pack\">50-Pack - $" + item.price50 + " USD</option>" + "\n";
            }
            if (!item.price100.Equals("NA", StringComparison.InvariantCultureIgnoreCase))
            {
                if (item.price100[0] == '$')
                    output += "<option value=\"100-Pack\">100-Pack - " + item.price100 + " USD</option>" + "\n";
                else
                    output += "<option value=\"100-Pack\">100-Pack - $" + item.price100 + " USD</option>" + "\n";
            }
            output += "</select>" + "\n";
            output += "</span>" + "\n";
            output += "<input type=\"hidden\" name=\"option_index\" value=\"1\"/>" + "\n";
            if (!item.price1.Equals("NA", StringComparison.InvariantCultureIgnoreCase))
            {
                output += "<input type=\"hidden\" name=\"option_select0\" value=\"1-Pc.\"/>" + "\n";
                if (item.price1[0] == '$')
                    output += "<input type=\"hidden\" name=\"option_amount0\" value=\"" + item.price1.Substring(1) + "\"/>" + "\n";
                else
                    output += "<input type=\"hidden\" name=\"option_amount0\" value=\"" + item.price1 + "\"/>" + "\n";
            }
            if (!item.price10.Equals("NA", StringComparison.InvariantCultureIgnoreCase))
            {
                output += "<input type=\"hidden\" name=\"option_select1\" value=\"10-Pack\"/>" + "\n";
                if (item.price10[0] == '$')
                    output += "<input type=\"hidden\" name=\"option_amount1\" value=\"" + item.price10.Substring(1) + "\"/>" + "\n";
                else
                    output += "<input type=\"hidden\" name=\"option_amount1\" value=\"" + item.price10 + "\"/>" + "\n";
            }
            if (!item.price20.Equals("NA", StringComparison.InvariantCultureIgnoreCase))
            {
                output += "<input type=\"hidden\" name=\"option_select2\" value=\"20-Pack\"/>" + "\n";
                if (item.price20[0] == '$')
                    output += "<input type=\"hidden\" name=\"option_amount2\" value=\"" + item.price20.Substring(1) + "\"/>" + "\n";
                else
                    output += "<input type=\"hidden\" name=\"option_amount2\" value=\"" + item.price20 + "\"/>" + "\n";
            }
            if (!item.price50.Equals("NA", StringComparison.InvariantCultureIgnoreCase))
            {
                output += "<input type=\"hidden\" name=\"option_select3\" value=\"50-Pack\"/>" + "\n";
                if (item.price50[0] == '$')
                    output += "<input type=\"hidden\" name=\"option_amount3\" value=\"" + item.price50.Substring(1) + "\"/>" + "\n";
                else
                    output += "<input type=\"hidden\" name=\"option_amount3\" value=\"" + item.price50 + "\"/>" + "\n";
            }
            if (!item.price100.Equals("NA", StringComparison.InvariantCultureIgnoreCase))
            {
                output += "<input type=\"hidden\" name=\"option_select4\" value=\"100-Pack\"/>" + "\n";
                if (item.price100[0] == '$')
                    output += "<input type=\"hidden\" name=\"option_amount4\" value=\"" + item.price100.Substring(1) + "\"/>" + "\n";
                else
                    output += "<input type=\"hidden\" name=\"option_amount4\" value=\"" + item.price100 + "\"/>" + "\n";
            }
            output += "<span class=\"cell cart3\">" + "\n";
            output += "<input type=\"image\" name=\"submit\" src=\"https://www.paypal.com/en_US/i/btn/btn_cart_LG.gif\">" + "\n";
            output += "<img src=\"https://www.paypal.com/en_US/i/scr/pixel.gif\"/>" + "\n";
            output += "</span>" + "\n";
            output += "</form>" + "\n";
            output += "</span>" + "\n";
            output += "</div>" + "\n";
            output += "</div>" + "\n";
            output += "</li>" + "\n";
            output += "\n";
            return output;
        }

        private string fixCapitals(string words)
        {
            char[] characters = words.Trim().ToCharArray();
            words = "";
            characters[0] = ("" + characters[0]).ToUpper()[0];
            for (int i = 0; i < characters.Length; i++)
            {
                if (characters[i] == 32)
                {
                    words += characters[i++];
                    characters[i] = ("" + characters[i]).ToUpper()[0];
                    
                }
                words += characters[i];
            }
                return words;
        }

        private string getBottomOfPage()
        {
            string output = "";
            string line;

            // Read the file and display it line by line.
            System.IO.StreamReader file =
               new System.IO.StreamReader(bottomOfPageFile);
            while ((line = file.ReadLine()) != null)
            {
                    output += line + "\n";
            }

            file.Close();

            return output;
        }

        public void makeSqlCode()
        {
            //(8, '#194, #Interior, #Signal Marker, #smt II, #smt III, #wild psycho, #4 star, #supernova, #T10, #194 socket, #194 sockets, #2821', 'autolumination.com/194.htm'),
            
            string output = "INSERT INTO `tbl_search_data` (`search_data_id`, `key`, `url`) VALUES\n";
            int index = 1;
            foreach (Item i in items)
            {
                if (!"".Equals(i.partNumber) && i.getMakes()[0] != null && i.getManufacturers()[0] != null)
                {
                    string manufacturer = ((string)i.getManufacturers()[0]);
                    output += "(" + index + ", '";
                    output += "#" + i.partNumber + ", ";
                    output += "#" + i.partNumber.ToLower() + ", ";
                    output += "#" + i.partNumber.ToLower().Replace("-", "") + ", ";
                    output += "#" + i.partNumber.Substring(i.partNumber.Length - 4) + ", ";
                    foreach (string aeveco in i.aevecoPartNumber.Split(' '))
                    {
                        if (!"aeveco".Equals(aeveco, StringComparison.InvariantCultureIgnoreCase))
                        {
                            output += "#" + aeveco + ", ";
                        }
                    }
                    output += "#" + i.partNumber.Replace("-", "");
                    output += ",', '";
                    output += i.getPartNumberLink();
                    output += "'),\n";
                    index++;
                }
                for (int j = 0; j < i.getMakes().Count; j++)
                {
                    if (!"0".Equals(i.getManufacturers()[j]) && !"&nbsp".Equals(i.getManufacturers()[j]))
                    {
                        string manufacturer = ((string)i.getManufacturers()[j]);
                        output += "(" + index + ", '";
                        output += "#" + manufacturer.ToLower() + ", ";
                        output += "#" + manufacturer + ", ";
                        output += "#" + manufacturer.Replace("-", "").ToLower() + ", ";
                        output += "#" + manufacturer.Replace("-", "");
                        output += ",', '";
                        output += i.getPartLink(j).Substring(11);
                        output += "'),\n";
                        index++;
                    }
                }
                

                
            }
            output = output.Substring(0, output.Length - 2);
            output += ";";

            System.IO.File.WriteAllText(saveLocation + @"\sqlFile.txt", output);
            
        }    
    }
}
