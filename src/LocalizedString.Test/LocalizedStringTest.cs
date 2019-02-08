﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading;
using Xunit;
using Xunit.Abstractions;

namespace clearwaterstream
{
    public class TestData
    {
        public TestData()
        {
            CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;

            SampleString = new LocalizedString("chicken")
                .InCanadianEnglish("chicken, eh")
                .InCanadianFrench("éh poulet")
                .InFrench("poulet")
                .InQueensEnglish("hen");
        }

        public LocalizedString SampleString { get; private set; }
    }

    [Collection("Sequential")]
    public class LocalizedStringTest : IClassFixture<TestData>
    {
        readonly ITestOutputHelper output;
        readonly LocalizedString sample;

        public LocalizedStringTest(ITestOutputHelper output, TestData testData)
        {
            this.output = output;
            sample = testData.SampleString;
        }

        [Fact]
        public void EqualInvariant()
        {
            Assert.Equal("chicken", sample.ToString());
        }

        [Fact]
        public void EqualOperator()
        {
            Assert.True(sample == "chicken");
        }

        [Fact]
        public void InFrench()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("fr-FR");

            Assert.Equal("poulet", sample.ToString());
        }
    }
}
