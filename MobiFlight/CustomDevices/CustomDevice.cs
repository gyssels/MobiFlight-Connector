﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobiFlight.CustomDevices
{
    /// <summary>
    /// General device information properties.
    /// </summary>
    public class Info
    {
        /// <summary>
        /// The label of the custom device in the UI.
        /// </summary>
        public String Label;

        /// <summary>
        /// A unique identifier for the device.
        /// </summary>
        public String Type;

        /// <summary>
        /// The author of this custom device.
        /// </summary>
        public String Author;

        /// <summary>
        /// The URL of the custom device. Ideally a github repository.
        /// </summary>
        public String URL;

        /// <summary>
        /// The version of this custom device
        /// </summary>
        public String Version;
    }

    /// <summary>
    /// Config properties of this custom device used in the device settings panel.
    /// </summary>
    public class Config
    {
        /// <summary>
        /// A an array of pins required for connecting the custom device.
        /// </summary>
        public List<String> Pins;


        /// <summary>
        /// Indicates whether it uses i2c address or pins.
        /// </summary>
        public bool isI2C = false;
    }

    /// <summary>
    /// A message type to distinguish commands
    /// </summary>
    public class MessageType
    {
        /// <summary>
        /// The identifier for the message type. Unique in the scope of the device.
        /// </summary>
        public string Id;

        /// <summary>
        /// The label for the message type which is used in the config wizard UI.
        /// </summary>
        public String Label;

        /// <summary>
        /// The description for the message type and instructions how to use it.
        /// </summary>
        public String Description;

        public override string ToString()
        {
            return $"{Id}";
        }
    }

    public class CustomDevice
    {
        /// <summary>
        /// General device information properties.
        /// </summary>
        public Info Info;

        /// <summary>
        /// Connection settings for the board.
        /// </summary>
        public Config Config;

        /// <summary>
        /// List of MessageTypes supported by the device.
        /// </summary>
        public List<MessageType> MessageTypes = new List<MessageType>();

        public override string ToString()
        {
            return $"{Info.Label} ({Info.Version})";
        }
    }
}