#region Copyright & License
//
// Copyright 2001-2004 The Apache Software Foundation
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
#endregion

using System;
using System.IO;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

using log4net.Config;
using log4net.Util;
using log4net.Layout;
using log4net.Core;
using log4net.Appender;
using log4net.Repository;

using log4net.Tests.Appender;

using NUnit.Framework;

namespace log4net.Tests.Util
{
	/// <summary>
	/// Used for internal unit testing the <see cref="PropertiesDictionary"/> class.
	/// </summary>
	/// <remarks>
	/// Used for internal unit testing the <see cref="PropertiesDictionary"/> class.
	/// </remarks>
	[TestFixture] public class PropertiesDictionaryTest
	{
		[Test] public void TestSerialization()
		{
			PropertiesDictionary pd = new PropertiesDictionary();

			for(int i=0; i<10; i++)
			{
				pd[i.ToString()] = i;
			}

			Assertion.AssertEquals("Dictionary should have 10 items", 10, pd.Count);

			// Serialize the properties into a memory stream
			BinaryFormatter formatter = new BinaryFormatter();
			MemoryStream memory = new MemoryStream();
			formatter.Serialize(memory, pd);

			// Deserialize the stream into a new properties dictionary
			memory.Position = 0;
			PropertiesDictionary pd2 = (PropertiesDictionary)formatter.Deserialize(memory);

			Assertion.AssertEquals("Deserialized Dictionary should have 10 items", 10, pd2.Count);

			foreach(string key in pd.GetKeys())
			{
				Assertion.AssertEquals("Check Value Persisted for key ["+key+"]", pd[key], pd2[key]);
			}
		}

	}
}
