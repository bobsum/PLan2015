using System;
using System.IO;
using System.Xml.Serialization;

namespace Bismuth.Framework.Xml
{
    /// <summary>
    /// A generic XmlSerializer.
    /// This new serializer also adds the ability load and save directly to files.
    /// </summary>
    /// <typeparam name="T">Type of which the xml serializer can serialize.</typeparam>
    public class XmlSerializer<T> : XmlSerializer
    {
        public XmlSerializer() : base(typeof(T)) { }
        public XmlSerializer(params Type[] extraTypes) : base(typeof(T), extraTypes) { }

        public T Load(string fileName)
        {
            return Load(fileName, FileMode.OpenOrCreate, default(T));
        }

        public T Load(string fileName, T defaultValue)
        {
            return Load(fileName, FileMode.OpenOrCreate, defaultValue);
        }

        public T Load(string fileName, FileMode fileMode)
        {
            return Load(fileName, fileMode, default(T));
        }

        /// <summary>
        /// Deserializes the content of a file to an object.
        /// </summary>
        /// <param name="fileName">The file name.</param>
        /// <param name="fileMode">The file mode.</param>
        /// <param name="defaultValue">The default value which is returned if the file does noe exist or is empty.</param>
        /// <returns>The deserialized content of a file.</returns>
        public T Load(string fileName, FileMode fileMode, T defaultValue)
        {
            using (FileStream file = new FileStream(fileName, fileMode, FileAccess.Read))
            {
                if (file.Length > 0)
                {
                    return (T)Deserialize(file);
                }
                else
                {
                    return defaultValue;
                }
            }
        }

        /// <summary>
        /// Saves a value or object to a file.
        /// </summary>
        /// <param name="fileName">The object to serialize.</param>
        /// <param name="value">The object to serialize.</param>
        public void Save(string fileName, T value)
        {
            using (FileStream file = new FileStream(fileName, FileMode.Create))
            {
                Serialize(file, value);
                file.Flush();
            }
        }
    }
}
