﻿// --------------------------------------------------------------------------------------------
// <copyright file="GuidKeyFixture.cs" company="Effort Team">
//     Copyright (C) 2011-2014 Effort Team
//
//     Permission is hereby granted, free of charge, to any person obtaining a copy
//     of this software and associated documentation files (the "Software"), to deal
//     in the Software without restriction, including without limitation the rights
//     to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//     copies of the Software, and to permit persons to whom the Software is
//     furnished to do so, subject to the following conditions:
//
//     The above copyright notice and this permission notice shall be included in
//     all copies or substantial portions of the Software.
//
//     THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//     IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//     FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//     AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//     LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//     OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
//     THE SOFTWARE.
// </copyright>
// --------------------------------------------------------------------------------------------

namespace Effort.Test.Features
{
    using System;
    using System.Data.Common;
    using Effort.Test.Data.Features;
    using NUnit.Framework;

    [TestFixture]
    public class GuidKeyFixture
    {
        private FeatureDbContext context;

        [SetUp]
        public void Initialize()
        {
            DbConnection connection = 
                Effort.DbConnectionFactory.CreateTransient();

            this.context = 
                new FeatureDbContext(
                    connection, 
                    CompiledModels.GetModel<GuidKeyEntity>());
        }

        [TearDown]
        public void Cleanup()
        {
            this.context.Dispose();
        }

        [Test]
        public void GuidKey_InsertGeneratesNewGuid()
        {
            GuidKeyEntity entity = new GuidKeyEntity();

            this.context.GuidKeyEntities.Add(entity);
            this.context.SaveChanges();

            Assert.AreNotEqual(Guid.Empty, entity.Id);
        }

        [Test]
        public void GuidKey_UpdateKeepsGuid()
        {
            GuidKeyEntity entity = new GuidKeyEntity();

            this.context.GuidKeyEntities.Add(entity);
            this.context.SaveChanges();

            Guid guid = entity.Id;
            entity.Data = "Changed data";
            this.context.SaveChanges();

            Assert.AreEqual(guid, entity.Id);
        }
    }
}
