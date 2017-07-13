using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace TradingAnalyzer.Framework
{
    #region EnumExtensions
    public static class EnumExtensions
    {
        #region GetDisplay
        public static String GetDisplay(this Enum value)
        {
            // Get the type
            Type type = value.GetType();

            // Get fieldinfo for this type
            FieldInfo fieldInfo = type.GetField(value.ToString());

            // Get the stringvalue attributes
            DisplayAttribute[] attribs = fieldInfo.GetCustomAttributes(
                typeof(DisplayAttribute), false) as DisplayAttribute[];

            // Return the first if there was a match.
            return attribs.Length > 0 ? attribs[0].Display : String.Empty;
        }
        #endregion
    }
    #endregion

    #region Attributes
    #region DisplayAttribute
    /// <summary>
    /// This attribute is used to represent a string value
    /// for a value in an enum.
    /// </summary>
    public class DisplayAttribute : Attribute
    {
        #region Properties
        /// <summary>
        /// Holds the stringvalue for a value in an enum.
        /// </summary>
        public String Display { get; protected set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor used to init a StringValue Attribute
        /// </summary>
        /// <param name="value"></param>
        public DisplayAttribute(String value)
        {
            this.Display = value;
        }
        #endregion
    }
    #endregion
    #endregion
}
