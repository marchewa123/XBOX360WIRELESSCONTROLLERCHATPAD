﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Data;

namespace Driver360WChatPad
{
    public class ChatpadController
    {
        public DataTable dt = new DataTable("ChatPadKey");
        private DataView dv;
        public ChatpadController()
        {
            try
            {
                dt.Columns.Add(new DataColumn("ID", typeof(int)));
                dt.Columns.Add(new DataColumn("Map", typeof(string)));
                dt.Columns.Add(new DataColumn("OrangeModifier", typeof(string)));
                dt.Columns.Add(new DataColumn("GreenModifier", typeof(string)));
                dt.ReadXml("ChatPadMappings.xml");
                dv = new DataView(dt);
            }
            catch (Exception e)
            {
                ErrorLogging.WriteLogEntry(String.Format("General error during XML Mapping initializing ChatpadController class: {0}", e.InnerException), ErrorLogging.LogLevel.Fatal);
            }
        }
        public string GetChatPadKeyValue(string value,bool orangeModifer, bool greenModifer) 
        {
            try
            {
                dv.RowFilter = String.Format("(ID={0})", Convert.ToInt16(value));
                if (orangeModifer)
                {
                    return dv[0]["OrangeModifier"].ToString();
                }
                else if (greenModifer)
                {
                    return dv[0]["GreenModifier"].ToString();
                }
                else
                {
                    return dv[0]["Map"].ToString();
                }
            }
            catch (IndexOutOfRangeException iorex)
            {
                //Value from chat pad not recognized, should not be possible, yeah right
                ErrorLogging.WriteLogEntry(String.Format("Chatpad data not recognized: {0}", iorex.InnerException), ErrorLogging.LogLevel.Error);
                return "";
            }
        }
    }
}
