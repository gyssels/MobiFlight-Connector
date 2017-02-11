﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using MobiFlight.InputConfig;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MobiFlight.InputConfig.Tests
{
    [TestClass()]
    public class FsuipcOffsetInputActionTests
    {
        [TestMethod()]
        public void FsuipcOffsetInputActionTest()
        {
            FsuipcOffsetInputAction o = new FsuipcOffsetInputAction();
            Assert.AreEqual(o.FSUIPCOffset, FsuipcOffsetInputAction.FSUIPCOffsetNull, "FSUIPCOffset is not FSUIPCOffsetNull");
            Assert.AreEqual(o.FSUIPCMask,0xFF,"FSUIPCMask is not 0xFF");
            Assert.AreEqual(o.FSUIPCOffsetType,FSUIPCOffsetType.Integer,"FSUIPCOffsetType not correct");
            Assert.AreEqual(o.FSUIPCSize,1,"FSUIPCSize not correct");
            Assert.AreEqual(o.FSUIPCBcdMode,false,"Not correct");
            Assert.AreEqual(o.Value,"","Value not correct");
            Assert.IsNotNull(o.Transform, "Transform not initialized");
        }

        [TestMethod()]
        public void CloneTest()
        {
            FsuipcOffsetInputAction o = generateTestObject();
            FsuipcOffsetInputAction c = (FsuipcOffsetInputAction) o.Clone();

            Assert.AreNotSame(o, c, "Objects are the same");
            Assert.AreEqual(o.FSUIPCBcdMode, c.FSUIPCBcdMode, "FSUIPCBcdMode are not the same");
            Assert.AreEqual(o.FSUIPCMask, c.FSUIPCMask, "FSUIPCMask are not the same");
            Assert.AreEqual(o.FSUIPCOffset, c.FSUIPCOffset, "FSUIPCOffset are not the same");
            Assert.AreEqual(o.FSUIPCOffsetType, c.FSUIPCOffsetType, "FSUIPCOffsetType are not the same");
            Assert.AreEqual(o.FSUIPCSize, c.FSUIPCSize, "FSUIPCSize are not the same");
            Assert.AreEqual(o.Value, c.Value, "Value are not the same");
            Assert.AreEqual(o.Transform.Expression, c.Transform.Expression, "Value are not the same");
        }

        private FsuipcOffsetInputAction generateTestObject()
        {
            FsuipcOffsetInputAction o = new FsuipcOffsetInputAction();
            o.FSUIPCBcdMode = true;
            o.FSUIPCMask = 0xFFFF;
            o.FSUIPCOffset = 0x1234;
            o.FSUIPCOffsetType = FSUIPCOffsetType.Float;
            o.FSUIPCSize = 2;
            o.Value = "$+1";
            o.Transform.Expression = "$*1";
            return o;
        }

        [TestMethod()]
        public void WriteXmlTest()
        {
            StringWriter sw = new StringWriter();
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Encoding = System.Text.Encoding.UTF8;
            settings.Indent = true;
            //settings.NewLineHandling = NewLineHandling.Entitize;
            System.Xml.XmlWriter xmlWriter = System.Xml.XmlWriter.Create(sw, settings);

            FsuipcOffsetInputAction o = generateTestObject();
            xmlWriter.WriteStartElement("onPress");
            o.WriteXml(xmlWriter);
            xmlWriter.WriteEndElement();
            xmlWriter.Flush();
            string s = sw.ToString();

            String result = System.IO.File.ReadAllText(@"assets\MobiFlight\InputConfig\FsuipcOffsetInputAction\WriteXmlTest.1.xml");

            Assert.AreEqual(s, result, "The both strings are not equal");
        }

        [TestMethod()]
        public void ReadXmlTest()
        {
            FsuipcOffsetInputAction o = new FsuipcOffsetInputAction();
            String s = System.IO.File.ReadAllText(@"assets\MobiFlight\InputConfig\FsuipcOffsetInputAction\ReadXmlTest.1.xml");
            StringReader sr = new StringReader(s);
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreWhitespace = true;

            System.Xml.XmlReader xmlReader = System.Xml.XmlReader.Create(sr, settings);
            xmlReader.ReadToDescendant("onPress");
            o.ReadXml(xmlReader);

            Assert.AreEqual(o.FSUIPCBcdMode, true, "FSUIPCBcdMode are not the same");
            Assert.AreEqual(o.FSUIPCMask, 0xFFFFFFFF, "FSUIPCMask are not the same");
            Assert.AreEqual(o.FSUIPCOffset, 0x1234, "FSUIPCOffset are not the same");
            Assert.AreEqual(o.FSUIPCOffsetType, FSUIPCOffsetType.Float, "FSUIPCOffsetType are not the same");
            Assert.AreEqual(o.FSUIPCSize, 4, "FSUIPCSize are not the same");
            Assert.AreEqual(o.Value, "$-1", "Value are not the same");
        }

        [TestMethod()]
        public void executeTest()
        {
            FsuipcOffsetInputAction o = generateTestObject();
            MobiFlightUnitTests.mock.FSUIPC.FSUIPCCacheMock mock = new MobiFlightUnitTests.mock.FSUIPC.FSUIPCCacheMock();
            o.FSUIPCOffsetType = FSUIPCOffsetType.Integer;
            o.FSUIPCBcdMode = false;
            o.Value = "12";
            o.execute(mock, null);
            Assert.AreEqual(mock.Writes.Count, 1, "The message count is not as expected");
            Assert.AreEqual(mock.Writes[0].Offset, 0x1234, "The Offset is wrong");
            Assert.AreEqual(mock.Writes[0].Value, "12", "The Param Value is wrong");
        }
    }
}