using Eto.Drawing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Eto.Forms
{
	/// <summary>
	/// Interface to allow you to share common code with <see cref="Clipboard"/> and <see cref="DataObject"/>.
	/// </summary>
	public interface IDataObject
	{
		/// <summary>
		/// Gets the type id's for each type of data in the data object.
		/// </summary>
		/// <value>The content types in the data object.</value>
		string[] Types { get; }

		/// <summary>
		/// Gets a value indicating whether this <see cref="T:Eto.Forms.IDataObject"/> contains a value for <see cref="Text"/>.
		/// </summary>
		/// <value><c>true</c> if the data object contains text; otherwise, <c>false</c>.</value>
		bool ContainsText { get; }

		/// <summary>
		/// Gets a value indicating whether this <see cref="T:Eto.Forms.IDataObject"/> contains a value for <see cref="Html"/>.
		/// </summary>
		/// <value><c>true</c> if the data object contains html; otherwise, <c>false</c>.</value>
		bool ContainsHtml { get; }

		/// <summary>
		/// Gets a value indicating whether this <see cref="T:Eto.Forms.IDataObject"/> contains a value for <see cref="Image"/>.
		/// </summary>
		/// <value><c>true</c> if the data object contains an image; otherwise, <c>false</c>.</value>
		bool ContainsImage { get; }

		/// <summary>
		/// Gets a value indicating whether this <see cref="T:Eto.Forms.IDataObject"/> contains a value for <see cref="Uris"/>.
		/// </summary>
		/// <remarks>
		/// This can be a mix of both URL and File objects.  You can use <see cref="Uri.IsFile"/> to test for that.
		/// On some platforms, (e.g. windows), only a single URL can be retrieved or set.
		/// </remarks>
		/// <value><c>true</c> if the data object contains uris; otherwise, <c>false</c>.</value>
		bool ContainsUris { get; }

		/// <summary>
		/// Sets a string into the data object with the specified type identifier.
		/// </summary>
		/// <remarks>
		/// This is useful when setting alternate string values into the data object that are not plain text.
		/// If you are storing plain text, use the <see cref="Text"/> property instead.
		/// </remarks>
		/// <seealso cref="GetString"/>
		/// <param name="value">Value to set in the data object.</param>
		/// <param name="type">Type identifier that was used to store the data.</param>
		void SetString(string value, string type);

		/// <summary>
		/// sets a data array into the data object with the specified type identifier.
		/// </summary>
		/// <param name="value">Data to store in the data object.</param>
		/// <param name="type">Type identifier to store the data.</param>
		void SetData(byte[] value, string type);

		/// <summary>
		/// Gets a string from the data object with the specified type identifier.
		/// </summary>
		/// <returns>The string.</returns>
		/// <seealso cref="SetString"/>
		/// <param name="type">Type identifier that was used to store the data.</param>
		string GetString(string type);

		/// <summary>
		/// Gets a value indicating that data with the specified type is contained in the data object.
		/// </summary>
		/// <returns><c>true</c> if the data object contains the specified type, <c>false</c> otherwise.</returns>
		/// <param name="type">Type to test.</param>
		bool Contains(string type);

		/// <summary>
		/// Gets a data array from the data object with the specified type identifier.
		/// </summary>
		/// <returns>The data array, or null if not found in the data object.</returns>
		/// <seealso cref="SetData"/>
		/// <param name="type">Type identifier that was used to store the data.</param>
		byte[] GetData(string type);


		/// <summary>
		/// Gets or sets the plain text in the data object.
		/// </summary>
		/// <value>The plain text in the data object, or null if no plain text string in the data object.</value>
		string Text { get; set; }

		/// <summary>
		/// Gets or sets html text in the data object.
		/// </summary>
		/// <value>The html value in the data object, or null if no html in the data object.</value>
		string Html { get; set; }

		/// <summary>
		/// Gets or sets an image in the data object.
		/// </summary>
		/// <value>The image in the data object, or null if no image is in the data object.</value>
		Image Image { get; set; }

		/// <summary>
		/// Gets or sets the Uri's of the files in the data object.
		/// </summary>
		/// <remarks>
		/// This can be a mix of both URL and File objects.  You can use <see cref="Uri.IsFile"/> to test for that.
		/// On some platforms, (e.g. windows), only a single URL can be retrieved or set.
		/// </remarks>
		/// <value>The uris of the files, or null if no files are in the data object.</value>
		Uri[] Uris { get; set; }

		/// <summary>
		/// Clears the data object completely of all values
		/// </summary>
		void Clear();

	}

	/// <summary>
	/// Drag/Drop action data.
	/// </summary>
	[Handler(typeof(IHandler))]
	public class DataObject : Widget, IDataObject
	{
		new IHandler Handler => (IHandler)base.Handler;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Eto.Forms.DataObject"/> class.
		/// </summary>
		public DataObject()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Eto.Forms.DataObject"/> class.
		/// </summary>
		/// <param name="handler">Handler implementation.</param>
		public DataObject(IHandler handler) : base(handler)
		{
		}

		/// <summary>
		/// Gets the types of data in the data object to be used with <see cref="GetString"/> and <see cref="GetData"/>
		/// </summary>
		/// <value>The types.</value>
		public string[] Types => Handler.Types;


		/// <summary>
		/// Gets a value indicating whether this <see cref="T:Eto.Forms.DataObject"/> contains a value for <see cref="Text"/>.
		/// </summary>
		/// <value><c>true</c> if the data object contains text; otherwise, <c>false</c>.</value>
		public bool ContainsText => Handler.ContainsText;

		/// <summary>
		/// Gets a value indicating whether this <see cref="T:Eto.Forms.DataObject"/> contains a value for <see cref="Html"/>.
		/// </summary>
		/// <value><c>true</c> if the data object contains html; otherwise, <c>false</c>.</value>
		public bool ContainsHtml => Handler.ContainsHtml;

		/// <summary>
		/// Gets a value indicating whether this <see cref="T:Eto.Forms.DataObject"/> contains a value for <see cref="Image"/>.
		/// </summary>
		/// <value><c>true</c> if the data object contains an image; otherwise, <c>false</c>.</value>
		public bool ContainsImage => Handler.ContainsImage;

		/// <summary>
		/// Gets a value indicating whether this <see cref="T:Eto.Forms.DataObject"/> contains a value for <see cref="Uris"/>.
		/// </summary>
		/// <remarks>
		/// This can be a mix of both URL and File objects.  You can use <see cref="Uri.IsFile"/> to test for that.
		/// On some platforms, (e.g. windows), only a single URL can be retrieved or set.
		/// </remarks>
		/// <value><c>true</c> if the data object contains uris; otherwise, <c>false</c>.</value>
		public bool ContainsUris => Handler.ContainsUris;

		/// <summary>
		/// Sets a string into the data object with the specified custom type.
		/// </summary>
		/// <param name="value">Value to store.</param>
		/// <param name="type">Type of the data.</param>
		public void SetString(string value, string type) => Handler.SetString(value, type);

		/// <summary>
		/// Sets a data stream into the clipboard with the specified type identifier.
		/// </summary>
		/// <param name="stream">Stream to store in the clipboard.</param>
		/// <param name="type">Type identifier when retrieving the stream.</param>
		public void SetDataStream(Stream stream, string type)
		{
			if (stream.CanSeek)
			{
				var buffer = new byte[stream.Length];
				if (stream.Position != 0)
					stream.Seek(0, SeekOrigin.Begin);
				stream.Read(buffer, 0, buffer.Length);
				SetData(buffer, type);
			}
			else
			{
				using (var ms = new MemoryStream())
				{
					stream.CopyTo(ms);
					SetData(ms.ToArray(), type);
				}
			}
		}

		static Type s_BinaryFormatterType = Type.GetType("System.Runtime.Serialization.Formatters.Binary.BinaryFormatter")
			?? Type.GetType("System.Runtime.Serialization.Formatters.Binary.BinaryFormatter, System.Runtime.Serialization.Formatters")
			?? Type.GetType("System.Runtime.Serialization.Formatters.Binary.BinaryFormatter, netstandard");
		static MethodInfo s_SerializeMethod = s_BinaryFormatterType?.GetRuntimeMethod("Serialize", new[] { typeof(Stream), typeof(object) });
		static MethodInfo s_DeserializeMethod = s_BinaryFormatterType?.GetRuntimeMethod("Deserialize", new[] { typeof(Stream) });

		/// <summary>
		/// Sets the <paramref name="value"/> into the data object with the specified <paramref name="type"/> using serialization.
		/// </summary>
		/// <remarks>
		/// The object specified must be serializable.
		/// </remarks>
		/// <param name="value">Serializable value to set as a value in the data object</param>
		/// <param name="type">Type identifier to set the value for</param>
		public void SetObject(object value, string type)
		{
			if (s_BinaryFormatterType == null || s_SerializeMethod == null)
				throw new InvalidOperationException("Could not create an instance of BinaryFormatter");
			using (var stream = new MemoryStream())
			{
				var binaryFormatter = Activator.CreateInstance(s_BinaryFormatterType);
				s_SerializeMethod.Invoke(binaryFormatter, new object[] { stream, value });
				SetDataStream(stream, type);
			}
		}

		/// <summary>
		/// Gets a serialized value with the specified <paramref name="type"/> identifier.
		/// </summary>
		/// <param name="type">type identifier to get the value for.</param>
		/// <returns>Value of the object if deserializable, otherwise null.</returns>
		public object GetObject(string type)
		{
			if (s_BinaryFormatterType == null || s_DeserializeMethod == null)
				throw new InvalidOperationException("Could not create an instance of BinaryFormatter");
			var stream = GetDataStream(type);
			if (stream == null)
				return null;
			var binaryFormatter = Activator.CreateInstance(s_BinaryFormatterType);
			return s_DeserializeMethod.Invoke(binaryFormatter, new object[] { stream });
		}

		/// <summary>
		/// Sets a data array for the specified type identifier.
		/// </summary>
		/// <param name="value">byte array to set.</param>
		/// <param name="type">Type identifier.</param>
		public void SetData(byte[] value, string type) => Handler.SetData(value, type);

		/// <summary>
		/// Gets a value indicating that data with the specified type is contained in the data object.
		/// </summary>
		/// <returns><c>true</c> if the data object contains the specified type, <c>false</c> otherwise.</returns>
		/// <param name="type">Type to test.</param>
		public bool Contains(string type) => Handler.Contains(type);

		/// <summary>
		/// Gets a string from the data object with the specified type identifier.
		/// </summary>
		/// <returns>The string value, or null if it does not exist in the data object.</returns>
		/// <param name="type">Type identifier for the string.</param>
		public string GetString(string type) => Handler.GetString(type);

		/// <summary>
		/// Gets the data stream with the specified type identifier.
		/// </summary>
		/// <returns>The data stream if found, or null otherwise.</returns>
		/// <seealso cref="SetDataStream"/>
		/// <param name="type">Type identifier that was used to store the data.</param>
		public Stream GetDataStream(string type)
		{
			var buffer = GetData(type);
			return buffer == null ? null : new MemoryStream(buffer, false);
		}

		/// <summary>
		/// Gets a byte array from the data object with the specified type identifier.
		/// </summary>
		/// <returns>The byte data.</returns>
		/// <param name="type">Type identifier to get the data for.</param>
		public byte[] GetData(string type) => Handler.GetData(type);

		/// <summary>
		/// Gets or sets the plain text in the data object.
		/// </summary>
		/// <value>The plain text in the data object, or null if the data object contains no text.</value>
		public string Text
		{
			get { return Handler.Text; }
			set { Handler.Text = value; }
		}

		/// <summary>
		/// Gets or sets html text in the data object.
		/// </summary>
		/// <value>The html value in the data object, or null if the data object contains no html.</value>
		public string Html
		{
			get { return Handler.Html; }
			set { Handler.Html = value; }
		}

		/// <summary>
		/// Gets or sets an image in the data object.
		/// </summary>
		/// <value>The image in the data object, or null if the data object contains no image.</value>
		public Image Image
		{
			get { return Handler.Image; }
			set { Handler.Image = value; }
		}

		/// <summary>
		/// Gets or sets the Uri's of the files in the data object
		/// </summary>
		/// <value>The uris of the files, or null if no files are in the data object.</value>
		public Uri[] Uris
		{
			get { return Handler.Uris; }
			set { Handler.Uris = value; }
		}

		/// <summary>
		/// Clears the data object
		/// </summary>
		public void Clear() => Handler.Clear();

		/// <summary>
		/// Handler interface for platform implementations of <see cref="DataObject"/>
		/// </summary>
		public new interface IHandler : Widget.IHandler, IDataObject
		{

		}
	}
}
