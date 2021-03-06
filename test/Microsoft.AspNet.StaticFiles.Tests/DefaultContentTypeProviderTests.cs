// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Xunit;

namespace Microsoft.AspNet.StaticFiles
{
    public class DefaultContentTypeProviderTests
    {
        [Fact]
        public void UnknownExtensionsReturnFalse()
        {
            var provider = new FileExtensionContentTypeProvider();
            string contentType;
            Assert.False(provider.TryGetContentType("unknown.ext", out contentType));
        }

        [Fact]
        public void KnownExtensionsReturnTrye()
        {
            var provider = new FileExtensionContentTypeProvider();
            string contentType;
            Assert.True(provider.TryGetContentType("known.txt", out contentType));
            Assert.Equal("text/plain", contentType);
        }

        [Fact]
        public void DoubleDottedExtensionsAreNotSupported()
        {
            var provider = new FileExtensionContentTypeProvider();
            string contentType;
            Assert.False(provider.TryGetContentType("known.exe.config", out contentType));
        }

        [Fact]
        public void DashedExtensionsShouldBeMatched()
        {
            var provider = new FileExtensionContentTypeProvider();
            string contentType;
            Assert.True(provider.TryGetContentType("known.dvr-ms", out contentType));
            Assert.Equal("video/x-ms-dvr", contentType);
        }

        [Fact]
        public void BothSlashFormatsAreUnderstood()
        {
            var provider = new FileExtensionContentTypeProvider();
            string contentType;
            Assert.True(provider.TryGetContentType(@"/first/example.txt", out contentType));
            Assert.Equal("text/plain", contentType);
            Assert.True(provider.TryGetContentType(@"\second\example.txt", out contentType));
            Assert.Equal("text/plain", contentType);
        }

        [Fact]
        public void DotsInDirectoryAreIgnored()
        {
            var provider = new FileExtensionContentTypeProvider();
            string contentType;
            Assert.True(provider.TryGetContentType(@"/first.css/example.txt", out contentType));
            Assert.Equal("text/plain", contentType);
            Assert.True(provider.TryGetContentType(@"\second.css\example.txt", out contentType));
            Assert.Equal("text/plain", contentType);
        }
    }
}
